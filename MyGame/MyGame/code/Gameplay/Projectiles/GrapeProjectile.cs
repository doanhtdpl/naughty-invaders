using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class GrapeProjectile : Projectile
    {
        public GrapeProjectile(Vector3 position, float orientation, Vector2 direction)
            : base("grapeProjectile", position, orientation, direction, 10, 300, 1, 0.2f, tTeam.Enemies)
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
