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
        const float SCALE_MULTIPLIER = 1.1f;
        int poseChanges = 0;
        int currentHits = 0;

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
            if (currentHits >= HITS_PER_POSE)
            {
                currentHits = 0;
                ++poseChanges;
                scale *= SCALE_MULTIPLIER;
                parts[0].setRadius(parts[0].radius * SCALE_MULTIPLIER);
                playAction("pose" + Calc.randomNatural(1,4).ToString());
                OrbManager.Instance.addOrbs(position2D, 2, 0, 0, 0);
                if (poseChanges >= CHANGE_POSES)
                {
                    return false;
                }
            }
            return true;
        }

        public override void die()
        {
            base.die();
        }

        public override void update()
        {
            base.update();
        }

        public override void render()
        {
            base.render();
        }
    }
}
