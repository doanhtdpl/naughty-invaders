using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Xml;
using System.IO;


namespace MyGame
{
    class StateMainMenu : GameState
    {
        Menu menu;

        void initializeMenu()
        {
            menu = new Menu(300.0f);
            Vector2 scale = new Vector2(1.5f, 0.9f);

            MenuElement mb1 = new MenuElement("", new Vector2(-20.0f, -120), scale);
            mb1.setFunction("startGame", MenuElement.tInputType.A);
            MenuElement mb2 = new MenuElement("", new Vector2(-20.0f, -185), scale);
            mb2.setFunction("goToCredits", MenuElement.tInputType.A);
            MenuElement mb3 = new MenuElement("", new Vector2(-20.0f, -260), scale);
            mb3.setFunction("exitToArcade", MenuElement.tInputType.A);
            
            //menu.menuTexts.Add(new MenuText("start game", new Vector2(-20, -100), 1.0f));
            //menu.menuTexts.Add(new MenuText("credits", new Vector2(-20, -170), 1.0f));
            //menu.menuTexts.Add(new MenuText("exit to arcade", new Vector2(-20, - 240), 1.0f));

            mb1.upNode = mb3;
            mb1.downNode = mb2;
            mb2.upNode = mb1;
            mb2.downNode = mb3;
            mb3.upNode = mb2;
            mb3.downNode = mb1;
            
            menu.menuElements.Add(mb1);
            menu.menuElements.Add(mb2);
            menu.menuElements.Add(mb3);
            menu.setCurrentNode(mb1);

            TransitionManager.Instance.addTransition(TransitionManager.tTransition.FadeOut, 1.0f, Color.Black);
        }

        public override void initialize()
        {
            SoundManager.Instance.loadXML();

            transitionColor = new Color(10, 10, 10, 255);
            transitionColor *= 0.85f;
            initializeMenu();
            renderAlways = false;

            SoundManager.Instance.playSong("Naughty_jingle", true);
        }

        public override void loadContent()
        {
            EditorHelper.Instance.loadNewLevelFromGame("menu");
            CameraManager.Instance.cameraMode = CameraManager.tCameraMode.WorldMap;
            CameraManager.Instance.setWorldMapParams(12, 17, 0.6f, 0.6f);
            GamerManager.getMainPlayer().renderState = RenderableEntity2D.tRenderState.NoRender;

            CameraManager.Instance.worldMapPosition = new Vector3(0, 150, -250);

            loaded = true;
        }

        public override void update()
        {
            base.update();

            LevelManager.Instance.update();
            ParticleManager.Instance.update();
            CameraManager.Instance.update();

            SoundManager.Instance.update();

            SB.cam.update();

            if (TransitionManager.Instance.isFading()) return;

            menu.update();
        }

        public override void render()
        {
            EntityManager.Instance.render();
            LevelManager.Instance.render();

            menu.render();

            GraphicsManager.Instance.spriteBatchBegin();
            "start game".renderNI(Screen.getXYfromCenter(0, -100), 1.6f, StringManager.tStyle.Border, new Color(119, 181, 223), new Color(124, 0, 62));
            "credits".renderNI(Screen.getXYfromCenter(0, -170), 1.6f, StringManager.tStyle.Border, new Color(119, 181, 223), new Color(124, 0, 62));
            "exit to arcade".renderNI(Screen.getXYfromCenter(0, -240), 1.6f, StringManager.tStyle.Border, new Color(119, 181, 223), new Color(124, 0, 62));
            GraphicsManager.Instance.spriteBatchEnd();
        }

        public override void dispose()
        {
            menu.clean();
        }
    }
}