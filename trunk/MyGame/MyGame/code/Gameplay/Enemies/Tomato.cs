using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Tomato : Enemy
    {
        const float SPEED = 400.0f;
        const float LATERAL_SPEED = 1.0f;

        public Tomato(Vector3 position, float orientation)
            : base("tomato", position, orientation)
        {
            life = 10.0f;
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
            ParticleManager.Instance.addParticles("grapeDies", this.position, Vector3.Zero, Color.White);
        }

        public override void update()
        {
            base.update();

            // always move down
            Vector3 posToAdd = new Vector3(0, -SPEED, 0) * SB.dt;
            // go to the player
            posToAdd.X += (GamerManager.getSessionOwner().Player.position.X - position.X) * LATERAL_SPEED * SB.dt;

            position += posToAdd;
        }

        public override void render()
        {
            base.render();
        }
    }
}
