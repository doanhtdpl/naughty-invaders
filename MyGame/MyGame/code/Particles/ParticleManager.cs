using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class ParticleManager
    {
        static ParticleManager instance = null;
        ParticleManager()
        {
        }
        public static ParticleManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ParticleManager();
                }
                return instance;
            }
        }

        public static void resetParticles()
        {
        }

        public void update()
        {   
        }

        public void render()
        {
        }
    }
}
