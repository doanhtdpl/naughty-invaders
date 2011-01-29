using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class LevelManager
    {
        List<Entity2D> staticProps = new List<Entity2D>();
        List<Entity2D> animatedProps = new List<Entity2D>();

        static LevelManager instance = null;

        LevelManager()
        {
            addStaticProp(new RenderableEntity2D("test", new Vector3(0, 100,  0), new Vector2(50, 50), 0));
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
            staticProps.Remove(re);
        }
        public void removeAnimatedProp(Entity2D ae)
        {
            animatedProps.Remove(ae);
        }

        public List<Entity2D> getStaticProps()
        {
            return staticProps;
        }
        public List<Entity2D> getAnimatedProps()
        {
            return animatedProps;
        }

        public void loadXML(string levelName)
        {

        }

        public void update()
        {
        }

        void removeStaticProp(int i)
        {
            staticProps.RemoveAt(i);
        }
        void removeAnimatedProp(int i)
        {
            animatedProps.RemoveAt(i);
        }

        public void render()
        {
            foreach (RenderableEntity2D ent in staticProps)
            {
                ent.render();
            }
        }

        public void dispose()
        {
            staticProps.Clear();
            animatedProps.Clear();
        }
    }
}
