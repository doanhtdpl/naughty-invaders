using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Enemy : AnimatedEntity2D
    {
        public bool active { get; set; }

        public Enemy(string entityName, Vector3 position, Vector2 scale, float orientation)
            : base(entityName, position, scale, orientation)
        {
            active = false;
        }

        public override void update()
        {
            base.update();
        }

        public override void render()
        {
            base.render();
        }

        // applies damage, returns true if enemy dies
        public virtual bool getsHit()
        {
            return true;
        }
    }
}
