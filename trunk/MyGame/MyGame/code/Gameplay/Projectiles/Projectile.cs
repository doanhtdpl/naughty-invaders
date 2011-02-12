using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class Projectile : AnimatedEntity2D
    {
        public float damage { set; get; }
        public float speed { set; get; }
        public int lifes { set; get; }
        public bool remove { set; get; }
        public bool active { set; get; }
        public float cooldown { set; get; }

        public enum tTeam { Players, Enemies };
        public tTeam team { set; get; }

        public Projectile(string name, Vector3 position, Vector2 scale, float orientation, Vector2 direction, float damage, float speed, int lifes, float cooldown, tTeam team):base("projectiles", name, position, orientation)
        {
            entityName = name;
            remove = false;
            active = true;
            this.direction2D = direction;
            this.damage = damage;
            this.speed = speed;
            this.lifes = lifes;
            this.cooldown = cooldown;
            this.team = team;
        }

        public override void update()
        {
            base.update();
            position2D += direction2D * speed * SB.dt;
        }

        public override void render()
        {
            base.render();
        }

        // returns true if projectile dies
        public virtual bool impact()
        {
            --lifes;
            return (lifes <= 0);
        }
    }
}
