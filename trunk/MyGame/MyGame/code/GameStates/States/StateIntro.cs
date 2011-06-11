using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Xml;
using System.IO;

namespace MyGame
{
    class StateIntro : GameState
    {
        Texture2D logo;
        public float timer = 0;

        public const int introTime = 3;

        public override void initialize()
        {
            TransitionManager.Instance.addTransition(TransitionManager.tTransition.FadeOut, 1.0f, Color.Black);
        }

        public override void loadContent()
        {
            EditorHelper.Instance.loadNewLevelFromGame("logo");
            GamerManager.getMainPlayer().renderState = RenderableEntity2D.tRenderState.NoRender;
        }

        public override void update()
        {
            timer += SB.dt;
            if (timer > introTime && !TransitionManager.Instance.isFading())
            {
                TransitionManager.Instance.changeStateWithFade(StateManager.tGameState.Menu, 1, null, 0.5f, Color.Black);
            }

#if DEBUG
            if (ControlPadManager.Instance.controlPads[0].A_firstPressed()
                || ControlPadManager.Instance.controlPads[0].X_firstPressed()
                || ControlPadManager.Instance.controlPads[0].B_firstPressed()
                || ControlPadManager.Instance.controlPads[0].Y_firstPressed())
            {
                StateManager.clearStates();
                StateManager.addState(StateManager.tGameState.WorldMap);
            }
#endif
            LevelManager.Instance.update();
            ParticleManager.Instance.update();
            CameraManager.Instance.update();
        }

        public override void render()
        {
            GraphicsManager.Instance.graphicsDevice.Clear(SB.BGColor);

            EntityManager.Instance.render();
            LevelManager.Instance.render();
        }

        public override void dispose()
        {

        }
    }
}