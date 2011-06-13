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
    class StateGameIntro : GameState
    {
        public float timer = 0;

        public const int introTime = 3;

        AnimatedEntity2D ashcry;

        public override void initialize()
        {
            TransitionManager.Instance.addTransition(TransitionManager.tTransition.FadeOut, 1.0f, Color.Black);
        }

        public override void loadContent()
        {
            EditorHelper.Instance.loadNewLevelFromGame("intro");
            GamerManager.getMainPlayer().renderState = RenderableEntity2D.tRenderState.NoRender;
            GamerManager.getMainPlayer().mode = Player.tMode.SavingItems;

            ashcry = new AnimatedEntity2D("intro", "ash", new Vector3(0, 0, 0), 0, Color.White);
            LevelManager.Instance.addAnimatedProp(ashcry);
        }

        public override void update()
        {
#if DEBUG
            if (GamerManager.getMainControls().A_firstPressed())
            {
                TransitionManager.Instance.changeStateWithFade(StateManager.tGameState.WorldMap, 1, null, 0.2f, Color.Black);
            }
#endif
            timer += SB.dt;
            if (timer > introTime && !TransitionManager.Instance.isFading())
            {
                CameraManager.Instance.getCurrentNode().setLinkedNode(CameraManager.Instance.getNodes().getNodeAt(1));
                CameraManager.Instance.getCurrentNode().value.speed = 1400;
                CameraManager.Instance.setCurrentNode(CameraManager.Instance.getCurrentNode());
                TransitionManager.Instance.changeStateWithFade(StateManager.tGameState.WorldMap, 1, null, 0.8f, Color.Black);
            }

            LevelManager.Instance.update();
            ParticleManager.Instance.update();
            CameraManager.Instance.update();
            SB.cam.update();
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