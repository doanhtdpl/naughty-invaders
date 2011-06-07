using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class MacedoniaMinigame : AnimatedEntity2D
    {
        public MacedoniaMinigame(Vector3 position, float orientation)
            : base("enemies", "macedonia", position, orientation, Color.White)
        {
        }

        public override void update()
        {
            base.update();
        }
    }
}
