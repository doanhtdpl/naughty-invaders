using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class BasicProjectile : Projectile
    {
        public BasicProjectile(string name, Vector3 position, Vector2 scale, float orientation, Vector2 direction )
            : base(name, position, scale, orientation, direction, 100, 500, 1, 0.2f, tTeam.Players)
        {
            playAction("start");
        }
    }
}
