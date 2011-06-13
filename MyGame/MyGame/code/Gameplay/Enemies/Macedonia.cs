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
        float RAYO_PREPARE_DURATION = 1.2f;
        float RAYO_DURATION = 1.8f;
        int RAYO_LIMIT_X = 650;

        float RAYO_PREPARE_EFFECT_STEP = 0.05f;
        float RAYO_ATTACK_EFFECT_STEP = 0.1f;

        int TELEPORT_LIMIT_X = 600;
        const int MAX_RAYOS = 3;

        int RAYO_ATTACK_WIDTH = 120;

        //Appear/disappear
        float HIDDEN_TIME = 1.0f;
        float APPEAR_TIME = 0.2f;
        float DISAPPEAR_TIME = 0.2f;

        //Laugh
        float LAUGH_TIME = 2.0f;

        //Talk/Invoke enemies
        int INVOKE_NUM_ORANGES = 5;
        float INVOKE_TIME_BETWEEN_ORANGES = 0.4f;
        int INVOKE_LIMIT_X = 500;
        float INVOKE_TIME = 2.0f;

        //Shake
        float SHAKE_MIN_TIME_FRUIT = 0.20f;
        float SHAKE_MAX_TIME_FRUIT = 0.25f;

        float SHAKE_TIME_MIN = 1.0f;
        float SHAKE_TIME_MAX = 1.5f;
        int SHAKE_TIMES_MIN = 1;
        int SHAKE_TIMES_MAX = 3;


        bool visible = true;
        float stateTime, idleTime, appearX, shakeNextFruitTime, shakeCount, shakeTime, effectStep;
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

            lifebar = new Lifebar("macedonia", this, new Vector2(0.6f, 0.6f), new Vector2(0.0f, 170.0f), Color.White);

            for(int i=0; i<MAX_RAYOS; i++)
            {
                rayo[i] = new AnimatedEntity2D("animatedProps", "macedoniaWeaponBody", Vector3.Zero, 0, Color.White);
                //rayo[i].scale = new Vector3(rayo[i].scale.X * 2, rayo[i].scale.Y, rayo[i].scale.Z);
            }
            rayoEnd = new AnimatedEntity2D("animatedProps", "macedoniaWeaponEnd", Vector3.Zero, 0, Color.White);
            //rayoEnd.scale = new Vector3(rayoEnd.scale.X * 2, rayoEnd.scale.Y, rayoEnd.scale.Z);

            showRayo(false);

            visible = false;

            //scale *= 2;

            loadIntroCinematic();
            //loadEndCinematic();
        }

        void loadIntroCinematic()
        {
            Cinematic cinematic = new Cinematic();

            ActorEvent ae1 = new ActorEvent(this);
            ae1.setActionToPlay("talk");
            DialogEvent de1 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogBossMacedoniaIntro1.Translate());
            ActorEvent ae2 = new ActorEvent(this);
            ae2.setActionToPlay("idle");
            DialogEvent de2 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogBossMacedoniaIntro2.Translate());

            cinematic.events.Add(ae1);
            cinematic.events.Add(de1);
            cinematic.events.Add(ae2);
            cinematic.events.Add(de2);

            CinematicManager.Instance.addCinematic("macedoniaIntro", cinematic);
        }

        void loadEndCinematic()
        {
            Cinematic cinematic = new Cinematic();

            ActorEvent ae2 = new ActorEvent(this);
            ae2.setActionToPlay("talk");
            DialogEvent de1 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogBossMacedoniaOutro1.Translate());
            ActorEvent ae3 = new ActorEvent(this);
            ae3.setActionToPlay("idle");
            DialogEvent de2 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogBossMacedoniaOutro2.Translate());
            ActorEvent ae1 = new ActorEvent(this);
            ae1.setActionToPlay("talk");
            DialogEvent de3 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogBossMacedoniaOutro3.Translate());
            ActorEvent ae4 = new ActorEvent(this);
            ae4.setActionToPlay("idle");
            SpecialEvent se1 = new SpecialEvent(this);
            se1.setPlayEffect("macedoniaAppear", position + new Vector3(0, -100, 5), Vector3.Zero, Color.White, 2);
            ActorEvent ae5 = new ActorEvent(this);
            ae5.setRender(false);

            cinematic.events.Add(ae2);
            cinematic.events.Add(de1);
            cinematic.events.Add(ae3);
            cinematic.events.Add(de2);
            cinematic.events.Add(ae1);
            cinematic.events.Add(de3);
            cinematic.events.Add(ae4);
            cinematic.events.Add(se1);
            cinematic.events.Add(ae5);

            CinematicManager.Instance.addCinematic("macedoniaEnd", cinematic);
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(-10, 0), 80);
        }

        public void removeCollisions()
        {
            parts.Clear();
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            if (visible && state != tMacedoniaBossState.Die)
            {
                numHits++;
                life -= ce.damage;

                if (life < 0)
                {
                    changeState(tMacedoniaBossState.Die);
                    SoundManager.Instance.playEffect(entityName + "Dies");
                }

                life = Math.Max(life, 1);
            }

            ParticleManager.Instance.addParticles(entityName + "GotHit", this.position + new Vector3(0, -120, 5), Vector3.Zero, Color.White);
            if (life > 0)
            {
                SoundManager.Instance.playEffect(entityName + "GotHit");
            }
            else
            {
                //SoundManager.Instance.playEffect(entityName + "Dies");
            }
            return life > 0;
        }

        public override void die()
        {
            //base.die();
            //entityState = tEntityState.Dying;
            changeState(tMacedoniaBossState.Die);
        }


        public void updateIdleMove()
        {
            idleTime += SB.dt;

            //Idle move
            float angle = (float)(idleTime * 1000 * (Math.PI / 180));
            position = initPos + new Vector3(100 * (float)Math.Sin(angle / 10), 30 * (float)Math.Cos(angle / 7), 0.0f);
        }

        //**************************************************
        // Update State
        public override void update()
        {
            base.update();

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                changeState(tMacedoniaBossState.Die);
            }

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

                    updateIdleMove();

                    break;

                case tMacedoniaBossState.RayoPrepare:
                    effectStep -= SB.dt;
                    if (effectStep <= 0)
                    {
                        ParticleManager.Instance.addParticles("zumoPrepare", position + new Vector3(0, -170, 5), Vector3.Zero, Color.White, 0.5f);
                        effectStep = RAYO_PREPARE_EFFECT_STEP;
                    }

                    if (stateTime > RAYO_PREPARE_DURATION)
                    {
                        changeState(tMacedoniaBossState.RayoAttack);
                    }
                    break;

                case tMacedoniaBossState.RayoAttack:
                    updateRayo();

                    effectStep -= SB.dt;
                    if (effectStep <= 0)
                    {
                        ParticleManager.Instance.addParticles("zumoAttack", position + new Vector3(0, -135, 5), Vector3.Zero, Color.White);
                        effectStep = RAYO_ATTACK_EFFECT_STEP;
                    }

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
                        EnemyManager.Instance.addEnemy("orange", invokeFrom + invokeStep * invokeCount + new Vector3(0, -1000, 0));
                        invokeCount++;
                    }

                    if (stateTime > INVOKE_TIME)
                        changeState(tMacedoniaBossState.Laugh);

                    updateIdleMove();
                    break;

                case tMacedoniaBossState.Laugh:
                    if (stateTime > LAUGH_TIME)
                        changeState(tMacedoniaBossState.Idle);

                    updateIdleMove();

                    break;

                case tMacedoniaBossState.Die:
                    if (CinematicManager.Instance.cinematicToPlay == null)
                    {
                        GamerManager.getGamerEntity(0).data.levelsPassed["fruitownB"] = true;
                        StateManager.addState(StateManager.tGameState.EndStage);
                    }
                    break;
            }

            int deadCount = 0;
            foreach (MacedoniaFruit fruit in fruits)
            {
                fruit.update();

                if (!fruit.isDead() && !fruit.isDying())
                {
                    foreach(Projectile p in ProjectileManager.Instance.getProjectiles())
                    {
                        if (p.team == Projectile.tTeam.Players && (fruit.position - p.position).Length() < 80)
                        {
                            ParticleManager.Instance.addParticles("macedoniaGotHit", p.position, Vector3.Zero, Color.White);
                            p.die();
                        }
                    }
                    if ((fruit.position - GamerManager.getMainPlayer().position).Length() < 100)
                    {
                        GamerManager.getMainPlayer().gotHitAtPart(null, 0);
                        fruit.explode();
                    }
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
                    ParticleManager.Instance.addParticles("macedoniaAppear", position + new Vector3(0, -100, 5), Vector3.Zero, Color.White, 2);
                    removeCollisions();
                    break;

                case tMacedoniaBossState.Appear:
                    ParticleManager.Instance.addParticles("macedoniaAppear", position + new Vector3(0, -100, 5), Vector3.Zero, Color.White, 2);
                    setCollisions();
                    break;

                case tMacedoniaBossState.Idle:
                    playAction("idle");

                    numHits = 0;

                    float rand = Calc.randomScalar();
                    if (rand <= 0.25)
                        nextAttack = tMacedoniaBossState.RayoPrepare;
                    else if (rand <= 0.5)
                        nextAttack = tMacedoniaBossState.Shake;
                    else if (rand <= 0.7)
                        nextAttack = tMacedoniaBossState.Talk;
                    else if (rand <= 0.85)
                        nextAttack = tMacedoniaBossState.Disappear;
                    else
                        nextAttack = tMacedoniaBossState.Laugh;

                    timeToAttack = Calc.randomScalar(1.0f, 2.0f);

                    break;

                case tMacedoniaBossState.RayoPrepare:
                    playAction("rayoPrepare");
                    rayoInitLives = GamerManager.getMainPlayer().lifes;
                    break;

                case tMacedoniaBossState.RayoAttack:
                    playAction("rayoAttack");
                    updateRayo();
                    showRayo(true);
                    positionRayoFrom = position;
                    if (position.X > 0)
                        positionRayoTo = new Vector3(-RAYO_LIMIT_X, position.Y, position.Z);
                    else
                        positionRayoTo = new Vector3(RAYO_LIMIT_X, position.Y, position.Z);

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

                    //invoke watermelon
                    ParticleManager.Instance.addParticles("macedoniaAppear", position + new Vector3(0, -350, 5), Vector3.Zero, Color.White);
                    EnemyManager.Instance.addEnemy("lemon", position + new Vector3(0, -350, 0));

                    ParticleManager.Instance.addParticles("macedonia2", position + new Vector3(-300, -350, 5), Vector3.Zero, Color.White);
                    EnemyManager.Instance.addEnemy("grape", position + new Vector3(-300, -350, 0));
                    ParticleManager.Instance.addParticles("macedonia2", position + new Vector3(300, -350, 5), Vector3.Zero, Color.White);
                    EnemyManager.Instance.addEnemy("grape", position + new Vector3(300, -350, 0));

                    playAction("talk");
                    break;

                case tMacedoniaBossState.Laugh:
                    playAction("laugh");
                    break;

                case tMacedoniaBossState.Die:
                    showRayo(false);

                    foreach (Enemy enemy in EnemyManager.Instance.getActiveEnemies())
                    {
                        if (enemy != null && !(enemy is Macedonia))
                            enemy.die();
                    }

                    loadEndCinematic();
                    CinematicManager.Instance.playCinematic("macedoniaEnd");
                    playAction("attackShake");
                    break;
            }

            stateTime = 0.0f;
            state = newState;
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

            float length = Math.Abs(position.Y - playerPos.Y - 100) - 15 - 58 + 35;
            if (!isPlayerHit)
                length = 5000;

            for (int i = 0; i < MAX_RAYOS; i++)
            {
                rayo[i].renderState = tRenderState.NoRender;
                rayo[i].update();
            }

            for (int i = 0; i < MAX_RAYOS; i++)
            {
                if (length < 0)
                    break; 

                rayo[i].renderState = tRenderState.Render;
                    
                float factor = 1.0f;
                if (length < rayo[i].getFrameSize().Y)
                    factor = length / rayo[i].getFrameSize().Y;

                rayo[i].paintMask = new Vector2(1.0f, factor);

                float magicNum = 1.004f;
                if (i == 2)
                    magicNum = 1.002f;
                rayo[i].position = new Vector3(position.X - 6, position.Y - i * rayo[i].getFrameSize().Y * magicNum - rayo[i].getFrameSize().Y * 0.5f * factor - 75 , position.Z + 0.1f);
                length -= rayo[i].getFrameSize().Y;
            }

            if (isPlayerHit)
            {
                rayoEnd.renderState = tRenderState.Render;
                rayoEnd.update();
                rayoEnd.position = new Vector3(position.X - 6, GamerManager.getMainPlayer().position.Y + 100, position.Z + 0.2f);
            }
            else
            {
                rayoEnd.renderState = tRenderState.NoRender;
            }

            if (isPlayerHit)
            {
                if (!GamerManager.getMainPlayer().gotHitAtPart(null, 0))
                {
                    GamerManager.getMainPlayer().die();
                }
            }
        }

        public override void render()
        {
            if (visible)
            {
                base.render();

                if (renderState == tRenderState.Render)
                {
                    lifebar.lifePercentage = life / LIFE;
                    GraphicsManager.Instance.spriteBatchBegin();
                    lifebar.render();
                    GraphicsManager.Instance.spriteBatchEnd();
                }
            }

            foreach (RenderableEntity2D fruit in fruits)
            {
                fruit.render();
            }
        }
    }
}
