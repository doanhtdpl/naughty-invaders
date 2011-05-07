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

        void initializeMenu()
        {
            menu = new Menu();
            MenuElement mbHeader = new MenuElement("smallHeader", "", new Vector2(0,300));
            MenuElement mb1 = new MenuElement("option1", "", new Vector2(0, 150));
            mb1.setFunction("unpause", MenuElement.tInputType.A);
            MenuElement mb2 = new MenuElement("option2", "", new Vector2(0, 50));
            mb2.setFunction("", MenuElement.tInputType.A);
            MenuElement mb3 = new MenuElement("option3", "", new Vector2(0, -50));
            mb3.setFunction("exitGame", MenuElement.tInputType.A);
            mb3.scale2D = new Vector2(mb3.scale.X, mb3.scale.Y*0.8f);

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
        }

        public override void initialize()
        {
            transitionColor = new Color(100, 0, 20, 200);
            type = StateManager.tGS.Pause;
            initializeMenu();
            //SoundManager.playSound("pause");
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
                StateManager.dequeueState(1);
                //SoundManager.playSound("pause");
            }
            //else if (GamerManager.getMainControls().B_firstPressed())
            //{
            //    StateManager.clearStates();
            //    StateManager.enqueueState(StateManager.tGS.Menu);
            //    //SoundManager.playSound("pause");
            //}
        }

        public override void render()
        {
            menu.render();

            //GraphicsManager.Instance.spriteBatch.Begin();
            //Vector2 pos = Screen.getXYfromCenter(-180, 200);
            //GraphicsManager.Instance.spriteBatch.Draw(GUIManager.Instance.smallHeader, new Rectangle((int)pos.X, (int)pos.Y,360,160), Color.White);
            //pos.X += 20;
            //pos.Y += 120;
            //GraphicsManager.Instance.spriteBatch.Draw(GUIManager.Instance.option1, new Rectangle((int)pos.X, (int)pos.Y, 320, 100), Color.White);
            //pos.X -= 20;
            //pos.Y += 80;
            //GraphicsManager.Instance.spriteBatch.Draw(GUIManager.Instance.option2, new Rectangle((int)pos.X, (int)pos.Y, 320, 100), Color.White);
            //pos.X += 30;
            //pos.Y += 80;
            //GraphicsManager.Instance.spriteBatch.Draw(GUIManager.Instance.option3, new Rectangle((int)pos.X, (int)pos.Y, 320, 90), Color.White);
            //TextKey.Pause.Translate().render(
            //        Screen.getXYfromCenter(new Vector2(0, 125)), 1.4f, Color.Black, StringManager.tTextAlignment.Centered,
            //        SB.font, 1000, 0, Color.White, 1.0f, new Vector2(1, 1), StringManager.tStyle.Border);
            //TextKey.PressStartToCont.Translate().render(
            //        Screen.getXYfromCenter(new Vector2(0, 25)), 1.1f, Color.White, StringManager.tTextAlignment.Centered,
            //        SB.font, 1000, 0, Color.Black, 1.0f, new Vector2(1, 1), StringManager.tStyle.Border);
            //TextKey.PressBToMenu.Translate().render(
            //        Screen.getXYfromCenter(new Vector2(0, -25)), 1.1f, Color.Red, StringManager.tTextAlignment.Centered,
            //        SB.font, 1000, 0, Color.Black, 1.0f, new Vector2(1, 1), StringManager.tStyle.Border);
            //GraphicsManager.Instance.spriteBatch.End();
        }

        public override void dispose()
        {
        }
    }
}
