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
        int tomatoesToSpawn;
        float spawnTomatoTime;
        float spawnTomatoTimer;

        public MinigameKingTomato(string level):base(level)
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(0.0f, 12.0f, GamerManager.getMainPlayer());
            ae1.setAt(new Vector3(0.0f, -700.0f, 0.0f));
            ae1.moveTo(new Vector3(0.0f, 0.0f, 0.0f), 200.0f);

            DialogEvent de1 = new DialogEvent(2.0f, 2.0f, tDialogCharacter.Wish, "Esto es Onion Village? resistiremos!", 70.0f);

            cinematic.events.Add((CinematicEvent)ae1);
            cinematic.events.Add((CinematicEvent)de1);
            CinematicManager.Instance.addCinematic("kingTomatoIntro", cinematic);

            CameraManager.Instance.cameraMode = CameraManager.tCameraMode.None;
            Camera2D.position = new Vector3(0.0f, 0.0f, 2000.0f);

            GamerManager.getMainPlayer().usingGarlicGun = true;

            spawnTomatoTimer = 0.0f;
            spawnTomatoTime = 0.02f;
            tomatoesToSpawn = 100;
        }
        bool firstUpdate = true;
        public override void update()
        {
            base.update();

            spawnTomatoTimer -= SB.dt;

            if (firstUpdate)
            {
                firstUpdate = false;
                EntityManager.Instance.registerEntity(GamerManager.getGamerEntity(0).Player);
            }

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
        }
        
        public override void dispose()
        {
        }
    }
}