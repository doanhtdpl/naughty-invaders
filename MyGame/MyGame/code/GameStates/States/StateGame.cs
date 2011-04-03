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
        bool gameRunning = false;
        bool keyPPressed = false;
        #endif

        public override void initialize()
        {
            type = StateManager.tGS.Game;
            gameState = true;
            longLoad = true;
            DebugManager.Instance.initialize();
            ParticleManager.Instance.loadXML();
            //EditorHelper.Instance.loadLevelFromXML("fruit-1-1");
            CameraManager.Instance.setCameraMode(CameraManager.tCameraMode.Node);
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

        public override void render()
        {
            if (GamerManager.getGamerEntity(PlayerIndex.One).Controls.B_pressed())
            {
                GraphicsManager.Instance.graphicsDevice.Clear(new Color(0, 0, 0));
            }
            else
            {
                GraphicsManager.Instance.graphicsDevice.Clear(new Color(79, 98, 37));
            }
            EntityManager.Instance.render();
            LevelManager.Instance.render();
            DebugManager.Instance.render();
            GUIManager.Instance.render();
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

#if EDITOR
            }
#endif
            SB.cam.update();

            if (GamerManager.getGamerEntities().Count > 0 && GamerManager.getMainControls().Start_firstPressed())
                StateManager.enqueueState(StateManager.tGS.Pause);
        }
        
        public override void dispose()
        {
        }
    }
}