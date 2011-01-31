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
        public void addEnemy(string name, Vector2 position)
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            
            // convert to the class format (first char is upper)
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);

            Type t = Type.GetType("MyGame." + name);
            Object[] args = { position, new Vector2(50, 50), 0.0f, name };

            Object o = Activator.CreateInstance(t, args);
            // NOTE: if this line fails the problem may be inside the constructors called when creating an instance of that type

            enemies.Add((Enemy)o);
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
