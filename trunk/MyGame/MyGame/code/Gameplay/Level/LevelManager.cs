using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class LevelManager
    {
        List<RenderableEntity2D> staticProps = new List<RenderableEntity2D>();
        List<AnimatedEntity2D> animatedProps = new List<AnimatedEntity2D>();

        float nextSpawn = 0;

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

        public void addStaticProp(RenderableEntity2D re)
        {
            staticProps.Add(re);
        }
        public void addAnimatedProp(AnimatedEntity2D ae)
        {
            animatedProps.Add(ae);
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
