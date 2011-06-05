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
            if (level != null)
            {
                EditorHelper.Instance.loadNewLevelFromGame(level);
            }
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
#endif
        }
        
        public override void dispose()
        {
        }
    }
}