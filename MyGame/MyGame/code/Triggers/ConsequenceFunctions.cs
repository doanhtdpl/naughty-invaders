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

        public static bool activateGarlicGun(RenderableEntity2D player)
        {
            ((Player)player).activateGarlicGun();
            SoundManager.Instance.playEffect("equipGarlicGun");
            return true;
        }

        public static bool wishEntersGame()
        {
            ParticleManager.Instance.addParticles("playerFastShot", new Vector3(0, -50, 0), Vector3.One, Color.White, 5.0f);
            GamerManager.getMainPlayer().position = new Vector3(0, 0, 0);

            return true;
        }
    }
}
