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
        public const float SPAWN_TOMATO_KINGTOMATO_STATE_MIN_TIME = 1.0f;
        public const float SPAWN_TOMATO_KINGTOMATO_STATE_MAX_TIME = 2.0f;
        const float REST_TIME = 2.0f;
        const float FLEE_TIME = 3.0f;
        const int SHITTING_TOMATOES = 40;

        public enum tState { Combat, Rest, KingTomato }
        tState state;

        int currentWave;
        int tomatoesToSpawn;
        float spawnTomatoTime;
        float spawnTomatoTimer;
        float restTime;
        float spawnTomatoKingTomatoState;

        bool shitSpawnDone = false;
        bool fleeDone = false;
        float fleeTimer = FLEE_TIME;
        KingTomato kingTomato;

        public MinigameKingTomato(string level):base(level)
        {
            currentWave = 1;
            spawnTomatoTimer = 0.0f;
            spawnTomatoKingTomatoState = 0.0f;

            restTime = 3.0f;
            state = tState.Rest;
            loadIntroCinematic();
            loadReturnsCinematic();
            CinematicManager.Instance.playCinematic("kingTomatoIntro");
        }

        public override void initialize()
        {
            base.initialize();

            CameraManager.Instance.cameraMode = CameraManager.tCameraMode.None;
            Camera2D.position = new Vector3(0.0f, 0.0f, 2000.0f);

            EntityManager.Instance.registerEntity(GamerManager.getGamerEntity(0).Player);
        }

        void loadIntroCinematic()
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(GamerManager.getMainPlayer(), 999.0f, 0.0f, false);
            ae1.setAt(new Vector3(0.0f, -900.0f, 0.0f));
            ae1.moveTo(new Vector3(0.0f, -220.0f, 0.0f), 200.0f);
            cinematic.events.Add((CinematicEvent)ae1);
            DialogEvent de1 = new DialogEvent(tDialogCharacter.Wish,
                "Hola! Necesito saber como llegar a Ciudad Vegetal");
            cinematic.events.Add(de1);
            DialogEvent de2 = new DialogEvent(tDialogCharacter.OnionElder,
                "Hey chico, es por ahi. Pero el rey tomate y sus tropas vienen por ese camino. Nos quieren invadir!");
            cinematic.events.Add(de2);
            DialogEvent de3 = new DialogEvent(tDialogCharacter.Wish,
               "Yo os ayudare, asi podre llegar a Ciudad Vegetal!");
            cinematic.events.Add(de3);
            DialogEvent de4 = new DialogEvent(tDialogCharacter.OnionElder,
               "Genial! Te podemos ayudar prestandote el arma legendaria del pueblo: la Garlic Gun!");
            cinematic.events.Add(de4);
            ActorEvent ae2 = new ActorEvent(GamerManager.getMainPlayer(), 2.5f, 0.3f, false);
            ae2.addActorFunction("activateGarlicGun");
            cinematic.events.Add(ae2);
            DialogEvent de5 = new DialogEvent(tDialogCharacter.Wish,
               "Wow! parece muy poderosa!");
            cinematic.events.Add(de5);
            DialogEvent de6 = new DialogEvent(tDialogCharacter.OnionElder,
               "Presionando ::RS podras disparar en cualquier direccion! Wo! Ya llegan los tomates!");
            cinematic.events.Add(de6);

            CinematicManager.Instance.addCinematic("kingTomatoIntro", cinematic);
        }

        void loadSpeechCinematic(Enemy kingTomato)
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(kingTomato, 999.0f, 0.0f, false);
            ae1.setAt(new Vector3(1000.0f, 900.0f, 0.0f));
            ae1.moveTo(new Vector3(500.0f, 260.0f, 0.0f), 70.0f);

            DialogEvent de1 = new DialogEvent( tDialogCharacter.KingTomato, "Acabare contigo sucia perra!");
            DialogEvent de2 = new DialogEvent( tDialogCharacter.Wish, "Tu eres gilipollas");
            DialogEvent de3 = new DialogEvent( tDialogCharacter.KingTomato, "Adelante tomatinaaaa!!!");

            cinematic.events.Add((CinematicEvent)ae1);
            cinematic.events.Add((CinematicEvent)de1);
            cinematic.events.Add((CinematicEvent)de2);
            cinematic.events.Add((CinematicEvent)de3);
            CinematicManager.Instance.addCinematic("kingTomatoSpeech", cinematic);
        }
        void loadReturnsCinematic()
        {
            Cinematic cinematic = new Cinematic();

            DialogEvent de1 = new DialogEvent(tDialogCharacter.KingTomato, "Cobardes... no os necesito! Ahora veras! Hahaha");

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
                    tomatoesToSpawn = 70;

                    kingTomato = (KingTomato)EnemyManager.Instance.addEnemy("kingTomato", new Vector3(700, 900, 0));
                    loadSpeechCinematic(kingTomato);
                    CinematicManager.Instance.playCinematic("kingTomatoSpeech");
                    break;
            }
            return true;
        }

        void spawnTomato()
        {
            Vector3 position;
            if (Calc.randomScalar() > 0.5f)
            {
                position.X = Calc.randomScalar(Camera2D.screen.Left + 400, Camera2D.screen.Right);
                position.Y = Camera2D.screen.Bottom;
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
                    if (restTime < 0.0f)
                    {
                        ++currentWave;
                        spawnTomatoTimer -= SB.dt;
                        spawnTomatoTime = 0.02f;
                        tomatoesToSpawn = Calc.randomNatural(30, 60) + 10 * currentWave;
                        restTime = tomatoesToSpawn * 0.25f;
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
                        List<Entity2D> enemies = EnemyManager.Instance.getEnemies();
                        for (int i = 0; i < enemies.Count; ++i)
                        {
                            if (enemies[i] is TomatoFollower)
                            {
                                ((TomatoFollower)enemies[i]).fleeing = true;
                            }
                        }
                    }
                }
                break;
            }
        }
        
        public override void dispose()
        {
            GamerManager.getMainPlayer().mode = Player.tMode.Arcade;
        }
    }
}