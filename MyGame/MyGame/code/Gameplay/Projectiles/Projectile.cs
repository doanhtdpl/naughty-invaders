﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Projectile : CollidableEntity2D
    {
        public float speed { set; get; }
        public int lifes { set; get; }
        public bool remove { set; get; }
        public float cooldown { set; get; }
        public Vector2 direction { set; get; }

        public enum tTeam { Players, Enemies };
        public tTeam team { set; get; }

        public Projectile(string name, Vector3 position, float orientation, Vector2 direction, float damage, float speed, int lifes, float cooldown, tTeam team):base("projectiles", name, position, orientation, Color.White, damage)
        {
            entityName = name;
            remove = false;
            entityState = tEntityState.Active;
            this.direction = direction;
            this.speed = speed;
            this.lifes = lifes;
            this.cooldown = cooldown;
            this.team = team;
        }

        public override void update()
        {
            base.update();
            position2D += direction * speed * SB.dt;
        }

        public override void render()
        {
            base.render();
        }

        // returns true if projectile dies
        public virtual bool impact()
        {
            --lifes;
            if (lifes <= 0)
            {
                this.die();
                return true;
            }
            return false;
        }

        public override void delete()
        {
            ProjectileManager.Instance.removeProjectile(this);
            base.delete();
        }
        public override void requestDelete(bool force = false)
        {
            if (avoidDelete && !force) return;

            base.requestDelete();
        }
    }
}
