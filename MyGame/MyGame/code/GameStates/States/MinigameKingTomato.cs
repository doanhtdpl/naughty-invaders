using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class MinigameKingTomato : StateGame
    {
        public const float SPAWN_TOMATO_KINGTOMATO_STATE_MIN_TIME = 0.2f;
        public const float SPAWN_TOMATO_KINGTOMATO_STATE_MAX_TIME = 1.0f;
        const float REST_TIME = 2.0f;
        const float FLEE_TIME = 1.0f;
        const int SHITTING_TOMATOES = 100;

        public enum tState { Combat, Rest, KingTomato, End }
        tState state;

        int currentWave;
        int tomatoesToSpawn;
        float spawnTomatoTime;
        float spawnTomatoTimer;
        float restTime;
        float spawnTomatoKingTomatoState;

        bool kingTomatoStartedWave = false;
        bool shitSpawnDone = false;
        bool fleeDone = false;
        float fleeTimer = FLEE_TIME;

        float afterDieTime = 0.0f;

        KingTomato kingTomato;
        AnimatedEntity2D onionElder;

        public MinigameKingTomato(string level):base(level)
        {
            currentWave = 1;
            spawnTomatoTimer = 0.0f;
            spawnTomatoKingTomatoState = 0.0f;

            restTime = 3.0f;
            state = tState.Rest;
        }

        public override void initialize()
        {
            base.initialize();

            CameraManager.Instance.cameraMode = CameraManager.tCameraMode.None;
            Camera2D.position = new Vector3(0.0f, 0.0f, 2000.0f);

            //EntityManager.Instance.registerEntity(GamerManager.getGamerEntity(0).Player);
            onionElder = new AnimatedEntity2D("animatedProps", "onionElder", new Vector3(0.0f, 150.0f, 0.0f), 0.0f, Color.White);

            kingTomato = (KingTomato)EnemyManager.Instance.addEnemy("kingTomato", new Vector3(1200.0f, 1000.0f, 0));
            kingTomato.updateState = RenderableEntity2D.tUpdateState.NoUpdate;
            loadIntroCinematic();
            loadSpeechCinematic(kingTomato);
            loadReturnsCinematic();
            CinematicManager.Instance.playCinematic("kingTomatoIntro");
        }

        void loadIntroCinematic()
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(GamerManager.getMainPlayer(), false);
            ae1.setAt(new Vector3(0.0f, -900.0f, 0.0f));
            ae1.moveTo(new Vector3(0.0f, -220.0f, 0.0f), 200.0f);
            cinematic.events.Add((CinematicEvent)ae1);
            DialogEvent de1 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogOnionIntro1.Translate());
            cinematic.events.Add(de1);
            DialogEvent de2 = new DialogEvent(tDialogCharacter.OnionElder, TextKey.DialogOnionIntro2.Translate());
            cinematic.events.Add(de2);
            DialogEvent de3 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogOnionIntro3.Translate());
            cinematic.events.Add(de3);
            DialogEvent de4 = new DialogEvent(tDialogCharacter.OnionElder, TextKey.DialogOnionIntro4.Translate());
            cinematic.events.Add(de4);
            ActorEvent ae2 = new ActorEvent(GamerManager.getMainPlayer(), false, 0.3f, true, 2.5f);
            ae2.addActorFunction("activateGarlicGun");
            cinematic.events.Add(ae2);
            DialogEvent de5 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogOnionIntro5.Translate());
            cinematic.events.Add(de5);
            DialogEvent de6 = new DialogEvent(tDialogCharacter.OnionElder, TextKey.DialogOnionIntro6.Translate());
            cinematic.events.Add(de6);
            ActorEvent ae3 = new ActorEvent(onionElder, false);
            ae3.moveTo(new Vector3(-1070.0f, -490.0f, 0.0f), 300.0f);
            cinematic.events.Add((CinematicEvent)ae3);

            CinematicManager.Instance.addCinematic("kingTomatoIntro", cinematic);
        }
        void loadSpeechCinematic(Enemy kingTomato)
        {
            Cinematic cinematic = new Cinematic();

            ActorEvent ae1 = new ActorEvent(GamerManager.getMainPlayer(), false);
            ae1.moveTo(new Vector3(0.0f, 0, 0.0f), Player.SPEED, true);

            ActorEvent ae2 = new ActorEvent(GamerManager.getMainPlayer(), false);
            ae2.setOrientation(-Calc.PiOver4);

            ActorEvent ae3 = new ActorEvent(kingTomato, false);
            ae3.setAt(new Vector3(1200.0f, 1000.0f, 0.0f));
            ae3.moveTo(new Vector3(600.0f, 360.0f, 0.0f), KingTomato.SPEED);

            DialogEvent de1 = new DialogEvent(tDialogCharacter.KingTomato, TextKey.DialogTomatoComes1.Translate());
            DialogEvent de2 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogTomatoComes2.Translate());
            DialogEvent de3 = new DialogEvent(tDialogCharacter.KingTomato, TextKey.DialogTomatoComes3.Translate());

            cinematic.events.Add((CinematicEvent)ae1);
            cinematic.events.Add((CinematicEvent)ae2);
            cinematic.events.Add((CinematicEvent)ae3);
            cinematic.events.Add((CinematicEvent)de1);
            cinematic.events.Add((CinematicEvent)de2);
            cinematic.events.Add((CinematicEvent)de3);
            CinematicManager.Instance.addCinematic("kingTomatoSpeech", cinematic);
        }
        void loadReturnsCinematic()
        {
            Cinematic cinematic = new Cinematic();

            DialogEvent de1 = new DialogEvent(tDialogCharacter.KingTomato, TextKey.DialogTomatoAttack1.Translate());
            cinematic.events.Add((CinematicEvent)de1);
            CinematicManager.Instance.addCinematic("kingTomatoReturns", cinematic);
        }

        int getTomatoesLeft()
        {
            int tomatoesLeft = 0;
            List<Entity2D> enemies = EnemyManager.Instance.getEnemies();
            for (int i = 0; i < enemies.Count; ++i)
            {
                if (enemies[i].entityName == "tomato")
                {
                    ++tomatoesLeft;
                }
            }
            enemies = EnemyManager.Instance.getActiveEnemies();
            for (int i = 0; i < enemies.Count; ++i)
            {
                if (enemies[i].entityName == "tomato")
                {
                    ++tomatoesLeft;
                }
            }
            return tomatoesLeft;
        }

        bool startNewWave()
        {
            switch (currentWave)
            {
                case 1:
                    spawnTomatoTimer -= SB.dt;
                    spawnTomatoTime = 0.05f;
                    tomatoesToSpawn = 10;
                    break;
                case 2:
                    spawnTomatoTimer -= SB.dt;
                    spawnTomatoTime = 0.04f;
                    tomatoesToSpawn = 30;
                    break;
                case 3:
                    if (OrbManager.Instance.orbs.Count > 0) return false;
                    spawnTomatoTimer -= SB.dt;
                    spawnTomatoTime = 0.03f;
                    tomatoesToSpawn = 50;

                    CinematicManager.Instance.playCinematic("kingTomatoSpeech");
                    break;
            }
            return true;
        }

        void spawnTomato()
        {
            Vector3 position = Vector3.Zero;
            if (Calc.randomScalar() > 0.5f)
            {
                position.X = Calc.randomScalar(Camera2D.screen.Left + 400, Camera2D.screen.Right);
                position.Y = Camera2D.screen.Bottom + 50.0f;
            }
            else
            {
                position.Y = Calc.randomScalar(Camera2D.screen.Top + 300, Camera2D.screen.Bottom);
                position.X = Camera2D.screen.Right;
            }
            position.Z = 0.0f;
            EnemyManager.Instance.addEnemy("tomatoFollower", position);
        }

        bool updateTomatoes()
        {
            spawnTomatoTimer -= SB.dt;
            if (tomatoesToSpawn > 0 && spawnTomatoTimer < 0.0f)
            {
                spawnTomato();

                spawnTomatoTimer = spawnTomatoTime;
                --tomatoesToSpawn;
            }

            return tomatoesToSpawn > 0;
        }

        public override void update()
        {
            base.update();

            if (CinematicManager.Instance.cinematicToPlay != null) return;

            switch (state)
            {
                case tState.Rest:
                    restTime -= SB.dt;
                    if (restTime < 0.0f)
                    {
                        if (startNewWave())
                        {
                            state = tState.Combat;
                        }
                    }
                break;
                case tState.Combat:
                    if (!updateTomatoes() && getTomatoesLeft() == 0)
                    {
                        ++currentWave;
                        if (currentWave > 3)
                        {
                            restTime = Calc.randomScalar(REST_TIME * 2, REST_TIME * 3);
                            state = tState.KingTomato;
                            kingTomato.playAction("shouting");
                        }
                        else
                        {
                            state = tState.Rest;
                        }
                    }
                break;
                case tState.KingTomato:
                    if (kingTomato.state == KingTomato.tState.Commanding)
                    {
                        restTime -= SB.dt;
                        updateTomatoes();
                        if (restTime < 2.0f && !kingTomatoStartedWave)
                        {
                            kingTomatoStartedWave = true;
                            kingTomato.playAction("shouting");
                        }
                        if (restTime < 0.0f)
                        {
                            ++currentWave;
                            spawnTomatoTimer -= SB.dt;
                            spawnTomatoTime = 0.02f;
                            tomatoesToSpawn = Calc.randomNatural(25, 35) + 10 * currentWave;
                            restTime = tomatoesToSpawn * 0.25f;
                            kingTomatoStartedWave = false;
                        }
                        spawnTomatoKingTomatoState -= SB.dt;
                        if (spawnTomatoKingTomatoState < 0.0f)
                        {
                            spawnTomatoKingTomatoState = Calc.randomScalar(SPAWN_TOMATO_KINGTOMATO_STATE_MIN_TIME, SPAWN_TOMATO_KINGTOMATO_STATE_MAX_TIME);
                            spawnTomato();
                        }
                    }
                    else if (kingTomato.state == KingTomato.tState.Shitting)
                    {
                        if (!shitSpawnDone)
                        {
                            shitSpawnDone = true;
                            for (int i = 0; i < SHITTING_TOMATOES; ++i)
                            {
                                spawnTomato();
                            }
                        }
                        fleeTimer -= SB.dt;
                        if (!fleeDone && fleeTimer < 0.0f)
                        {
                            fleeDone = true;
                            List<Entity2D> enemies = EnemyManager.Instance.getActiveEnemies();
                            for (int i = 0; i < enemies.Count; ++i)
                            {
                                if (enemies[i] is TomatoFollower)
                                {
                                    ((TomatoFollower)enemies[i]).fleeing = true;
                                }
                            }
                            CinematicManager.Instance.playCinematic("kingTomatoReturns");
                            kingTomato.state = KingTomato.tState.Recovering;
                        }
                    }
                    else if (kingTomato.state == KingTomato.tState.Delete)
                    {
                        afterDieTime += SB.dt;
                        if (afterDieTime > 2.0f)
                        {
                            kingTomato.requestDelete(true);
                            GamerManager.getSessionOwner().data.levelsPassed["onionVillage"] = true;
                            StateManager.addState(StateManager.tGameState.EndStage);
                        }
                    }
                break;
            }
        }
        
        public override void dispose()
        {
        }
    }
}