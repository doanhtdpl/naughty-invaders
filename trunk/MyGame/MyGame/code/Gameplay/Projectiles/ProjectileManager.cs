using System;
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

        List<Projectile> projectiles = new List<Projectile>();

        public void update()
        {
            bool breakOuter = false;
            Rectangle projectileRectangle;
            List<Enemy> enemies = EnemyManager.Instance.getEnemies();

            for (int i = 0; i < projectiles.Count; ++i)
            {
                if (!projectiles[i].update())
                {
                    projectiles.RemoveAt(i);
                    --i;
                    continue;
                }
                projectileRectangle = projectiles[i].getRectangle();
                // if projectile is from player...
                if (projectiles[i].team == Projectile.tTeam.Players)
                {
                    for (int j = 0; j < enemies.Count; ++j)
                    {
                        if (enemies[j].getRectangle().Intersects(projectileRectangle))
                        {
                            // the enemy get hit!
                            if (enemies[j].getsHit())
                            {
                                enemies.RemoveAt(j);
                                --j;
                                break;
                            }

                            // projectile dies?
                            if (projectiles[i].impact())
                            {
                                projectiles.RemoveAt(i);
                                --i;
                                breakOuter = true;
                                break;
                            }
                        }
                    }
                    if (breakOuter)
                    {
                        breakOuter = false;
                        break;
                    }
                }
                // if projectile is from enemy...
                if (projectiles[i].team == Projectile.tTeam.Enemies)
                {

                }
            }
        }

        public void addProjectile(Projectile p)
        {
            projectiles.Add(p);
        }

        void removeProjectile(int i)
        {
            projectiles.RemoveAt(i);
        }

        public void render()
        {
            foreach (Projectile projectile in projectiles)
                projectile.render();
        }

        public void dispose()
        {
            projectiles.Clear();
        }
    }
}
