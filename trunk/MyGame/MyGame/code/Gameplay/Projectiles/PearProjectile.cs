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
            : base("pearProjectile", position, Calc.directionToAngle(direction), direction, 10, 300, 1, 0.2f, tTeam.Enemies)
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
