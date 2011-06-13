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

        enum tAshState { Cry, Idle, Candy };
        tAshState state;

        AnimatedEntity2D ash = null, ashTears = null;

        public override void initialize()
        {
            TransitionManager.Instance.addTransition(TransitionManager.tTransition.FadeOut, 1.0f, Color.Black);
        }

        public override void loadContent()
        {
            EditorHelper.Instance.loadNewLevelFromGame("intro");
            GamerManager.getMainPlayer().renderState = RenderableEntity2D.tRenderState.NoRender;
            GamerManager.getMainPlayer().mode = Player.tMode.SavingItems;

            foreach (AnimatedEntity2D ent in LevelManager.Instance.getAnimatedProps())
            {
                if (ent.entityName == "ash")
                    ash = ent;
                if (ent.entityName == "ashTears")
                    ashTears = ent;
                if (ash != null && ashTears != null)
                    break;
            }

            ash.playAction("cry");
            ashTears.playAction("tearsLoop");
            ashTears.renderState = RenderableEntity2D.tRenderState.NoRender;
            state = tAshState.Cry;
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
            switch(state)
            {
                case tAshState.Cry:
                    if (timer > 4)
                    {
                        state = tAshState.Idle;
                        ash.playAction("cryEnd");
                        ashTears.playAction("tearsEnd");
                        timer = 0;
                    }
                    break;

                case tAshState.Idle:
                    if (timer > 1)
                    {
                        state = tAshState.Candy;
                        ash.playAction("sweet");
                        ashTears.renderState = RenderableEntity2D.tRenderState.NoRender;
                        timer = 0;
                    }
                    break;

                case tAshState.Candy:
                    if (timer > 5 && !TransitionManager.Instance.isFading())
                    {
                        CameraManager.Instance.getCurrentNode().setLinkedNode(CameraManager.Instance.getNodes().getNodeAt(1));
                        CameraManager.Instance.getCurrentNode().value.speed = 1400;
                        CameraManager.Instance.setCurrentNode(CameraManager.Instance.getCurrentNode());
                        TransitionManager.Instance.changeStateWithFade(StateManager.tGameState.WorldMap, 1, null, 0.8f, Color.Black);
                    }
                    break;
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

            if (state == tAshState.Candy)
            {
                Color color = Color.Black;
                if (timer < 1)
                    color.A = 0;
                else if (timer < 2)
                    color.A = (byte)((timer - 1) * 255);
                else if (timer < 4)
                    color.A = 255;
                else if (timer < 5)
                    color.A = (byte)(Math.Max(0, (5 - timer)) * 255);
                else
                    color.A = 0;

                GraphicsManager.Instance.spriteBatchBegin();
                "i hope my wish comes true".renderNI(Screen.getXYfromCenter(-100, 0), 1.0f, StringManager.tStyle.Normal, color, color);
                GraphicsManager.Instance.spriteBatchEnd();
            }
        }

        public override void dispose()
        {

        }
    }
}