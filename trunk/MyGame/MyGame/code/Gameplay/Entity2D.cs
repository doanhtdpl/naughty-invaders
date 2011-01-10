using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class Entity2D
    {
        public Vector2 position { get; set; }
        public Vector2 size { get; set; }
        public float orientation { get; set; }

        public Rectangle getRectangle()
        {
            return new Rectangle((int)(position.X - size.X * 0.5), (int)(position.Y - size.Y * 0.5), (int)size.X, (int)size.Y);
        }
    }
}
