using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace MyGame
{
    class EnemyManager
    {
        List<Entity2D> enemies = new List<Entity2D>();
        List<Entity2D> activeEnemies = new List<Entity2D>();
        List<Enemy> enemiesToDelete = new List<Enemy>();
        List<EnemySpawnZone> enemySpawnZones = new List<EnemySpawnZone>();

        static EnemyManager instance = null;

        EnemyManager()
        {
        }

        public static EnemyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyManager();
                }
                return instance;
            }
        }

        #region ENTITY MANAGEMENT
        public void addEnemySpawnZone(EnemySpawnZone enemySpawnZone)
        {
            enemySpawnZones.Add(enemySpawnZone);
        }
        public void addEnemy(Enemy e)
        {
            enemies.Add(e);
        }
        public Entity2D addEnemy(string name, Vector3 position, int id = -1)
        {
            // convert to the class format (first char is upper)
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);

            Type t = Type.GetType("MyGame." + name);
            Object[] args = { position, 0.0f };
            if (t == null)
            {
                t = Type.GetType("MyGame.GenericEnemy");
                args = new Object[]{ position, 0.0f, name };
            }

            Object o = Activator.CreateInstance(t, args);
            // NOTE: if this line fails the problem may be inside the constructors called when creating an instance of that type

            Enemy e = (Enemy)o;
            enemies.Add(e);
            return e;
        }
        public void removeEnemy(Entity2D e)
        {
            EntityManager.Instance.removeEntity(e);
            if (enemies.Contains(e))
            {
                enemies.Remove(e);
            }
            else
            {
                activeEnemies.Remove(e);
            }
        }
        public void requestDeleteOf(Enemy e)
        {
            enemiesToDelete.Add(e);
        }
        public void clean()
        {
            foreach (Enemy e in enemies)
            {
                EntityManager.Instance.removeEntity(e);
            }
            foreach (Enemy e in activeEnemies)
            {
                EntityManager.Instance.removeEntity(e);
            }
            enemies.Clear();
            activeEnemies.Clear();

            enemySpawnZones.Clear();
        }

        public void dispose()
        {
            clean();
        }
        public List<Entity2D> getEnemies()
        {
            return enemies;
        }
        public List<Entity2D> getActiveEnemies()
        {
            return activeEnemies;
        }
        public List<EnemySpawnZone> getEnemySpawnZones()
        {
            return enemySpawnZones;
        }
        #endregion

        public void updateSleepingEnemies()
        {
            // if an enemy waypoint is almost entering the camera, create that new enemy
            for (int i = 0; i < enemies.Count; ++i)
            {
                Enemy e = (Enemy)enemies[i];
                if (SB.cam.isVisible(enemies[i].getRectangle()))
                {
                    if (e is Orange)
                    {
                        ((Orange)e).initialize();
                    }
                    // activate
                    activeEnemies.Add(e);
                    enemies.RemoveAt(i);
                }
            }
        }
        public void deleteEnemiesToDelete()
        {
            for (int i = 0; i < enemiesToDelete.Count; ++i)
            {
                enemiesToDelete[i].delete();
            }
            enemiesToDelete.Clear();
        }
        public void checkCollisionsWithPlayers()
        {
            bool alive = true;
            foreach (Player p in GamerManager.getPlayerEntities())
            {
                foreach (Enemy enemy in activeEnemies)
                {
                    if (enemy.entityState == Entity2D.tEntityState.Dying) continue;

                    if (p.collidesWith(enemy, ref alive))
                    {
                        if (!alive)
                        {
                            p.die();
                        }
                    }
                }
            }
        }
        public void updateEnemySpawnZones()
        {
            float cameraTopY = Camera2D.screen.Bottom;
            for (int i = 0; i < enemySpawnZones.Count; ++i)
            {
                if (!enemySpawnZones[i].update(cameraTopY))
                {
                    enemySpawnZones.RemoveAt(i);
                    --i;
                }
            }
        }

        public void update()
        {
            updateEnemySpawnZones();
            updateSleepingEnemies();

            checkCollisionsWithPlayers();

            foreach (Enemy enemy in activeEnemies)
            {
                // if the projectile is out of screen, delete it
                if (!CoolizionManager.pointVSrectangle(enemy.position2D, Camera2D.screenWithMargins, (int)enemy.getRadius()))
                {
                    requestDeleteOf(enemy);
                    continue;
                }

                enemy.update();
            }
            deleteEnemiesToDelete();
        }

#if DEBUG
        public void renderDebug()
        {
            for (int i = 0; i < enemySpawnZones.Count; ++i)
            {
                DebugManager.Instance.addRectangle(enemySpawnZones[i].getZone(), Color.White);
            }
        }
#endif
    }
}
