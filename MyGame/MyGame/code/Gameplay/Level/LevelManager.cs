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
        List<List<int>> groupList = new List<List<int>>();

        List<Line> levelCollisions = new List<Line>();

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
        public void removeEntity(Entity2D ent)
        {
            if (animatedProps.IndexOf(ent) >= 0)
                removeAnimatedProp(ent);
            else if (staticProps.IndexOf(ent) >= 0)
                removeStaticProp(ent);
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

            levelCollisions.Clear();
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

        public List<List<int>> getGroups()
        {
            return groupList;
        }
        public void addGroup(List<int> group)
        {
            foreach(List<int> list in groupList)
            {
                List<int> aux = new List<int>();
                aux.AddRange(list);
                if (list.Count == group.Count)
                {
                    foreach (int i in group)
                    {
                        aux.Remove(i);
                    }
                    if (aux.Count == 0)
                        return;
                }
            }
            groupList.Add(group);
        }
        #endregion

        public void addLevelCollision(Line l)
        {
            levelCollisions.Add(l);
        }
        public List<Line> getLevelCollisions()
        {
            return levelCollisions;
        }

        public void cleanLevel()
        {
            EntityManager.Instance.clean();
            LevelManager.Instance.clean();
            EnemyManager.Instance.clean();
            ProjectileManager.Instance.clean();
            CameraManager.Instance.clean();
            OrbManager.Instance.clean();
            ParticleManager.Instance.getParticles().Clear();
            CinematicManager.Instance.clean();
            groupList.Clear();
       }

        public void update()
        {
            foreach (AnimatedEntity2D ent in animatedProps)
            {
                ent.update();
            }
        }

        public void render()
        {
        }
    }
}
