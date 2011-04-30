﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class PlayerBigShot : Projectile
    {
        const float MINIMUM_DAMAGE = 15.0f;
        const float MAXIMUM_DAMAGE = 50;
        const float FULL_CHARGE_DAMAGE = 100.0f;

        public PlayerBigShot(Vector3 position, float scale, float chargeValue)
            : base("playerProjectile", position, 0, Vector2.UnitY, 0.0f, 800, 1, 0.2f, tTeam.Players)
        {
            playAction("start");
            setCollisions(scale);
            scale2D = new Vector2(80 * scale, 80 * scale);
            color = Color.OrangeRed;

            if (chargeValue == 1.0f)
            {
                this.damage = FULL_CHARGE_DAMAGE;
            }
            else
            {
                this.damage = (MAXIMUM_DAMAGE - MINIMUM_DAMAGE) * chargeValue + MINIMUM_DAMAGE;
            }

            special = tSpecial.BreaksGuard;
        }

        public virtual void setCollisions(float scale)
        {
            addCollision(new Vector2(0, 0), 25.0f * scale);
        }
    }
}