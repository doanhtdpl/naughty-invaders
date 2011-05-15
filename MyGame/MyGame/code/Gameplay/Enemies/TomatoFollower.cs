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
        const float MAX_SPEED = 450.0f;

        float speed;

        public TomatoFollower(Vector3 position, float orientation)
            : base("tomato", position, orientation, 1)
        {
            life = 10.0f;
            speed = Calc.randomScalar(MIN_SPEED, MAX_SPEED);
            setCollisions();
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

            // always move down
            Vector3 posToAdd = (GamerManager.getSessionOwner().Player.position - position);
            // go to the player
            posToAdd.Normalize();
            posToAdd += posToAdd * speed * SB.dt;

            position += posToAdd;
        }

        public override void render()
        {
            base.render();
        }
    }
}
