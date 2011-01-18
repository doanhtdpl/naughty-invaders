using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class EnemyManager
    {
        List<Enemy> enemies = new List<Enemy>();
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

        public void addEnemy(Enemy e)
        {
            enemies.Add(e);
        }

        public List<Enemy> getEnemies()
        {
            return enemies;
        }

        public void update()
        {
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
                enemy.update();
            }
        }

        void removeEnemy(int i)
        {
            enemies.RemoveAt(i);
        }

        public void render()
        {
            foreach (Enemy enemy in enemies)
                enemy.render();
        }

        public void dispose()
        {
            enemies.Clear();
        }
    }
}
