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
    class MinigameEpilepticMacedonia : StateGame
    {
        const float FRUIT_SPAWN_TIME_MIN = 2.0f;
        const float FRUIT_SPAWN_TIME_MAX = 4.0f;
        float nextFruitSpawn = 3.0f;
        int spawnedFruits = 0;

        Macedonia macedonia;

        public MinigameEpilepticMacedonia(string level)
            : base(level)
        {
            Camera2D.position = new Vector3(0.0f, 0.0f, 1500.0f);
            // TODO ugly! this must have to be set with the editor and loaded from xml
            GamerManager.getMainPlayer().mode = Player.tMode.SavingItems;
        }

        void loadIntroCinematic()
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(GamerManager.getMainPlayer(), false);
            ae1.setAt(new Vector3(0.0f, -1000.0f, 0.0f));
            ae1.moveTo(new Vector3(0.0f, -200.0f, 0.0f), 200.0f);

            DialogEvent de1 = new DialogEvent(tDialogCharacter.Wish, "Woo macedonia! Yo te ayudare!");
            DialogEvent de2 = new DialogEvent(tDialogCharacter.Macedonia, "Te lo agradezco Wish, eres una puta de mucho cuidado! :)");

            ActorEvent ae2 = new ActorEvent(GamerManager.getMainPlayer(), false);
            ae2.moveTo(new Vector3(0.0f, -500.0f, 0.0f), 200.0f);

            cinematic.events.Add((CinematicEvent)ae1);
            cinematic.events.Add((CinematicEvent)de1);
            cinematic.events.Add((CinematicEvent)de2);
            cinematic.events.Add((CinematicEvent)ae2);
            CinematicManager.Instance.addCinematic("epilepticMacedoniaIntro", cinematic);
        }

        public override void initialize()
        {
            base.initialize();

            Camera2D.position = new Vector3(0.0f, 0.0f, 2000.0f);

            macedonia = (Macedonia)EnemyManager.Instance.addEnemy("macedonia", new Vector3(0.0f, 200.0f, 0));
            loadIntroCinematic();
            CinematicManager.Instance.playCinematic("epilepticMacedoniaIntro");
        }
        
        public override void update()
        {
            base.update();

            nextFruitSpawn -= SB.dt;
            if (nextFruitSpawn < 0.0f)
            {
                nextFruitSpawn = Calc.randomScalar(FRUIT_SPAWN_TIME_MIN, FRUIT_SPAWN_TIME_MAX);
                ++spawnedFruits;

                // spawn a fruit

            }

        }
        
        public override void dispose()
        {
        }
    }
}