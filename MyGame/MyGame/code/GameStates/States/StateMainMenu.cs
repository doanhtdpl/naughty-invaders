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
        public static TEX mainMenu = new TEX();
        Menu menu = new Menu(1.2f, Color.DarkGreen, Color.Green, Color.Black);

        public const int MENU_X = 160;
        public const int MENU_Y = 250;
        public const int INTER_Y = 80;

        public override void initialize()
        {
            type = StateManager.tGS.Menu;
            initializeMenu();
            SoundManager.playMusic("menuSong");
        }
        public void initializeMenu()
        {
            //if (GamerManager.getMainControls().saveData.stagesPassed[0])
            //{
            //    menu.options.Add(new Option(TextKey.Continue.Translate(), TextKey.ContinueDesc.Translate(), GUI.getXYfromCenter(MENU_X, MENU_Y), Option.TO_SELECT_LEVEL));
            //}
            //else
            //{
            //    menu.options.Add(new Option(TextKey.NewGame.Translate(), TextKey.NewGameDesc.Translate(), GUI.getXYfromCenter(MENU_X, MENU_Y), Option.TO_GAME));
            //}
            //menu.options.Add(new Option(TextKey.Scores.Translate(), TextKey.ScoresDesc.Translate(), GUI.getXYfromCenter(MENU_X, MENU_Y - INTER_Y), Option.TO_SCORES));
            //menu.options.Add(new Option(TextKey.Options.Translate(), TextKey.OptionsDesc.Translate(), GUI.getXYfromCenter(MENU_X, MENU_Y - (INTER_Y * 2)), Option.TO_OPTIONS));
            //menu.options.Add(new Option(TextKey.Credits.Translate(), TextKey.CreditsDesc.Translate(), GUI.getXYfromCenter(MENU_X, MENU_Y - (INTER_Y * 3)), Option.TO_CREDITS));
        }

        public override void loadContent()
        {
            loaded = true;
        }

        public override void update()
        {
            base.update();
            menu.update();
            GamerManager.updateTrialMessage();
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatch.Begin();
            menu.render();
            GamerManager.renderTrialMessage(new Vector2(0, -190), 1.3f);
            GraphicsManager.Instance.spriteBatch.End();
        }

        public override void dispose()
        {

        }
    }
}