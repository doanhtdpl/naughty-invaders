using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class PlayerFastShot : Projectile
    {
        public PlayerFastShot(Vector3 position, float factor, Color color)
            : base("playerProjectile", position, 0, Vector2.UnitY, 10 * factor, 800, 1, 0.08f, tTeam.Players)
        {
            playAction("start");
            setCollisions();
            scale2D = new Vector2(80, 80) * factor;
            this.color = color;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), 25.0f);
        }
    }
}
