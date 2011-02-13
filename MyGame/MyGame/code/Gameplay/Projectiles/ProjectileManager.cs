﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class ProjectileManager
    {
        static ProjectileManager instance = null;
        ProjectileManager()
        {
        }
        public static ProjectileManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectileManager();
                }
                return instance;
            }
        }

        List<Entity2D> projectiles = new List<Entity2D>();
        List<Projectile> projectilesToDelete = new List<Projectile>();

        #region ENTITY MANAGEMENT
        public void addProjectile(Projectile p)
        {
            projectiles.Add(p);
        }
        public void removeProjectile(int i)
        {
            EntityManager.Instance.removeEntity(projectiles[i]);
            projectiles.RemoveAt(i);
        }
        public void removeProjectile(Entity2D p)
        {
            EntityManager.Instance.removeEntity(p);
            projectiles.Remove(p);
        }
        public void clean()
        {
            foreach (Projectile p in projectiles)
            {
                EntityManager.Instance.removeEntity(p);
            }
            projectiles.Clear();
        }
        public void dispose()
        {
            clean();
        }
        public void requestDeleteOf(Projectile p)
        {
            projectilesToDelete.Add(p);
        }
        #endregion

        public void deleteProjectilesToDelete()
        {
            for (int i = 0; i < projectilesToDelete.Count; ++i)
            {
                projectilesToDelete[i].delete();
            }
        }
        public void update()
        {
            List<Entity2D> enemies = EnemyManager.Instance.getEnemies();
            for (int i = 0; i < projectiles.Count; ++i)
            {
                Projectile p = (Projectile)projectiles[i];
                p.update();
                if (p.state != Entity2D.tEntityState.Active)
                {
                    continue;
                }
                // if projectile is from player...
                if (p.team == Projectile.tTeam.Players)
                {
                    for (int j = 0; j < enemies.Count; ++j)
                    {
                        Enemy e = (Enemy)enemies[j];
                        if (e.state != Entity2D.tEntityState.Active)
                        {
                            continue;
                        }
                        bool alive = true;
                        if (e.collidesWith(p, ref alive))
                        {
                            if (!alive)
                            {
                                e.die();
                            }
                            // projectile dies?
                            if (p.impact())
                            {
                                break;
                            }
                            // if the enemy died, go to the next enemy
                            if (!alive)
                            {
                                continue;
                            }
                        }
                    }
                }
                // if projectile is from enemy...
                if (p.team == Projectile.tTeam.Enemies)
                {
                    for (int j = 0; j < GamerManager.getGamerEntities().Count; ++j)
                    {
                        Player player = GamerManager.getGamerEntities()[j].Player;
                        if (player.state != Entity2D.tEntityState.Active)
                        {
                            continue;
                        }
                        bool alive = true;
                        if (player.collidesWith(p, ref alive))
                        {
                            if (!alive)
                            {
                                player.die();
                            }
                            // projectile dies?
                            if (p.impact())
                            {
                                break;
                            }
                            // if the enemy died, go to the next enemy
                            if (!alive)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            deleteProjectilesToDelete();
        }

        public void render()
        {
            //foreach (Projectile projectile in projectiles)
            //    projectile.render();
        }
    }
}
