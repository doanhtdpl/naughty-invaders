using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Calsot : Enemy
    {
        const float SPEED = 600.0f;

        public Calsot(Vector3 position, float orientation)
            : base("calsot", position, orientation, 2)
        {
            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0.0f, -100.0f), 25);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            ParticleManager.Instance.addParticles(entityName + "GotHit", this.position, Vector3.Zero, Color.White);
            SoundManager.Instance.playEffect(entityName + "GotHit");
            return true;
        }

        public override void die()
        {
            //base.die();
            //ParticleManager.Instance.addParticles("grapeDies", this.position, Vector3.Zero);
        }

        public override void update()
        {
            base.update();

            // always move down
            position += new Vector3(0, -SPEED, 0) * SB.dt;
        }

        public override void render()
        {
            base.render();
        }
    }
}
