using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class StateCredits : GameState
    {
        //public static TEX mainMenu = new TEX();
        public override void initialize()
        {
            type = StateManager.tGS.Credits;
        }

        public override void loadContent()
        {
            loaded = true;
        }

        public override void update()
        {
            base.update();

            if (GamerManager.getMainControls().A_firstPressed())
            {
                StateManager.dequeueState(1);
            }
            if (GamerManager.getMainControls().B_firstPressed())
            {
                StateManager.dequeueState(1);
            }
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatch.Begin();
            GraphicsManager.Instance.spriteBatch.End();
        }

        public override void dispose()
        {

        }
    }
}