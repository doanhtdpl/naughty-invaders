using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Watermelon : Enemy
    {
        enum tWatermelonState { IdleProt, ReadyToAttack, ReadyToIdle }

        tWatermelonState state;

        const float SPEED = -15.0f;
        const float MIN_SHOT_ANGLE = -Calc.PiOver2 - 0.6f;
        const float MAX_SHOT_ANGLE = -Calc.PiOver2 + 0.6f;
        const int NUMBER_OF_PROJECTILES = 5;

        float vulnerableTime;
        float nextAttackTimer;

        public Watermelon(Vector3 position, float orientation)
            : base("watermelon", position, orientation, 10)
        {
            life = 800.0f;

            vulnerableTime = Calc.randomScalar(1.0f, 2.0f);
            nextAttackTimer = Calc.randomScalar(4.0f, 5.0f);

            playAction("idleProt");
            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 20), 75);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            if (getCurrentAction() == "idleProt" && ce.special != tSpecial.BreaksGuard)
            {
                life -= ce.damage * 0.1f;
            }
            else
            {
                life -= ce.damage;
            }
            return life > 0;
        }

        public override void die()
        {
            base.die();
        }

        public override void update()
        {
            base.update();

            position += new Vector3(0, -SPEED, 0) * SB.dt;

            nextAttackTimer -= SB.dt;

            switch(state)
            {
                case tWatermelonState.IdleProt:
                    // turn to idle position
                    if (nextAttackTimer < 0.8f)
                    {
                        // prepare move
                        playAction("FromIdleProtToIdle");
                        state = tWatermelonState.ReadyToAttack;
                    }
                    break;
                case tWatermelonState.ReadyToAttack:
                    if (nextAttackTimer < 0.0f)
                    {
                        // prepare move
                        playAction("attack");
                        state = tWatermelonState.ReadyToIdle;
                        throwProjectiles();
                    }
                    break;
                case tWatermelonState.ReadyToIdle:
                    if (nextAttackTimer < -vulnerableTime)
                    {
                        // prepare move
                        playAction("FromIdleToIdleProt");
                        state = tWatermelonState.IdleProt;
                        vulnerableTime = Calc.randomScalar(2.0f, 3.0f);
                        nextAttackTimer = Calc.randomScalar(4.0f, 5.0f);
                    }
                    break;
            }
        }

        void throwProjectiles()
        {
            for (int i = 0; i < NUMBER_OF_PROJECTILES; ++i)
            {
                float attackOrientation = Calc.randomAngle(MIN_SHOT_ANGLE, MAX_SHOT_ANGLE);
                Projectile p = new WatermelonProjectile(position, attackOrientation, Calc.angleToDirection(attackOrientation), Calc.randomScalar(200.0f, 350.0f));
                ProjectileManager.Instance.addProjectile(p);
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
