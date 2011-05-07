using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Grape : Enemy
    {
        const float SPEED = 30.0f;
        const float LATERAL_SPEED = 800.0f;
        const float MIN_SHOT_ANGLE = -Calc.PiOver2 - 0.6f;
        const float MAX_SHOT_ANGLE = -Calc.PiOver2 + 0.6f;

        bool moveRight;
        bool moveStarted;
        bool moveEnded;
        float afterAttackTimer;
        float nextMoveTimer;
        float movingTimer;
        float nextAttackTimer;

        public Grape(Vector3 position, float orientation)
            : base("grape", position, orientation)
        {
            life = 40.0f;
             
            nextMoveTimer = Calc.randomScalar(1.0f, 2.0f);
            nextAttackTimer = Calc.randomScalar(2.0f, 2.5f);

            afterAttackTimer = 999999.0f;

            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 20), 40);
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
            position += new Vector3(0, -SPEED, 0) * SB.dt;

            nextMoveTimer -= SB.dt;
            nextAttackTimer -= SB.dt;
            movingTimer -= SB.dt;
            afterAttackTimer -= SB.dt;

            // next move
            if (nextMoveTimer < 0)
            {
                // prepare move
                movingTimer = Calc.randomScalar(0.1f, 0.5f);
                // grape moves to the player's X position except 5% of time, that moves opposite
                if (Calc.randomScalar() > 0.05f)
                {
                    moveRight = position2D.X < GamerManager.getSessionOwner().Player.position2D.X;
                }
                else
                {
                    moveRight = position2D.X > GamerManager.getSessionOwner().Player.position2D.X;
                }
                // prepare next move
                nextMoveTimer = Calc.randomScalar(2.0f, 4.0f);
                moveStarted = false;
                moveEnded = false;
            }
            
            // update position
            if (movingTimer > 0)
            {
                this.orientation = 0.0f;

                if (moveRight)
                {
                    if (!moveStarted)
                    {
                        playAction("dashLeftStart");
                        moveStarted = true;
                    }
                    GameplayHelper.Instance.updateEntityPosition(this,
                        position2D + (new Vector2(LATERAL_SPEED, 0.0f) * SB.dt),
                        LevelManager.Instance.getLevelCollisions(), false);
                }
                else
                {
                    if (!moveStarted)
                    {
                        playAction("dashRightStart");
                        moveStarted = true;
                    }
                    GameplayHelper.Instance.updateEntityPosition(this,
                        position2D + (new Vector2(-LATERAL_SPEED, 0.0f) * SB.dt),
                        LevelManager.Instance.getLevelCollisions(), false);
                }
            }
            else if (!moveEnded)
            {
                playAction("idle");
                moveEnded = true;
            }

            // next attack
            if (movingTimer < 0 && nextAttackTimer < 0)
            {
                playAction("attack");
                float attackOrientation = Calc.directionToAngle(GamerManager.getSessionOwner().Player.position2D - position2D);
                attackOrientation += Calc.randomAngle(-0.3f, +0.3f);
                // clamp the angle to the grape's shot cone
                attackOrientation = Calc.clampAngle(attackOrientation, MIN_SHOT_ANGLE, MAX_SHOT_ANGLE);
                orientation = attackOrientation + Calc.PiOver2;
                Projectile p = new GrapeProjectile(position, attackOrientation + Calc.PiOver2, Calc.angleToDirection(attackOrientation));
                ProjectileManager.Instance.addProjectile(p);
                nextAttackTimer = Calc.randomScalar(1.0f, 3.0f);
                afterAttackTimer = 0.2f;
            }

            // to get back to orientation 0
            if (afterAttackTimer < 0.0f)
            {
                afterAttackTimer = 999999.0f;
                orientation = 0.0f;
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
