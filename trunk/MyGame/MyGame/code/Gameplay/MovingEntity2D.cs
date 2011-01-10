using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class MovingEntity2D : Entity2D
    {
        public Vector2 direction { set; get; }
        public Vector2 acceleration { set; get; }
    }
}
