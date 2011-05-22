using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace MyGame
{
    class ConsequenceFunctions
    {
        public static bool addParticles()
        {
            ParticleManager.Instance.addParticles("playerFastShot", new Vector3(0, 50, 0), Vector3.One, Color.White);
            return true;
        }

        public static void activateGarlicGun(RenderableEntity2D player)
        {
            ((Player)player).activateGarlicGun();
        }
    }
}
