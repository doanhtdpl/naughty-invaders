using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class TomatoFollower : Enemy
    {
        const float MIN_SPEED = 300.0f;
        const float MAX_SPEED = 550.0f;
        // turning speed in radians/second
        const float TURNING_SPEED = 2.5f;

        float speed;
        Vector2 direction;
        public bool fleeing { get; set; }

        public TomatoFollower(Vector3 position, float orientation)
            : base("tomato", position, orientation, 1)
        {
            life = 10.0f;
            speed = Calc.randomScalar(MIN_SPEED, MAX_SPEED);
            setCollisions();
            direction = (GamerManager.getSessionOwner().Player.position - position).toVector2();
            direction.Normalize();
            fleeing = false;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0.0f, 0.0f), 30);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            life -= ce.damage;
            return life > 0;
        }

        public override void die()
        {
            base.die();
            ParticleManager.Instance.addParticles("tomatoDies", this.position, Vector3.Zero, Color.White);
        }

        public override void update()
        {
            base.update();

            Vector3 directionTo;
            if (fleeing)
            {
                directionTo = (position - GamerManager.getSessionOwner().Player.position);
            }
            else
            {
                directionTo = (GamerManager.getSessionOwner().Player.position - position);
            }
            directionTo.Normalize();
            direction = Calc.fromDirectionToDirectionAtSpeed(direction, directionTo.toVector2(), TURNING_SPEED);
            orientation = Calc.directionToAngle(direction) + Calc.PiOver2;
            position2D += direction * speed * SB.dt;
        }

        public override void render()
        {
            base.render();
        }
    }
}
