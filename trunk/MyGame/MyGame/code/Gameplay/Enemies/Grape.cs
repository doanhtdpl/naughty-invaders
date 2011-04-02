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
        float nextMoveTimer;
        float movingTimer;
        float nextAttackTimer;

        public Grape(Vector3 position, float orientation)
            : base("grape", position, orientation)
        {
            life = 40.0f;

            nextMoveTimer = Calc.randomScalar(1.0f, 2.0f);
            nextAttackTimer = Calc.randomScalar(2.0f, 2.5f);

            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 20), 40);
        }

        public override bool gotHitAtPart(int partIndex, float damage)
        {
            life -= damage;
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
            }
            // next attack
            if (nextAttackTimer < 0)
            {
                playAction("attack");
                float orientation = Calc.directionToAngle(GamerManager.getSessionOwner().Player.position2D - position2D);
                orientation += Calc.randomAngle(-0.3f, +0.3f);
                // clamp the angle to the grape's shot cone
                orientation = Calc.clampAngle(orientation, MIN_SHOT_ANGLE, MAX_SHOT_ANGLE);
                Projectile p = new GrapeProjectile(position, orientation + Calc.PiOver2, Calc.angleToDirection(orientation));
                ProjectileManager.Instance.addProjectile(p);
                nextAttackTimer = Calc.randomScalar(1.0f, 3.0f);
            }

            // update position
            if (movingTimer > 0)
            {
                if (moveRight)
                {
                    GameplayHelper.Instance.updateEntityPosition(this,
                        position2D + (new Vector2(LATERAL_SPEED, 0.0f) * SB.dt),
                        LevelManager.Instance.getLevelCollisions(), false);
                }
                else
                {
                    GameplayHelper.Instance.updateEntityPosition(this,
                        position2D + (new Vector2(-LATERAL_SPEED, 0.0f) * SB.dt),
                        LevelManager.Instance.getLevelCollisions(), false);
                }
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
