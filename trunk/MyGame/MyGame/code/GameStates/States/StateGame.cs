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
    class StateGame : GameState
    {
        #if EDITOR
        public bool gameRunning = false;
        bool keyPPressed = false;
        #endif

        string level;

        public StateGame(string level)
        {
            this.level = level;
            TransitionManager.Instance.addTransition(TransitionManager.tTransition.FadeOut, 0.7f, Color.Black);
        }
        public StateGame() { }

        public override void initialize()
        {
            gameState = true;
            longLoad = true;
#if DEBUG
            DebugManager.Instance.initialize();
#endif
            ParticleManager.Instance.loadXML();
            SoundManager.Instance.loadXML();

            if (level == "final_Level01")
            {
                loadAndPlayIntroCinematic();
            }
        }
        public override void loadContent()
        {
            if (level != null)
            {
                EditorHelper.Instance.loadNewLevelFromGame(level);
            }
        }

        void loadAndPlayIntroCinematic()
        {

            Player player = GamerManager.getMainPlayer();
            Cinematic cinematic = new Cinematic();

            if (GamerManager.getSessionOwner().data.levelsPassed["final_Level01"])
            {
                ActorEvent ae1 = new ActorEvent(player, false);
                ae1.moveTo(new Vector3(0.0f, -200.0f, 0.0f), 200.0f);
                ActorEvent ae12 = new ActorEvent(player, false);
                ae12.setOrientation(0.0f);

                DialogEvent de7 = new DialogEvent(tDialogCharacter.DarkWish, TextKey.DialogDarkWishRemind1.Translate());
                DialogEvent de8 = new DialogEvent(tDialogCharacter.DarkWish,TextKey.DialogDarkWishRemind2.Translate());

                cinematic.events.Add((CinematicEvent)ae1);
                cinematic.events.Add((CinematicEvent)ae12);
                cinematic.events.Add((CinematicEvent)de7);
                cinematic.events.Add((CinematicEvent)de8);
            }
            else
            {
                ConsequenceFunctions.wishEntersGame();
                DialogEvent de1 = new DialogEvent(tDialogCharacter.DarkWish, TextKey.DialogDarkWish1.Translate());
                ActorEvent ae2 = new ActorEvent(player, false);
                ae2.setOrientation(2.3f);
                ActorEvent ae3 = new ActorEvent(player, false);
                ae3.setOrientation(4.8f);
                ActorEvent ae4 = new ActorEvent(player, false);
                ae4.setOrientation(1.1f);
                ActorEvent ae5 = new ActorEvent(player, false);
                ae5.setOrientation(0.0f);
                DialogEvent de2 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogDarkWish2.Translate());
                DialogEvent de3 = new DialogEvent(tDialogCharacter.DarkWish, TextKey.DialogDarkWish3.Translate());
                DialogEvent de4 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogDarkWish4.Translate());
                DialogEvent de5 = new DialogEvent(tDialogCharacter.DarkWish, TextKey.DialogDarkWish5.Translate());
                DialogEvent de6 = new DialogEvent(tDialogCharacter.Wish, TextKey.DialogDarkWish6.Translate());
                DialogEvent de7 = new DialogEvent(tDialogCharacter.DarkWish, TextKey.DialogDarkWish7.Translate());
                DialogEvent de8 = new DialogEvent(tDialogCharacter.DarkWish, TextKey.DialogDarkWish8.Translate());

                cinematic.events.Add((CinematicEvent)de1);
                cinematic.events.Add((CinematicEvent)ae2);
                cinematic.events.Add((CinematicEvent)ae3);
                cinematic.events.Add((CinematicEvent)ae4);
                cinematic.events.Add((CinematicEvent)ae5);
                cinematic.events.Add((CinematicEvent)de2);
                cinematic.events.Add((CinematicEvent)de3);
                cinematic.events.Add((CinematicEvent)de4);
                cinematic.events.Add((CinematicEvent)de5);
                cinematic.events.Add((CinematicEvent)de6);
                cinematic.events.Add((CinematicEvent)de7);
                cinematic.events.Add((CinematicEvent)de8);
            }

            CinematicManager.Instance.addCinematic("fruitownIntro", cinematic);
            CinematicManager.Instance.playCinematic("fruitownIntro");
        }

        public void restartLevel()
        {
        }
        public void resetLevelData()
        {
        }

        public override void render()
        {
            GraphicsManager.Instance.graphicsDevice.Clear(SB.BGColor);

            EntityManager.Instance.render();
            LevelManager.Instance.render();
            DebugManager.Instance.render();
            GUIManager.Instance.render();
            CinematicManager.Instance.render();

#if !EDITOR
            if (CinematicManager.Instance.cinematicToPlay == null)
            {
                if (!GamerManager.getSessionOwner().data.skills["dash1"].obtained
                    && GamerManager.getSessionOwner().data.XP >= GamerManager.getSessionOwner().data.skills["dash1"].cost)
                {
                    if (SB.gameTime.TotalGameTime.Milliseconds < 500)
                    {
                        GraphicsManager.Instance.spriteBatchBegin();
                        TextKey.MessageNewSkills.Translate().renderNI(Screen.getXYfromCenter(330, -230), 0.8f);
                        GraphicsManager.Instance.spriteBatchEnd();
                    }
                }
            }
#endif
        }
        
        public override void update()
        {
            base.update();

#if EDITOR
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                if (!keyPPressed)
                {
                    gameRunning = !gameRunning;
                }
                keyPPressed = true;
            }
            else
            {
                keyPPressed = false;
            }
            if (gameRunning)
            {
#endif
                GamerManager.updatePlayers();
                EnemyManager.Instance.update();
                LevelManager.Instance.update();
                ProjectileManager.Instance.update();
                ParticleManager.Instance.update();
                OrbManager.Instance.update();
                CameraManager.Instance.update();
                GUIManager.Instance.update();
                CinematicManager.Instance.update();
                TriggerManager.Instance.update();
#if EDITOR
            }
#endif
            SB.cam.update();

#if !EDITOR
            // state changes
            if (GamerManager.getMainControls().Start_firstPressed())
            {
                StateManager.addState(StateManager.tGameState.Pause);
            }
            if (GamerManager.getMainControls().Back_firstPressed())
            {
                StateManager.addState(StateManager.tGameState.SkillsMenu);
            }
#endif
        }
        
        public override void dispose()
        {
        }
    }
}