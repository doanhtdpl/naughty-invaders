﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Pear : Enemy
    {
        public enum tPearState { Moving, SecondAttack, ThirdAttack }

        const float SPEED = 10.0f;
        const float LATERAL_SPEED = 20.0f;
        const float FIRST_SHOT_ANGLE = Calc.ThreePiOver2 - 0.6f;
        const float SECOND_SHOT_ANGLE = Calc.ThreePiOver2;
        const float THIRD_SHOT_ANGLE = Calc.ThreePiOver2 + 0.6f;

        float nextAttackTimer;
        float nextMoveTimer;
        float currentMoveTimer;
        bool moveRight;
        tPearState state;

        public Pear(Vector3 position, float orientation)
            : base("pear", position, orientation)
        {
            life = 100.0f;
            nextAttackTimer = Calc.randomScalar(4.0f, 5.5f);
            nextMoveTimer = Calc.randomScalar(0.5f, 1.5f);
            currentMoveTimer = Calc.randomScalar(2.0f, 3.0f);
            moveRight = Calc.randomScalar() < 0.5f;
            setCollisions();
            state = tPearState.Moving;
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

        public override void update()
        {
            base.update();

            // always move down
            position += new Vector3(0, -SPEED, 0) * SB.dt;

            nextAttackTimer -= SB.dt;
            nextMoveTimer -= SB.dt;
            currentMoveTimer -= SB.dt;

            // prepare next move
            if (nextMoveTimer < 0)
            {
                nextMoveTimer = Calc.randomScalar(0.5f, 1.5f);
                currentMoveTimer = Calc.randomScalar(2.0f, 3.0f);
                moveRight = Calc.randomScalar() < 0.5f;
            }

            switch (state)
            {
                case tPearState.Moving:
                    if (currentMoveTimer > 0.0f)
                    {
                        Vector2 nextPosition = moveRight ? position2D + new Vector2(+LATERAL_SPEED, 0.0f) * SB.dt : position2D + new Vector2(-LATERAL_SPEED, 0.0f) * SB.dt;
                        GameplayHelper.Instance.updateEntityPosition(this, nextPosition, LevelManager.Instance.getLevelCollisions());
                    }

                    if (nextAttackTimer < 0.6f)
                    {
                        playAction("attack");
                        Vector2 direction = Calc.angleToDirection( FIRST_SHOT_ANGLE );
                        Projectile p = new PearProjectile(position, direction);
                        ProjectileManager.Instance.addProjectile(p);
                        state = tPearState.SecondAttack;
                    }
                break;
                case tPearState.SecondAttack:
                    if (nextAttackTimer < 0.55f)
                    {
                        Vector2 direction = Calc.angleToDirection(SECOND_SHOT_ANGLE);
                        Projectile p = new PearProjectile(position, direction);
                        ProjectileManager.Instance.addProjectile(p);
                        state = tPearState.ThirdAttack;
                    }
                break;
                case tPearState.ThirdAttack:
                    if (nextAttackTimer < 0.5f)
                    {
                        Vector2 direction = Calc.angleToDirection(THIRD_SHOT_ANGLE);
                        Projectile p = new PearProjectile(position, direction);
                        ProjectileManager.Instance.addProjectile(p);
                        nextAttackTimer = Calc.randomScalar(3.5f, 4.5f);
                        state = tPearState.Moving;
                    }
                break;
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}