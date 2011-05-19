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
        public enum tState { Combat, Rest }
        tState state;

        int currentWave;
        int tomatoesToSpawn;
        float spawnTomatoTime;
        float spawnTomatoTimer;
        float restTime;

        public MinigameKingTomato(string level):base(level)
        {
            currentWave = 1;
            spawnTomatoTimer = 0.0f;

            restTime = 3.0f;
            state = tState.Rest;
            loadIntroCinematic();
            CinematicManager.Instance.playCinematic("kingTomatoIntro");
        }

        public override void initialize()
        {
            base.initialize();

            CameraManager.Instance.cameraMode = CameraManager.tCameraMode.None;
            Camera2D.position = new Vector3(0.0f, 0.0f, 2000.0f);

            GamerManager.getMainPlayer().mode = Player.tMode.GarlicGun;

            EntityManager.Instance.registerEntity(GamerManager.getGamerEntity(0).Player);
        }

        void loadIntroCinematic()
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(0.0f, 12.0f, GamerManager.getMainPlayer());
            ae1.setAt(new Vector3(0.0f, -900.0f, 0.0f));
            ae1.moveTo(new Vector3(0.0f, -220.0f, 0.0f), 200.0f);

            DialogEvent de1 = new DialogEvent(2.0f, 2.0f, tDialogCharacter.Wish, "Esto es Onion Village? resistiremos!", 70.0f);

            cinematic.events.Add((CinematicEvent)ae1);
            cinematic.events.Add((CinematicEvent)de1);
            CinematicManager.Instance.addCinematic("kingTomatoIntro", cinematic);
        }

        void loadSpeechCinematic(Enemy kingTomato)
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(0.0f, 8.0f, kingTomato);
            ae1.setAt(new Vector3(700.0f, 900.0f, 0.0f));
            ae1.moveTo(new Vector3(700.0f, 600.0f, 0.0f), 50.0f);

            DialogEvent de1 = new DialogEvent(2.0f, 2.0f, tDialogCharacter.Wish, "Acabare contigo sucia perra!", 70.0f);
            DialogEvent de2 = new DialogEvent(4.5f, 2.0f, tDialogCharacter.Wish, "Tu eres gilipollas", 70.0f);
            DialogEvent de3 = new DialogEvent(7.0f, 2.0f, tDialogCharacter.Wish, "Adelante tomatinaaaa!!!", 70.0f);

            cinematic.events.Add((CinematicEvent)ae1);
            cinematic.events.Add((CinematicEvent)de1);
            cinematic.events.Add((CinematicEvent)de2);
            cinematic.events.Add((CinematicEvent)de3);
            CinematicManager.Instance.addCinematic("kingTomatoSpeech", cinematic);
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

        void startNewWave()
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
                    spawnTomatoTimer -= SB.dt;
                    spawnTomatoTime = 0.03f;
                    tomatoesToSpawn = 50;

                    Enemy e = (Enemy)EnemyManager.Instance.addEnemy("kingTomato", new Vector3(700, 900, 0));
                    loadSpeechCinematic(e);
                    CinematicManager.Instance.playCinematic("kingTomatoSpeech");
                    break;
                case 4:
                    spawnTomatoTimer -= SB.dt;
                    spawnTomatoTime = 0.02f;
                    tomatoesToSpawn = 100;
                    break;
                case 5:
                    spawnTomatoTimer -= SB.dt;
                    spawnTomatoTime = 0.02f;
                    tomatoesToSpawn = 200;
                    break;
            }
        }

        bool updateTomatoes()
        {
            spawnTomatoTimer -= SB.dt;
            if (tomatoesToSpawn > 0 && spawnTomatoTimer < 0.0f)
            {
                Vector3 position;
                position.X = Calc.randomScalar(Camera2D.screen.Left, Camera2D.screen.Right);
                position.Y = Camera2D.screen.Bottom;
                position.Z = 0.0f;
                EnemyManager.Instance.addEnemy("tomatoFollower", position);

                spawnTomatoTimer = spawnTomatoTime;
                --tomatoesToSpawn;
            }

            return tomatoesToSpawn > 0;
        }

        const float REST_TIME = 2.0f;
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
                        state = tState.Combat;
                        startNewWave();
                    }
                    break;
                case tState.Combat:
                    if (!updateTomatoes() && getTomatoesLeft() == 0)
                    {
                        ++currentWave;
                        restTime = REST_TIME;
                        state = tState.Rest;
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