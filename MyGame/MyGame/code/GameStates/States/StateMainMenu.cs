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
        public const int MENU_X = 160;
        public const int MENU_Y = 250;
        public const int INTER_Y = 80;

        public override void initialize()
        {
            type = StateManager.tGS.Menu;
        }
        public void initializeMenu()
        {
           
        }

        public override void loadContent()
        {
            loaded = true;
        }

        public override void update()
        {
            base.update();
            GamerManager.updateTrialMessage();
        }

        public override void render()
        {
            GamerManager.renderTrialMessage(new Vector2(0, -190), 1.3f);
        }

        public override void dispose()
        {

        }
    }
}