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
            initializeWorldMatrix2D(Vector3.Zero, new Vector2(20, 20), 0);
        }

        public override void update()
        {
            //base.update();
            position = new Vector3(position.X, position.Y - 100 * SB.dt, 0);
        }

        public override void render()
        {
            //base.render();
        }

        // applies damage, returns true if enemy dies
        public bool getsHit()
        {
            return true;
        }
    }
}
