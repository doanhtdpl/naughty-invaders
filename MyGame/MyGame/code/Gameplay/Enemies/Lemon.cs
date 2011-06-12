using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Lemon : Enemy
    {
        const int CHANGE_POSES = 5;
        const int HITS_PER_POSE = 4;
        const float SCALE_INCREMENT = 0.07f;
        int poseChanges = 0;
        int currentHits = 0;

        bool scaling = false;
        const float SCALE_TIME = 0.1f;
        float scaleTimer = 0.1f;
        Vector2 backupScale;

        public Lemon(Vector3 position, float orientation)
            : base("lemon", position, orientation, 1)
        {
            life = 10.0f;
            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0.0f, 0.0f), 40);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            ++currentHits;
            scaling = true;
            scaleTimer = SCALE_TIME;
            backupScale = scale2D;
            parts[0].radius = parts[0].radius * (1 + SCALE_INCREMENT);
            playAction("pose" + Calc.randomNatural(1, 4).ToString());
            if (currentHits >= HITS_PER_POSE)
            {
                currentHits = 0;
                ++poseChanges;
                OrbManager.Instance.addOrbs(position2D, 2, 0, 0, 0);
                if (poseChanges >= CHANGE_POSES)
                {
                    SoundManager.Instance.playEffect(entityName + "Dies");
                    return false;

                }
            }
            ParticleManager.Instance.addParticles(entityName + "GotHit", this.position, Vector3.Zero, Color.White);
            SoundManager.Instance.playEffect(entityName + "GotHit");
            return true;
        }

        public override void die()
        {
            base.die();
        }

        public override void update()
        {
            base.update();

            scaleTimer -= SB.dt;
            if (scaling)
            {
                if (scaleTimer < 0)
                {
                    scale2D = backupScale * (1 + SCALE_INCREMENT);
                    scaling = false;
                }
                else
                {
                    float factor = (SCALE_TIME - scaleTimer) / SCALE_TIME;
                    factor = (float)Math.Sin(factor * (Calc.PI)) * SCALE_INCREMENT;
                    scale2D = backupScale * (1 + factor);
                }
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
