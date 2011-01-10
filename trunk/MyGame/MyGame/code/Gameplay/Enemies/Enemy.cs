using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class Enemy : AnimatedEntity2D
    {
        public Enemy()
        {
        }

        public void update()
        {
            position = new Vector2(position.X, position.Y - 100 * SB.dt);
        }

        public void render()
        {
            Player.sampleTex.render(position, orientation);
        }

        // applies damage, returns true if enemy dies
        public bool getsHit()
        {
            return true;
        }
    }
}
