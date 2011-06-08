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
            if (level != null)
            {
                EditorHelper.Instance.loadNewLevelFromGame(level);
            }
        }
        public StateGame() { }

        public override void initialize()
        {
            type = StateManager.tGS.Game;
            gameState = true;
            longLoad = true;
            DebugManager.Instance.initialize();
            ParticleManager.Instance.loadXML();
            SoundManager.Instance.loadXML();
        }

        public void restartLevel()
        {
        }
        public void resetLevelData()
        {
        }

        public override void loadContent()
        {
        }

        bool addCameraNodes = true;
        public override void render()
        {
            GraphicsManager.Instance.graphicsDevice.Clear(SB.BGColor);

            EntityManager.Instance.render();
            LevelManager.Instance.render();
            DebugManager.Instance.render();
            GUIManager.Instance.render();
            CinematicManager.Instance.render();

#if !EDITOR
            if (!GamerManager.getSessionOwner().data.skills["dash1"].obtained
                && GamerManager.getSessionOwner().data.XP >= GamerManager.getSessionOwner().data.skills["dash1"].cost)
            {
                if (SB.gameTime.TotalGameTime.Milliseconds < 500)
                {
                    GraphicsManager.Instance.spriteBatchBegin();
                    "buy new skills! press ::BACK".renderNI(Screen.getXYfromCenter(330, -230), 0.8f);
                    GraphicsManager.Instance.spriteBatchEnd();
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
                StateManager.gameStates.Add(new StatePausedGame());
            }
            if (GamerManager.getMainControls().Back_firstPressed())
            {
                StateManager.gameStates.Add(new StateSkillsMenu());
            }
#endif
        }
        
        public override void dispose()
        {
        }
    }
}