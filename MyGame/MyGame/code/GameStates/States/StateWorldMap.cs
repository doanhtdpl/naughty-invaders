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
    class StateWorldMap : GameState
    {
        public override void initialize()
        {
            type = StateManager.tGS.Game;
            gameState = true;
            longLoad = true;
            DebugManager.Instance.initialize();
            ParticleManager.Instance.loadXML();
            EditorHelper.Instance.loadNewLevel("mapa");
            CameraManager.Instance.cameraMode = CameraManager.tCameraMode.FollowPlayer;
        }

        public override void loadContent()
        {

        }

        public override void render()
        {
            EntityManager.Instance.render();
            LevelManager.Instance.render();
            DebugManager.Instance.render();
            GUIManager.Instance.render();
        }
        
        public override void update()
        {
            base.update();

            LevelManager.Instance.update();
            ProjectileManager.Instance.update();
            ParticleManager.Instance.update();
            OrbManager.Instance.update();
            CameraManager.Instance.update();
            GUIManager.Instance.update();

            SB.cam.update();
        }
        
        public override void dispose()
        {
        }
    }
}