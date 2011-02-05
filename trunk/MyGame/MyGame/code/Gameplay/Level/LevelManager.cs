using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    struct sEnemyWaypoint
    {
        public string name;
        public Vector2 position;
        public Rectangle rectangle;

        public sEnemyWaypoint(string name, Vector2 position, Rectangle rectangle)
        {
            this.name = name;
            this.position = position;
            this.rectangle = rectangle;
        }
    }

    class LevelManager
    {
        List<Entity2D> staticProps = new List<Entity2D>();
        List<Entity2D> animatedProps = new List<Entity2D>();
        List<sEnemyWaypoint> enemyWaypoints = new List<sEnemyWaypoint>();

        static LevelManager instance = null;

        LevelManager()
        {
        }

        public static LevelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LevelManager();
                }
                return instance;
            }
        }

        #region ENTITY MANAGEMENT
        public void addStaticProp(Entity2D re)
        {
            staticProps.Add(re);
        }
        public void addAnimatedProp(Entity2D ae)
        {
            animatedProps.Add(ae);
        }
        public void removeStaticProp(Entity2D re)
        {
            EntityManager.Instance.removeEntity(re);
            staticProps.Remove(re);
        }
        public void removeStaticProp(int i)
        {
            removeStaticProp(staticProps[i]);
        }
        public void removeAnimatedProp(Entity2D ae)
        {
            EntityManager.Instance.removeEntity(ae);
            animatedProps.Remove(ae);
        }
        public void removeAnimatedProp(int i)
        {
            removeStaticProp(animatedProps[i]);
        }
        public void clean()
        {
            foreach (Entity2D sp in staticProps)
            {
                EntityManager.Instance.removeEntity(sp);
            }
            staticProps.Clear();
            foreach (Entity2D ae in animatedProps)
            {
                EntityManager.Instance.removeEntity(ae);
            }
            animatedProps.Clear();

            enemyWaypoints.Clear();
        }
        public void dispose()
        {
            clean();
        }
        public List<Entity2D> getStaticProps()
        {
            return staticProps;
        }
        public List<Entity2D> getAnimatedProps()
        {
            return animatedProps;
        }
        #endregion

        public void cleanLevel()
        {
            EntityManager.Instance.clean();
            LevelManager.Instance.clean();
            EnemyManager.Instance.clean();
            ProjectileManager.Instance.clean();
        }

        public void update()
        {
            // if an enemy waypoint is almost entering the camera, create that new enemy
            for(int i=0; i<enemyWaypoints.Count; ++i)
            {
                if (SB.cam.isVisible(enemyWaypoints[i].rectangle))
                {
                    EnemyManager.Instance.addEnemy(enemyWaypoints[i].name, enemyWaypoints[i].position);
                    enemyWaypoints.RemoveAt(i);
                    --i;
                }
            }

            foreach (AnimatedEntity2D ent in animatedProps)
            {
                ent.update();
            }
        }

        public void render()
        {
            //foreach (RenderableEntity2D ent in staticProps)
            //{
            //    ent.render();
            //}

            //foreach (AnimatedEntity2D ent in animatedProps)
            //{
            //    ent.render();
            //}
        }
    }
}
