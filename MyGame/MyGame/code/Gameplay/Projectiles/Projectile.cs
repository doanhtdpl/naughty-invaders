using System;
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

        public Projectile(float damage, float speed, int lifes, float cooldown, tTeam team)
        {
            remove = false;
            active = true;
            this.damage = damage;
            this.speed = speed;
            this.lifes = lifes;
            this.cooldown = cooldown;
            this.team = team;
        }

        public bool update()
        {
            position = new Vector2(position.X, position.Y + speed * SB.dt);
            return true;
        }

        public void render()
        {
            TextureManager.Instance.getTexture("A").render(position, orientation, new Vector2(100,100));
        }

        // returns true if projectile dies
        public virtual bool impact()
        {
            --lifes;
            return (lifes <= 0);
        }
    }
}
