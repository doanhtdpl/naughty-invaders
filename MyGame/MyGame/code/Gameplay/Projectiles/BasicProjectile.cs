using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class BasicProjectile : Projectile
    {
        //public BasicProjectile(float damage, int lifes, tTeam team) : base(damage, lifes, team) { }
        public BasicProjectile(Vector3 position, Vector2 scale, float orientation )
            : base(position, scale, orientation, "basicProjectile", 100, 100, 1, 0.2f, tTeam.Players) { }
    }
}
