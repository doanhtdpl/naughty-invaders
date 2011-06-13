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
        const int FRUITS_TO_SPAWN = 50;
        const float FRUIT_SPAWN_TIME_MIN = 1.0f;
        const float FRUIT_SPAWN_TIME_MAX = 2.0f;
        float nextFruitSpawn = 2.0f;
        int spawnedFruits = 0;
        int savedFruits = 0;
        float timeAfterLast = 3.0f;
        bool playedEnd = false;
        bool startedThrowing = false;

        const float SPAWN_ORB_TIME = 0.1f;
        float lastOrb = 0.0f;
        int orbsToSpawn = 0;

        MacedoniaMinigame macedonia;
        List<MacedoniaFruit> fruits = new List<MacedoniaFruit>();

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

            DialogEvent de1 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogEpilepticIntro1.Translate());
            DialogEvent de2 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticIntro2.Translate());
            DialogEvent de3 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogEpilepticIntro3.Translate());
            DialogEvent de4 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticIntro4.Translate());
            DialogEvent de5 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogEpilepticIntro5.Translate());
            DialogEvent de6 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticIntro6.Translate());

            cinematic.events.Add((CinematicEvent)ae1);
            cinematic.events.Add((CinematicEvent)de1);
            cinematic.events.Add((CinematicEvent)de2);
            cinematic.events.Add((CinematicEvent)de3);
            cinematic.events.Add((CinematicEvent)de4);
            cinematic.events.Add((CinematicEvent)de5);
            cinematic.events.Add((CinematicEvent)de6);

            if (!GamerManager.getSessionOwner().data.skills["dash1"].obtained)
            {
                DialogEvent deSpecial = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogLearnDash.Translate());
                cinematic.events.Add((CinematicEvent)deSpecial);
            }

            ActorEvent ae2 = new ActorEvent(GamerManager.getMainPlayer(), false);
            ae2.moveTo(new Vector3(0.0f, -500.0f, 0.0f), 200.0f);
            cinematic.events.Add((CinematicEvent)ae2);

            ActorEvent ae3 = new ActorEvent(macedonia, false);
            ae3.setActionToPlay("attackShake");
            cinematic.events.Add((CinematicEvent)ae3);


            CinematicManager.Instance.addCinematic("epilepticMacedoniaIntro", cinematic);
        }

        void loadEndCinematic()
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(GamerManager.getMainPlayer(), false);
            ae1.moveTo(new Vector3(0.0f, -200.0f, 0.0f), 200.0f);
            ActorEvent ae12 = new ActorEvent(GamerManager.getMainPlayer(), false);
            ae12.setOrientation(0.0f);

            cinematic.events.Add((CinematicEvent)ae1);
            cinematic.events.Add((CinematicEvent)ae12);

            Player player = GamerManager.getMainPlayer();
            if (savedFruits < 25)
            {
                if (!GamerManager.getSessionOwner().data.skills["dash1"].obtained)
                {
                    DialogEvent deSpecial = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticBadSpecial.Translate());
                    cinematic.events.Add((CinematicEvent)deSpecial);
                }
                DialogEvent de1 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticBad1.Translate());
                ActorEvent ae2 = new ActorEvent(player, false);
                ae2.setOrientation(2.3f);
                ActorEvent ae3 = new ActorEvent(player, false);
                ae3.setOrientation(4.8f);
                ActorEvent ae4 = new ActorEvent(player, false);
                ae4.setOrientation(1.1f);
                ActorEvent ae5 = new ActorEvent(player, false);
                ae5.setOrientation(0.0f);
                DialogEvent de2 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogEpilepticBad2.Translate());
                ActorEvent ae6 = new ActorEvent(player, false);
                ae6.moveTo(new Vector3(player.positionX, -950.0f, 0.0f), 100.0f);
                DialogEvent de3 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticBad3.Translate());

                cinematic.events.Add((CinematicEvent)de1);
                cinematic.events.Add((CinematicEvent)ae2);
                cinematic.events.Add((CinematicEvent)ae3);
                cinematic.events.Add((CinematicEvent)ae4);
                cinematic.events.Add((CinematicEvent)ae5);
                cinematic.events.Add((CinematicEvent)de2);
                cinematic.events.Add((CinematicEvent)ae6);
                cinematic.events.Add((CinematicEvent)de3);
            }
            else if (savedFruits < 40)
            {
                DialogEvent de1 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticMedium1.Translate());
                DialogEvent de2 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogEpilepticMedium2.Translate());
                DialogEvent de3 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticMedium3.Translate());

                cinematic.events.Add((CinematicEvent)de1);
                cinematic.events.Add((CinematicEvent)de2);
                cinematic.events.Add((CinematicEvent)de3);

                if (!GamerManager.getSessionOwner().data.skills["dash1"].obtained)
                {
                    DialogEvent deSpecial = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticMediumSpecial.Translate());
                    cinematic.events.Add((CinematicEvent)deSpecial);
                }
            }
            else
            {
                DialogEvent de1 = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticGood1.Translate());
                DialogEvent de2 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogEpilepticGood2.Translate());

                cinematic.events.Add((CinematicEvent)de1);
                cinematic.events.Add((CinematicEvent)de2);
                if (!GamerManager.getSessionOwner().data.skills["dash1"].obtained)
                {
                    DialogEvent deSpecial = new DialogEvent(tDialogCharacter.Macedonia, TextKey.DialogEpilepticGoodSpecial.Translate());
                    cinematic.events.Add((CinematicEvent)deSpecial);
                }
            }

            CinematicManager.Instance.addCinematic("endMacedoniaMinigame", cinematic);
        }

        public override void initialize()
        {
            base.initialize();

            Camera2D.position = new Vector3(0.0f, 0.0f, 2000.0f);

            macedonia = new MacedoniaMinigame(new Vector3(0.0f, 200.0f, 0), 0.0f);
            loadIntroCinematic();
            CinematicManager.Instance.playCinematic("epilepticMacedoniaIntro");

            SoundManager.Instance.playSong("Naughty_jingle", true);
        }
        
        public override void update()
        {
            base.update();

            macedonia.update();
            GamerManager.getMainPlayer().mode = Player.tMode.SavingItems;

            if (orbsToSpawn > 0)
            {
                lastOrb -= SB.dt;
                if (lastOrb < 0.0f)
                {
                    OrbManager.Instance.addOrbs(macedonia.position2D, 5, 0, 0, 0, true);
                    lastOrb = SPAWN_ORB_TIME;
                    orbsToSpawn -= 5;
                }
            }

            if (CinematicManager.Instance.cinematicToPlay != null) return;

            if (!startedThrowing)
            {
                startedThrowing = true;
                macedonia.playAction("attackShake", false, 0.5f);
            }

            if (spawnedFruits >= FRUITS_TO_SPAWN)
            {
                timeAfterLast -= SB.dt;
            }
            else
            {
                nextFruitSpawn -= SB.dt;
                if (nextFruitSpawn < 0.0f)
                {
                    // spawn a fruit
                    fruits.Add(new MacedoniaFruit("minifruits-1" + Calc.randomNatural(1, 9), macedonia.position + new Vector3(0, 50, 10)));
                    float timeModifier = spawnedFruits * 0.05f;
                    nextFruitSpawn = Calc.randomScalar(FRUIT_SPAWN_TIME_MIN - timeModifier, FRUIT_SPAWN_TIME_MAX - timeModifier);
                    nextFruitSpawn = Calc.clamp(nextFruitSpawn, 0.5f, 1000.0f);
                    ++spawnedFruits;
                    macedonia.playAction("attackShake", false, 0.5f + (1.5f * (spawnedFruits / FRUITS_TO_SPAWN)));
                }
            }

            if (!playedEnd && spawnedFruits >= FRUITS_TO_SPAWN && timeAfterLast < 0.0f)
            {
                playedEnd = true;
                loadEndCinematic();
                macedonia.playAction("idle");
                CinematicManager.Instance.playCinematic("endMacedoniaMinigame");
                orbsToSpawn = savedFruits * 10;
            }

            Player player = GamerManager.getMainPlayer();
            for(int i=0; i<fruits.Count; ++i)
            {
                if (!fruits[i].isDead() && !fruits[i].isDying() && (fruits[i].position - player.position).Length() < 80)
                {
                    fruits[i].explode(true);
                    fruits[i].delete();
                    fruits.RemoveAt(i);
                    --i;
                    ++savedFruits;
                    SoundManager.Instance.playEffect("pickMacedoniaFruit");
                }
                else
                {
                    fruits[i].update();
                }
            }

            if (playedEnd && CinematicManager.Instance.cinematicToPlay == null)
            {
                GamerManager.getSessionOwner().data.levelsPassed["macedonia"] = true;
                StateManager.addState(StateManager.tGameState.EndStage);
            }
        }
        
        public override void  render()
        {
            base.render();
        }

        public override void dispose()
        {
        }
    }
}