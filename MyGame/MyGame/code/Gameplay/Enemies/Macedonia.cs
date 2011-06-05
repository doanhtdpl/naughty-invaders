using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class Macedonia : Enemy
    {
        enum tMacedoniaBossState { Idle, AttackRayo, AttackShake, AttackTalk, Die, Laugh }

        tMacedoniaBossState state;

        float stateTime;

        float timeToAttack;
        tMacedoniaBossState nextAttack;

        public Macedonia(Vector3 position, float orientation)
            : base("macedonia", position, orientation, 4)
        {
            life = 1000.0f;

            changeState(tMacedoniaBossState.Idle);
            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(20, 20), 40);
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

            stateTime += SB.dt;

            switch (state)
            {
                case tMacedoniaBossState.Idle:
                    timeToAttack -= SB.dt;
                    if (timeToAttack <= 0)
                    {
                        changeState(nextAttack);
                    }
                    break;
                case tMacedoniaBossState.AttackRayo:
                    if (stateTime > 2.0)
                        changeState(tMacedoniaBossState.Idle);
                    break;
                case tMacedoniaBossState.AttackShake:
                    if (stateTime > 2.0)
                        changeState(tMacedoniaBossState.Idle);
                    break;
                case tMacedoniaBossState.AttackTalk:
                    if (stateTime > 2.0)
                        changeState(tMacedoniaBossState.Idle);
                    break;
                case tMacedoniaBossState.Laugh:
                    if (stateTime > 2.0)
                        changeState(tMacedoniaBossState.Idle);
                    break;
                case tMacedoniaBossState.Die:
                    break;
            }
        }

        private void changeState(tMacedoniaBossState newState)
        {
            switch (newState)
            {
                case tMacedoniaBossState.Idle:
                    playAction("idle");

                    float rand = Calc.randomScalar();
                    
                    if (Keyboard.GetState().IsKeyDown(Keys.A)) // (rand <= 0.33)
                        nextAttack = tMacedoniaBossState.AttackRayo;
                    else if (Keyboard.GetState().IsKeyDown(Keys.S)) // (rand <= 0.66)
                        nextAttack = tMacedoniaBossState.AttackShake;
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                        nextAttack = tMacedoniaBossState.AttackTalk;

                    timeToAttack = Calc.randomScalar(1.0f, 2.0f);

                    break;

                case tMacedoniaBossState.AttackRayo:
                    playAction("attack");
                    break;

                case tMacedoniaBossState.AttackShake:
                    playAction("attackShake");
                    break;

                case tMacedoniaBossState.AttackTalk:
                    playAction("attackIdle");
                    break;

                case tMacedoniaBossState.Laugh:
                    playAction("laugh");
                    break;

                case tMacedoniaBossState.Die:
                    break;
            }

            stateTime = 0.0f;
            state = newState;
        }

        public override void render()
        {
            base.render();
        }
    }
}
