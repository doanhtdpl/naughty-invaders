using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Grapes : Enemy
    {
//        public Grapes()
        public Grapes(Vector2 position, Vector2 scale, float orientation, string entityName)
            : base(new Vector3(position.X, position.Y, 0), scale, orientation, entityName)
//            : base (Vector3.Zero, Vector2.Zero, 0, "woala")
        {

        }

        public override void update()
        {
            //base.update();
            position = new Vector3(position.X, position.Y - 100 * SB.dt, 0);
        }

        public override void render()
        {
            base.render();
        }

        // applies damage, returns true if enemy dies
        public override bool getsHit()
        {
            return true;
        }
    }
}
