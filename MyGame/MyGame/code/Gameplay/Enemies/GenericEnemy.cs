using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class GenericEnemy : Enemy
    {
        float nextAttackTimer;

        public GenericEnemy(Vector3 position, float orientation, string name)
            : base(name, position, orientation, 1)
        {
            life = 10.0f;
            nextAttackTimer = Calc.randomScalar(2.0f, 2.5f);

            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 20), 40);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            return true;
        }

        public override void update()
        {
            base.update();

            // always move down
            position += new Vector3(0, -30.0f, 0) * SB.dt;

            nextAttackTimer -= SB.dt;

            if (nextAttackTimer < 0)
            {
                playAction("attack");
                nextAttackTimer = 3.0f;
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
