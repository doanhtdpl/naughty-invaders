using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    class BasicProjectile : Projectile
    {
        //public BasicProjectile(float damage, int lifes, tTeam team) : base(damage, lifes, team) { }
        public BasicProjectile() : base(100, 100, 1, 0.2f, tTeam.Players) { }
    }
}
