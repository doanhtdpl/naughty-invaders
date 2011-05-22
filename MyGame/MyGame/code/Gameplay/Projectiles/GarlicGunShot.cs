using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class GarlicGunShot : Projectile
    {
        public GarlicGunShot(Vector3 position, Vector2 direction)
            : base("playerProjectile", position, 0, direction, 10, 2000, 1, 0.05f, tTeam.Players)
        {
            playAction("start");
            setCollisions();
            scale2D = new Vector2(80, 80);
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), 25.0f);
        }
    }
}
