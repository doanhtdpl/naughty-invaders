using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class GrapeProjectile : Projectile
    {
        public GrapeProjectile(Vector3 position)
            : base("grapeProjectile", position, 0, -Vector2.UnitY, 10, 300, 1, 0.2f, tTeam.Enemies)
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
