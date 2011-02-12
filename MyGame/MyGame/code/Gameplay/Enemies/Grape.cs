using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Grape : Enemy
    {
        const float SPEED = 20.0f;
        bool moveRight;
        float nextMoveTimer;
        float movingTimer;
        float nextAttackTimer;

        public Grape(Vector2 position, Vector2 scale, float orientation)
            : base("grape", new Vector3(position.X, position.Y, 0), orientation)
        {
            nextMoveTimer = Calc.randomScalar(1.0f, 2.0f);
            nextAttackTimer = Calc.randomScalar(2.0f, 2.5f);
        }

        public override void update()
        {
            base.update();

            // always move down
            position += new Vector3(0, -SPEED, 0) * SB.dt;

            nextMoveTimer -= SB.dt;
            nextAttackTimer -= SB.dt;
            movingTimer -= SB.dt;

            if (nextMoveTimer < 0)
            {
                // prepare move
                movingTimer = Calc.randomScalar(0.5f, 2.0f);
                moveRight = Calc.randomScalar() < 0.5f;
                // prepare next move
                nextMoveTimer = Calc.randomScalar(2.0f, 3.0f);
            }
            if (nextAttackTimer < 0)
            {
                playAction("attack");
                nextAttackTimer = Calc.randomScalar(1.0f, 3.0f);
            }
            if (movingTimer < 0)
            {
                if (moveRight)
                {
                    position += new Vector3(30.0f, 0.0f, 0.0f) * SB.dt;
                }
                else
                {
                    position += new Vector3(-30.0f, 0.0f, 0.0f) * SB.dt;
                }
            }
        }

        public override void render()
        {
            base.render();
        }

        // applies damage, returns true if enemy dies
        public override bool getsHit()
        {
            base.getsHit();
            return true;
        }
    }
}
