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
        enum tMacedoniaBossState { Init, Cinematic, Hidden, Appear, Disappear, Idle, RayoPrepare, RayoAttack, Shake, Talk, Die, Laugh }

        tMacedoniaBossState state;

        float LIFE = 1500;

        //rayo
        float RAYO_DURATION = 1.5f;
        int RAYO_LIMIT_X = 400;

        int TELEPORT_LIMIT_X = 450;
        const int MAX_RAYOS = 3;

        int RAYO_ATTACK_WIDTH = 100;

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

        float SHAKE_TIME_MIN = 3.0f;
        float SHAKE_TIME_MAX = 3.0f;
        int SHAKE_TIMES_MIN = 1;
        int SHAKE_TIMES_MAX = 3;


        bool visible = true;
        float stateTime, idleTime, appearX, shakeNextFruitTime, shakeCount, shakeTime;
        int numHits, rayoInitLives, invokeCount, shakeTimes;
        Vector3 initPos, positionRayoTo, positionRayoFrom, invokeFrom, invokeStep;
        bool invokeRight;

        AnimatedEntity2D[] rayo = new AnimatedEntity2D[MAX_RAYOS];
        AnimatedEntity2D rayoEnd;

        List<RenderableEntity2D> fruits = new List<RenderableEntity2D>();

        float timeToAttack;
        tMacedoniaBossState nextAttack;

        Lifebar lifebar;

        public Macedonia(Vector3 position, float orientation)
            : base("macedonia", position, orientation, 4)
        {
            life = LIFE;
            idleTime = 0;
            changeState(tMacedoniaBossState.Init);
            setCollisions();

            lifebar = new Lifebar("macedonia", this, new Vector2(0.6f, 0.6f), new Vector2(0.0f, 140.0f), Color.Green);

            for(int i=0; i<MAX_RAYOS; i++)
            {
                rayo[i] = new AnimatedEntity2D("animatedProps", "macedoniaWeaponBody", Vector3.Zero, 0, Color.White);
            }
            rayoEnd = new AnimatedEntity2D("animatedProps", "macedoniaWeaponEnd", Vector3.Zero, 0, Color.White);

            showRayo(false);

            loadIntroCinematic();
        }

        void loadIntroCinematic()
        {
            Cinematic cinematic = new Cinematic();

            DialogEvent de1 = new DialogEvent(tDialogCharacter.Macedonia, "Te voy a machacar mierda seca!");
            DialogEvent de2 = new DialogEvent(tDialogCharacter.Wish, "A que te meto...");

            cinematic.events.Add((CinematicEvent)de1);
            cinematic.events.Add((CinematicEvent)de2);

            CinematicManager.Instance.addCinematic("macedoniaIntro", cinematic);
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
            entityState = tEntityState.Dying;
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
                    //stay until the camera stops
                    if (CameraManager.Instance.isIdle())
                    {
                        initPos = position;
                        nextAttack = tMacedoniaBossState.Cinematic;
                        changeState(tMacedoniaBossState.Hidden);

                        appearX = 0;
                    }
                    break;

                case tMacedoniaBossState.Cinematic:
                    if (CinematicManager.Instance.cinematicToPlay == null)
                    {
                        nextAttack = tMacedoniaBossState.Idle;
                        changeState(tMacedoniaBossState.Disappear);
                    }
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

                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        nextAttack = tMacedoniaBossState.RayoPrepare;
                        timeToAttack = 0;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        nextAttack = tMacedoniaBossState.Shake;
                        timeToAttack = 0;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        nextAttack = tMacedoniaBossState.Talk;
                        timeToAttack = 0;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.F))
                    {
                        nextAttack = tMacedoniaBossState.Laugh;
                        timeToAttack = 0;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.G))
                    {
                        nextAttack = tMacedoniaBossState.Disappear;
                        timeToAttack = 0;
                    }

                    if (timeToAttack < 0)
                    {
                        if (nextAttack == tMacedoniaBossState.RayoPrepare)
                        {
                            appearX = Calc.randomBool() ? -RAYO_LIMIT_X : RAYO_LIMIT_X;
                            changeState(tMacedoniaBossState.Disappear);
                        }
                        else if (nextAttack == tMacedoniaBossState.Shake)
                        {
                            shakeCount = 0;
                            changeState(nextAttack);
                        }
                        else if (nextAttack == tMacedoniaBossState.Talk)
                        {
                            changeState(nextAttack);
                        }
                        else if (nextAttack == tMacedoniaBossState.Laugh)
                        {
                            changeState(nextAttack);
                        }
                        else if (nextAttack == tMacedoniaBossState.Disappear)
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
                    updateRayo();

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

                        showRayo(false);
                    }
                    break;

                case tMacedoniaBossState.Shake:
                    shakeNextFruitTime -= SB.dt;

                    if (shakeNextFruitTime <= 0)
                    {
                        fruits.Add(new MacedoniaFruit("minifruits-1" + Calc.randomNatural(1, 9), position + new Vector3(0, 50, 0)));
                        shakeNextFruitTime = Calc.randomScalar(SHAKE_MIN_TIME_FRUIT, SHAKE_MAX_TIME_FRUIT);
                    }

                    if (stateTime > shakeTime)
                    {
                        shakeCount++;
                        if (shakeCount < shakeTimes)
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

                case tMacedoniaBossState.Cinematic:
                    CinematicManager.Instance.playCinematic("macedoniaIntro");
                    break;

                case tMacedoniaBossState.Disappear:
                    playAction("idle");
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

                    timeToAttack = Calc.randomScalar(0.5f, 1.0f);

                    break;

                case tMacedoniaBossState.RayoPrepare:
                    playAction("attack");
                    rayoInitLives = GamerManager.getMainPlayer().lifes;
                    break;

                case tMacedoniaBossState.RayoAttack:
                    updateRayo();
                    showRayo(true);
                    positionRayoFrom = position;
                    if (position.X > 0)
                        positionRayoTo = new Vector3(-500, position.Y, position.Z);
                    else
                        positionRayoTo = new Vector3(500, position.Y, position.Z);

                    break;

                case tMacedoniaBossState.Shake:
                    shakeTime = Calc.randomScalar(SHAKE_TIME_MIN, SHAKE_TIME_MAX);
                    shakeTimes = Calc.randomNatural(SHAKE_TIMES_MIN, SHAKE_TIMES_MAX);

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

        public override bool collidesWith(CollidableEntity2D ce, ref bool entityAlive)
        {
            if (state == tMacedoniaBossState.Hidden || state == tMacedoniaBossState.Disappear || state == tMacedoniaBossState.Appear)
            {
                return false;
            }

            if (state == tMacedoniaBossState.RayoAttack && Math.Abs(position.X - GamerManager.getMainPlayer().position.X) < RAYO_ATTACK_WIDTH && position.Y > GamerManager.getMainPlayer().position.Y)
            {
                return true;
            }

            return base.collidesWith(ce, ref entityAlive);
        }

        public void showRayo(bool value)
        {
            for (int i = 0; i < MAX_RAYOS; i++)
            {
                rayo[i].renderState = value ? tRenderState.Render : tRenderState.NoRender;
            }

            if(!value)
                rayoEnd.renderState = tRenderState.NoRender;
        }

        public void updateRayo()
        {
            Vector3 playerPos = GamerManager.getMainPlayer().position;

            bool isPlayerHit = Math.Abs(position.X - GamerManager.getMainPlayer().position.X) < RAYO_ATTACK_WIDTH;

            float length = Math.Abs(position.Y - playerPos.Y - 100) - 15;
            if (!isPlayerHit)
                length = 5000;

            for (int i = 0; i < MAX_RAYOS; i++)
            {
                rayo[i].renderState = tRenderState.NoRender;
            }

            for (int i = 0; i < MAX_RAYOS; i++)
            {
                if (length < 0)
                    break; 

                rayo[i].renderState = tRenderState.Render;
                    
                rayo[i].update();
                float factor = 1.0f;
                if (length < rayo[i].getFrameSize().Y)
                    factor = length / rayo[i].getFrameSize().Y;

                rayo[i].paintMask = new Vector2(1.0f, factor);

                rayo[i].position = new Vector3(position.X, position.Y - i * rayo[i].getFrameSize().Y - rayo[i].getFrameSize().Y * 0.5f * factor - 52 , position.Z + 0.1f);
                length -= rayo[i].getFrameSize().Y;
            }

            if (isPlayerHit)
            {
                rayoEnd.renderState = tRenderState.Render;
                rayoEnd.update();
                rayoEnd.position = new Vector3(position.X, GamerManager.getMainPlayer().position.Y + 100, position.Z + 0.2f);
            }
            else
            {
                rayoEnd.renderState = tRenderState.NoRender;
            }
        }

        public override void render()
        {
            if (visible)
            {
                base.render();

                lifebar.lifePercentage = life / LIFE;
                GraphicsManager.Instance.spriteBatchBegin();
                lifebar.render();
                GraphicsManager.Instance.spriteBatchEnd();
            }

            foreach (RenderableEntity2D fruit in fruits)
            {
                fruit.render();
            }
        }
    }
}
