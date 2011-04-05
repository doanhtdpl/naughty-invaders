using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class PlayerBigShot : Projectile
    {
        public PlayerBigShot(Vector3 position, float scale)
            : base("playerProjectile", position, 0, Vector2.UnitY, 10, 800, 1, 0.2f, tTeam.Players)
        {
            playAction("start");
            setCollisions(scale);
            scale2D = new Vector2(80 * scale, 80 * scale);
            color = Color.OrangeRed;
        }

        public virtual void setCollisions(float scale)
        {
            addCollision(new Vector2(0, 0), 25.0f * scale);
        }
    }
}
