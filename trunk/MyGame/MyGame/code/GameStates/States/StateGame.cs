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
        GUI gui = new GUI();

        public override void initialize()
        {
            type = StateManager.tGS.Game;
            gameState = true;
            longLoad = true;
            DebugManager.Instance.initialize();
            //EditorHelper.Instance.loadLevelFromXML("fruit-1-1");
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
            EntityManager.Instance.render();
            EnemyManager.Instance.render();
            LevelManager.Instance.render();
            ProjectileManager.Instance.render();
            ParticleManager.Instance.render();
            DebugManager.Instance.render();
            gui.render();

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

            //if (Keyboard.GetState().IsKeyDown(Keys.S))
            //{
            //    EditorHelper.Instance.saveLevelToXML("prueba1");
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.L))
            //{
            //    EditorHelper.Instance.loadLevelFromXML("E:/Proyectos/XNA/Naughty Invaders/MyGame/MyGame/bin/x86/Editor/Content/xml/levels/prueba1.xml");
            //}
        }
        
        public override void dispose()
        {
        }
    }
}