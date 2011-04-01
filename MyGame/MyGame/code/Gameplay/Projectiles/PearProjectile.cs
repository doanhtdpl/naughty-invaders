using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class PearProjectile : Projectile
    {
        public PearProjectile(Vector3 position, Vector2 direction)
            : base("pearProjectile", position, Calc.directionToAngle(direction) + MathHelper.ToRadians(90.0f), direction, 10, 200, 1, 0.2f, tTeam.Enemies)
        {
            playAction("start");
            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), 25.0f);
        }
    }
}
