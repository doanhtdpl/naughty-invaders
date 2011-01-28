using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class StateGame : GameState
    {
        GUI gui = new GUI();

        public override void initialize()
        {
            type = StateManager.tGS.Game;
            gameState = true;
            longLoad = true;
            DebugManager.Instance.initialize();
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
            SB.graphicsDevice.Clear(Color.DarkGray);
            SB.beginRender();
            GamerManager.renderPlayers();
            EnemyManager.Instance.render();
            LevelManager.Instance.render();
            ProjectileManager.Instance.render();
            ParticleManager.Instance.render();
            DebugManager.Instance.render();
            gui.render();

            DebugManager.Instance.addLine(new Vector3(0, 0, 0), new Vector3(200, 200, 0), Color.Red);
            DebugManager.Instance.addLine(new Vector3(0, 0, 0), new Vector3(200, -200, 0), Color.Green);
            DebugManager.Instance.addLine(new Vector3(0, 0, 0), new Vector3(-200, 200, 0), Color.Blue);
            DebugManager.Instance.render();
        }
        
        public override void update()
        {
            base.update();

            GamerManager.updatePlayers();
            EnemyManager.Instance.update();
            LevelManager.Instance.update();
            ProjectileManager.Instance.update();
            ParticleManager.Instance.update();
            gui.update();
            SB.cam.update();

            if (GamerManager.getMainControls().Start_firstPressed())
                StateManager.enqueueState(StateManager.tGS.Pause);
        }
        
        public override void dispose()
        {
        }
    }
}