using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using System.Xml;
using System.IO;

namespace MyGame
{
    class StateGameIntro : GameState
    {
        public float timer = 0;

        enum tAshState { Cry, Walk, Candy1, Candy2 };
        tAshState state;
        VideoPlayer player;
        Video video;
        Texture2D videoTexture;

        public const int introTime = 3;

        AnimatedEntity2D ash;

        public override void initialize()
        {
            TransitionManager.Instance.addTransition(TransitionManager.tTransition.FadeOut, 1.0f, Color.Black);
        }

        public override void loadContent()
        {
            EditorHelper.Instance.loadNewLevelFromGame("intro");
            GamerManager.getMainPlayer().renderState = RenderableEntity2D.tRenderState.NoRender;
            GamerManager.getMainPlayer().mode = Player.tMode.SavingItems;

            ash = new AnimatedEntity2D("intro", "ash", new Vector3(0, 0, 0), 0, Color.White);
            LevelManager.Instance.addAnimatedProp(ash);
            ash.playAction("idle");
            state = tAshState.Cry;

            //player = new VideoPlayer();
            //video = SB.content.Load<Video>("video/Wildlife");
        }

        public override void update()
        {
            //if (player.State == MediaState.Stopped)
            //{
            //    player.IsLooped = true;
            //    player.Play(video);
            //}

            timer += SB.dt;
            switch (state)
            {
                case tAshState.Cry:
                    if (timer > 3)
                    {
                        ash.playAction("walk");
                        timer = 0;
                        state = tAshState.Walk;
                    }
                    break;
                case tAshState.Walk:
                    if (timer > 3)
                    {
                        ash.playAction("candy1");
                        timer = 0;
                        state = tAshState.Candy1;
                    }
                    break;
                case tAshState.Candy1:
                    if (ash.getCurrentAction() == "candy2")
                    {
                        timer = 0;
                        state = tAshState.Candy2;
                    }
                    break;
                case tAshState.Candy2:
                    if (timer > introTime && !TransitionManager.Instance.isFading())
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

            //if (player.State != MediaState.Stopped)
            //    videoTexture = player.GetTexture();

            //// Draw the video, if we have a texture to draw.
            //if (videoTexture != null)
            //{
            //    GraphicsManager.Instance.spriteBatchBegin();
            //    videoTexture.render2D(Vector2.Zero, new Vector2(1280, 720), Color.White);
            //    GraphicsManager.Instance.spriteBatchEnd();
            //}
        }

        public override void dispose()
        {

        }
    }
}