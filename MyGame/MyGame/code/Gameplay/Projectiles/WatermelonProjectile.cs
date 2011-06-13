using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class WatermelonProjectile : Projectile
    {
        public WatermelonProjectile(Vector3 position, float orientation, Vector2 direction, float speed)
            : base("watermelonProjectile", position, orientation, direction, 10, speed, 1, 0.2f, tTeam.Enemies)
        {
            playAction("start");
            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), 5.0f);
        }
    }
}
