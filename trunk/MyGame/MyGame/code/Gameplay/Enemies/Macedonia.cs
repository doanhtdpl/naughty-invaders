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
        enum tMacedoniaBossState { Init, Hidden, Appear, Disappear, Idle, RayoPrepare, RayoAttack, Shake, Talk, Die, Laugh }

        tMacedoniaBossState state;

        //rayo
        float RAYO_DURATION = 1.5f;
        int RAYO_LIMIT_X = 400;

        int TELEPORT_LIMIT_X = 450;

        //Appear/disappear
        float HIDDEN_TIME = 1.0f;
        float APPEAR_TIME = 0.2f;
        float DISAPPEAR_TIME = 0.2f;

        //Laugh
        float LAUGH_TIME = 2.0f;

        //Talk/Invoke enemies
        int INVOKE_NUM_ORANGES = 20;
        float INVOKE_TIME_BETWEEN_ORANGES = 0.2f;
        int INVOKE_LIMIT_X = 350;
        float INVOKE_TIME = 4.0f;

        //Shake
        float SHAKE_MIN_TIME_FRUIT = 0.2f;
        float SHAKE_MAX_TIME_FRUIT = 0.3f;

        float SHAKE_TIME = 3.0f;
        float SHAKE_TIMES = 3;

        bool visible = true;
        float stateTime, idleTime, appearX, shakeNextFruitTime, shakeCount;
        int numHits, rayoInitLives, invokeCount;
        Vector3 initPos, positionRayoTo, positionRayoFrom, invokeFrom, invokeStep;
        bool invokeRight;

        List<RenderableEntity2D> fruits = new List<RenderableEntity2D>();

        float timeToAttack;
        tMacedoniaBossState nextAttack;

        public Macedonia(Vector3 position, float orientation)
            : base("macedonia", position, orientation, 4)
        {
            life = 1000.0f;
            idleTime = 0;
            changeState(tMacedoniaBossState.Init);
            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(-5, 0), 65);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            if (state == tMacedoniaBossState.Idle)
            {
                numHits++;
                life -= ce.damage;
            }
            return life > 0;
        }

        public override void die()
        {
            base.die();
            ParticleManager.Instance.addParticles("macedonia2", position, Vector3.Zero, Color.White);
            ParticleManager.Instance.addParticles("macedonia2", position, Vector3.Zero, Color.White);
            ParticleManager.Instance.addParticles("macedonia2", position, Vector3.Zero, Color.White);
        }


        //**************************************************
        // Update State
        public override void update()
        {
            base.update();

            stateTime += SB.dt;
            switch (state)
            {
                case tMacedoniaBossState.Init:
                    initPos = position;
                    nextAttack = tMacedoniaBossState.Idle;
                    changeState(tMacedoniaBossState.Hidden);

                    appearX = 0;
                    break;

                case tMacedoniaBossState.Hidden:
                    if (stateTime > HIDDEN_TIME)
                    {
                        initPos = new Vector3(appearX, initPos.Y, initPos.Z);
                        position = initPos;
                        idleTime = 0;

                        changeState(tMacedoniaBossState.Appear);
                    }

                    break;

                case tMacedoniaBossState.Appear:
                    if (stateTime > APPEAR_TIME && !visible)
                    {
                        visible = true;
                        changeState(nextAttack);
                    }

                    break;

                case tMacedoniaBossState.Disappear:
                    if (stateTime > DISAPPEAR_TIME && visible)
                    {
                        visible = false;
                        changeState(tMacedoniaBossState.Hidden);
                    }
                    break;

                case tMacedoniaBossState.Idle:
                    idleTime += SB.dt;
                    timeToAttack -= SB.dt;

                    if (timeToAttack < 0)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.A) || nextAttack == tMacedoniaBossState.RayoPrepare)
                        {
                            nextAttack = tMacedoniaBossState.RayoPrepare;
                            appearX = Calc.randomBool() ? -RAYO_LIMIT_X : RAYO_LIMIT_X;
                            changeState(tMacedoniaBossState.Disappear);
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.S) || nextAttack == tMacedoniaBossState.Shake)
                        {
                            shakeCount = 0;
                            nextAttack = tMacedoniaBossState.Shake;
                            changeState(nextAttack);
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.D) || nextAttack == tMacedoniaBossState.Talk)
                        {
                            nextAttack = tMacedoniaBossState.Talk;
                            changeState(nextAttack);
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.F) || nextAttack == tMacedoniaBossState.Laugh)
                        {
                            nextAttack = tMacedoniaBossState.Laugh;
                            changeState(nextAttack);
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.G) || nextAttack == tMacedoniaBossState.Disappear)
                        {
                            appearX = Calc.randomNatural(-TELEPORT_LIMIT_X, TELEPORT_LIMIT_X);
                            nextAttack = tMacedoniaBossState.Idle;
                            changeState(tMacedoniaBossState.Disappear);
                        }
                    }

                    //Idle move
                    float angle = (float)(idleTime * 1000 * (Math.PI / 180));
                    position = initPos + new Vector3(100 * (float)Math.Sin(angle / 10), 30 * (float)Math.Cos(angle/ 7), 0.0f);

                    break;

                case tMacedoniaBossState.RayoPrepare:
                    if (getCurrentAction() != "attack")
                    {
                        changeState(tMacedoniaBossState.RayoAttack);
                    }
                    break;

                case tMacedoniaBossState.RayoAttack:
                    float perc = stateTime / RAYO_DURATION;
                    position = positionRayoFrom + (positionRayoTo - positionRayoFrom) * perc;
                    if (stateTime > RAYO_DURATION)
                    {
                        if (rayoInitLives > GamerManager.getMainPlayer().lifes)
                            nextAttack = tMacedoniaBossState.Laugh;
                        else
                            nextAttack = tMacedoniaBossState.Idle;

                        appearX = Calc.randomNatural(-TELEPORT_LIMIT_X, TELEPORT_LIMIT_X);
                        changeState(tMacedoniaBossState.Disappear);
                    }
                    break;

                case tMacedoniaBossState.Shake:
                    shakeNextFruitTime -= SB.dt;

                    if (shakeNextFruitTime <= 0)
                    {
                        fruits.Add(new MacedoniaFruit("minifruits-1" + Calc.randomNatural(1, 9), position + new Vector3(0, 50, 0)));
                        shakeNextFruitTime = Calc.randomScalar(SHAKE_MIN_TIME_FRUIT, SHAKE_MAX_TIME_FRUIT);
                    }

                    if (stateTime > SHAKE_TIME)
                    {
                        shakeCount++;
                        if (shakeCount < SHAKE_TIMES)
                        {
                            nextAttack = tMacedoniaBossState.Shake;
                            appearX = Calc.randomNatural(-TELEPORT_LIMIT_X, TELEPORT_LIMIT_X);
                            changeState(tMacedoniaBossState.Disappear);
                        }
                        else
                        {
                            changeState(tMacedoniaBossState.Idle);
                        }
                    }
                    break;

                case tMacedoniaBossState.Talk:
                    if (stateTime > (invokeCount * INVOKE_TIME_BETWEEN_ORANGES))
                    {
                        EnemyManager.Instance.addEnemy("orange", invokeFrom + invokeStep * invokeCount);
                        invokeCount++;
                    }

                    if (stateTime > INVOKE_TIME)
                        changeState(tMacedoniaBossState.Laugh);
                    break;

                case tMacedoniaBossState.Laugh:
                    if (stateTime > LAUGH_TIME)
                        changeState(tMacedoniaBossState.Idle);
                    break;
                case tMacedoniaBossState.Die:
                    break;
            }

            int deadCount = 0;
            foreach (MacedoniaFruit fruit in fruits)
            {
                fruit.update();

                if (!fruit.isDead() && !fruit.isDying() && (fruit.position - GamerManager.getMainPlayer().position).Length() < 50)
                {
                    GamerManager.getMainPlayer().gotHitAtPart(null, 0);
                    fruit.explode();
                }

                if (fruit.isDead())
                    deadCount++;
            }

            if (fruits.Count > 0 && deadCount == fruits.Count)
                fruits.Clear();
        }


        //**************************************************
        // Change State
        private void changeState(tMacedoniaBossState newState)
        {
            switch (newState)
            {
                case tMacedoniaBossState.Hidden:
                    visible = false;
                    break;

                case tMacedoniaBossState.Disappear:
                    ParticleManager.Instance.addParticles("macedonia2", position + new Vector3(0, -50, 10), Vector3.Zero, Color.White);
                    break;

                case tMacedoniaBossState.Appear:
                    ParticleManager.Instance.addParticles("macedonia2", position + new Vector3(0, -50, 10), Vector3.Zero, Color.White);
                    break;

                case tMacedoniaBossState.Idle:
                    playAction("idle");

                    numHits = 0;

                    float rand = Calc.randomScalar();
                    if (rand <= 0.2)
                        nextAttack = tMacedoniaBossState.RayoPrepare;
                    else if (rand <= 0.4)
                        nextAttack = tMacedoniaBossState.Shake;
                    else if (rand <= 0.6)
                        nextAttack = tMacedoniaBossState.Talk;
                    else if (rand <= 0.8)
                        nextAttack = tMacedoniaBossState.Disappear;
                    else
                        nextAttack = tMacedoniaBossState.Laugh;

                    timeToAttack = Calc.randomScalar(1.0f, 2.0f);

                    break;

                case tMacedoniaBossState.RayoPrepare:
                    playAction("attack");
                    rayoInitLives = GamerManager.getMainPlayer().lifes;
                    break;

                case tMacedoniaBossState.RayoAttack:
                    positionRayoFrom = position;
                    if (position.X > 0)
                        positionRayoTo = new Vector3(-500, position.Y, position.Z);
                    else
                        positionRayoTo = new Vector3(500, position.Y, position.Z);

                    break;

                case tMacedoniaBossState.Shake:
                    playAction("attackShake");
                    break;

                case tMacedoniaBossState.Talk:
                    invokeCount = 0;
                    invokeRight = Calc.randomBool();
                    invokeFrom = new Vector3(invokeRight ? -INVOKE_LIMIT_X : INVOKE_LIMIT_X, position.Y, position.Z);
                    invokeStep = new Vector3((invokeRight ? INVOKE_LIMIT_X * 2 : -INVOKE_LIMIT_X * 2) / INVOKE_NUM_ORANGES, 0, 0);

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
            if(visible)
                base.render();

            foreach (RenderableEntity2D fruit in fruits)
            {
                fruit.render();
            }
        }
    }
}
