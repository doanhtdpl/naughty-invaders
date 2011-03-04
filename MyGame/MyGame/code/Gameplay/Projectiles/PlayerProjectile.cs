using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class PlayerProjectile : Projectile
    {
        public PlayerProjectile(Vector3 position)
            : base("playerProjectile", position, 0, Vector2.UnitY, 10, 800, 1, 0.08f, tTeam.Players)
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
