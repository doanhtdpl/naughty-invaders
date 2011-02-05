﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class Projectile : MovingEntity2D
    {
        public float damage { set; get; }
        public float speed { set; get; }
        public int lifes { set; get; }
        public bool remove { set; get; }
        public bool active { set; get; }
        public float cooldown { set; get; }

        public enum tTeam { Players, Enemies };
        public tTeam team { set; get; }

        public Projectile(Vector3 position, Vector2 scale, float orientation, string name, float damage, float speed, int lifes, float cooldown, tTeam team):base(name, position, scale, orientation)
        {
            remove = false;
            active = true;
            this.damage = damage;
            this.speed = speed;
            this.lifes = lifes;
            this.cooldown = cooldown;
            this.team = team;
        }

        public override void update()
        {
            position = new Vector3(position.X, position.Y + speed * SB.dt, 0);
        }

        public override void render()
        {
            TextureManager.Instance.getTexture("A").render(worldMatrix);
        }

        // returns true if projectile dies
        public virtual bool impact()
        {
            --lifes;
            return (lifes <= 0);
        }
    }
}
