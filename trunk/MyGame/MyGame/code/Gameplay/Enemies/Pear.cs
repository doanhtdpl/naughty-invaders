using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Pear : Enemy
    {
        public enum tPearState { Moving, SecondAttack, ThirdAttack }

        const float SPEED = 20.0f;
        float nextAttackTimer;
        tPearState state;

        public Pear(Vector3 position, float orientation)
            : base("pear", position, orientation)
        {
            life = 50.0f;
            nextAttackTimer = Calc.randomScalar(3.0f, 3.5f);
            setCollisions();
            state = tPearState.Moving;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 20), 40);
        }

        public override bool gotHitAtPart(int partIndex, float damage)
        {
            // if gets hit while attacking, reset the attack
            if (state != tPearState.Moving)
            {
                state = tPearState.Moving;
                nextAttackTimer = Calc.randomScalar(1.0f, 3.0f);
            }
            else // if isnt attacking, delay the next attack
            {
                nextAttackTimer += 0.3f;
            }
            //playAction("");
            life -= damage;
            return life > 0;
        }

        public override void update()
        {
            base.update();

            // always move down
            position += new Vector3(0, -SPEED, 0) * SB.dt;

            nextAttackTimer -= SB.dt;

            switch (state)
            {
                case tPearState.Moving:
                    if (nextAttackTimer < 0.6f)
                    {
                        playAction("attack");
                        Vector2 direction = Calc.angleToDirection( MathHelper.ToRadians(225.0f) );
                        Projectile p = new PearProjectile(position, direction);
                        ProjectileManager.Instance.addProjectile(p);
                        nextAttackTimer = Calc.randomScalar(1.0f, 3.0f);
                        state = tPearState.SecondAttack;
                    }
                break;
                case tPearState.SecondAttack:
                    if (nextAttackTimer < 0.4f)
                    {
                        Vector2 direction = Calc.angleToDirection(MathHelper.ToRadians(270.0f));
                        Projectile p = new PearProjectile(position, direction);
                        ProjectileManager.Instance.addProjectile(p);
                        state = tPearState.ThirdAttack;
                    }
                break;
                case tPearState.ThirdAttack:
                    if (nextAttackTimer < 0.2f)
                    {
                        Vector2 direction = Calc.angleToDirection(MathHelper.ToRadians(315.0f));
                        Projectile p = new PearProjectile(position, direction);
                        ProjectileManager.Instance.addProjectile(p);
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
