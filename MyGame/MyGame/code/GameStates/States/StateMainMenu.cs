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
        Texture2D logo;
        bool fade;

        void initializeMenu()
        {
            menu = new Menu(300.0f);
            Vector2 scale = new Vector2(1.5f, 0.9f);

            MenuElement mb1 = new MenuElement("", new Vector2(-20.0f, -100), scale);
            mb1.setFunction("startGame", MenuElement.tInputType.A);
            MenuElement mb2 = new MenuElement("", new Vector2(-20.0f, -170), scale);
            mb2.setFunction("goToCredits", MenuElement.tInputType.A);
            MenuElement mb3 = new MenuElement("", new Vector2(-20.0f, -230), scale);
            mb3.setFunction("exitToArcade", MenuElement.tInputType.A);
            
            menu.menuTexts.Add(new MenuText("start game", new Vector2(0, -70), 1.4f));
            menu.menuTexts.Add(new MenuText("credits", new Vector2(0, -140), 1.4f));
            menu.menuTexts.Add(new MenuText("exit to arcade", new Vector2(0, -210), 1.4f));

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

            GamerManager.createGamerEntity(PlayerIndex.One, true);

            TransitionManager.Instance.addTransition(TransitionManager.tTransition.FadeOut, 1.0f, Color.Black);

            fade = false;
        }

        public override void initialize()
        {
            transitionColor = new Color(10, 10, 10, 255);
            transitionColor *= 0.85f;
            type = StateManager.tGS.Menu;
            initializeMenu();
            renderAlways = false;
        }


        public override void loadContent()
        {
            logo = TextureManager.Instance.getTexture("GUI/menu/logo_NI");

            loaded = true;
        }

        public override void update()
        {
            base.update();
            //GamerManager.updateTrialMessage();

            ControlPad cp = GamerManager.getMainControls();

            if (cp.A_firstPressed() && !fade)
            {
                TransitionManager.Instance.fadeIn();
                fade = true;
            }

            //if (!TransitionManager.Instance.isFading())
            {
                menu.update();
            }
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatchBegin();
            logo.render2D(new Vector2(0, 120), new Vector2(logo.Width/2, logo.Height/2), Color.White);
            GraphicsManager.Instance.spriteBatchEnd();

            menu.render();
            //GamerManager.renderTrialMessage(new Vector2(0, -190), 1.3f);
        }

        public override void dispose()
        {
            menu.clean();
        }
    }
}