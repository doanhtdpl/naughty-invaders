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
            addStaticProp(new RenderableEntity2D("A", new Vector3(0, 100,  0), Vector2.One, 0));
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
        }

        public void dispose()
        {
            staticProps.Clear();
            animatedProps.Clear();
        }
    }
}
