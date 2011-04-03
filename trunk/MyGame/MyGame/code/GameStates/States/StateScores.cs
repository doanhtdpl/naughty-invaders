using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class StateScores : GameState
    {
        public const int TOTAL_SCORE_PAGES = 2;

        public static TEX award = new TEX();
        Color normalColor = Color.LightGray;
        Color gotColor = Color.White;
        Menu menu = new Menu(1f, Color.Gray, Color.LightGray, Color.Black, Color.White);

        //public static TEX mainMenu = new TEX();
        public override void initialize()
        {
            type = StateManager.tGS.Scores;
        }

        public override void loadContent()
        {
            Menu.loadContent();
            loaded = true;
        }

        public override void update()
        {
            base.update();
            menu.update();

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
            menu.render();
            GraphicsManager.Instance.spriteBatch.End();
        }

        public override void dispose()
        {

        }
    }
}