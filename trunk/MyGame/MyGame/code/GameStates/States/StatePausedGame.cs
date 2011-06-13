using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class StatePausedGame : GameState
    {
        Menu menu;
        Texture2D bg;

        void initializeMenu()
        {
            menu = new Menu(250.0f);
            Vector2 scale = new Vector2(0.8f, 0.8f);
            MenuElement mbHeader = new MenuElement("smallHeader", new Vector2(0, 200), scale);
            MenuElement mb1 = new MenuElement("option1", new Vector2(0, 50), scale);
            mb1.setFunction("unpause", MenuElement.tInputType.A);
            MenuElement mb2 = new MenuElement("option2", new Vector2(0, -50), scale);
            mb2.setFunction("goToSkillsMenu", MenuElement.tInputType.A);
            MenuElement mb3 = new MenuElement("option3", new Vector2(0, -150), scale);
            mb3.setFunction("exitGame", MenuElement.tInputType.A);
            mb3.scale = new Vector2(mb3.scale.X, mb3.scale.Y*0.8f);

            //menu.menuTexts.Add(new MenuText("Paused game", new Vector2(0, 230), 1.2f));
            //menu.menuTexts.Add(new MenuText("Continue game", new Vector2(20, 70), 1.0f));
            //menu.menuTexts.Add(new MenuText("Skills", new Vector2(15, -25), 1.0f));
            //menu.menuTexts.Add(new MenuText("Exit game", new Vector2(5, -130), 1.0f));
            //menu.menuTexts.Add(new MenuText("Press ::B to go back", new Vector2(250, -230), 1.0f));

            mb1.upNode = mb3;
            mb1.downNode = mb2;
            mb2.upNode = mb1;
            mb2.downNode = mb3;
            mb3.upNode = mb2;
            mb3.downNode = mb1;
            menu.menuElements.Add(mbHeader);
            menu.menuElements.Add(mb1);
            menu.menuElements.Add(mb2);
            menu.menuElements.Add(mb3);
            menu.setCurrentNode(mb1);

            bg = TextureManager.Instance.getTexture("GUI/menu", "pausescreen-35");
        }

        public override void initialize()
        {
            transitionColor = new Color(0, 0, 0, 200);
            initializeMenu();
            renderAlways = false;
            SoundManager.Instance.playEffect("pause");
        }

        public override void loadContent()
        {
            loaded = true;
        }

        public override void update()
        {
            base.update();

            menu.update();

            // unpause with start or B button
            if (GamerManager.getMainControls().Start_firstPressed() || GamerManager.getMainControls().B_firstPressed())
            {
                StateManager.dequeueStates(1);
                SoundManager.Instance.playEffect("pause");
            }
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatchBegin();
            Color color = Color.White;
            color.A = (byte)(255 * Math.Min(1, (timeRunning / 0.5f)));
            QuadRenderer.render2D(bg, new Vector2(0, 0), new Vector2(1285, 740), color);
            GraphicsManager.Instance.spriteBatchEnd();

            menu.render();

            GraphicsManager.Instance.spriteBatchBegin();
            TextKey.gamePaused.Translate().renderNI(Screen.getXYfromCenter(0, 230), 1.2f, StringManager.tStyle.Border);
            TextKey.continueGame.Translate().renderNI(Screen.getXYfromCenter(20, 70), 1.0f, StringManager.tStyle.Shadowed);
            TextKey.skills.Translate().renderNI(Screen.getXYfromCenter(15, -25), 1.0f, StringManager.tStyle.Shadowed);
            TextKey.backToMap.Translate().renderNI(Screen.getXYfromCenter(5, -130), 1.0f, StringManager.tStyle.Shadowed);
            TextKey.pressBBackToGame.Translate().renderNI(Screen.getXYfromCenter(250, -230), 1.0f, StringManager.tStyle.Shadowed);
            GraphicsManager.Instance.spriteBatchEnd();
        }

        public override void dispose()
        {
            menu.clean();
        }
    }
}
