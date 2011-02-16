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
        List<Enemy> enemiesToDelete = new List<Enemy>();
        float nextSpawn = 0;

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
        public void addEnemy(Enemy e)
        {
            enemies.Add(e);
        }
        public Entity2D addEnemy(string name, Vector3 position)
        {
            Assembly assem = Assembly.GetExecutingAssembly();

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
            enemies.Remove(e);
        }
        public void removeEnemy(int i)
        {
            EntityManager.Instance.removeEntity(enemies[i]);
            enemies.RemoveAt(i);
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
            enemies.Clear();
        }

        public void dispose()
        {
            clean();
        }
        public List<Entity2D> getEnemies()
        {
            return enemies;
        }
        #endregion

        public void updateSleepingEnemies()
        {
            // if an enemy waypoint is almost entering the camera, create that new enemy
            for (int i = 0; i < enemies.Count; ++i)
            {
                Enemy e = (Enemy)enemies[i];
                if (e.entityState == Entity2D.tEntityState.Waiting && SB.cam.isVisible(enemies[i].getRectangle()))
                {
                    e.entityState = Entity2D.tEntityState.Active;
                }
            }
        }
        public void deleteEnemiesToDelete()
        {
            for (int i = 0; i < enemiesToDelete.Count; ++i)
            {
                enemiesToDelete[i].delete();
            }
        }
        public void update()
        {
            updateSleepingEnemies();

            nextSpawn -= SB.dt;
            if (nextSpawn < 0)
            {
                //Enemy e = new Enemy();
                //e.position2D = new Vector2(Calc.randomScalar(-500, 500), 400);
                //e.scale2D = new Vector2(20, 20);
                //addEnemy(e);
                nextSpawn = 1;
            }

            foreach (Enemy enemy in enemies)
            {
                if (enemy.entityState != Entity2D.tEntityState.Waiting)
                {
                    enemy.update();
                }
            }
            deleteEnemiesToDelete();
        }

        public void render()
        {
            //foreach (Enemy enemy in enemies)
            //    enemy.render();
        }
    }
}
