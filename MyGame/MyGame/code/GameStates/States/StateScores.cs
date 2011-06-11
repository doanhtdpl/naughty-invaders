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

        public override void initialize()
        {
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
                StateManager.dequeueStates(1);
            }
            if (GamerManager.getMainControls().B_firstPressed())
            {
                StateManager.dequeueStates(1);
            }
        }

        public override void render()
        {
        }

        public override void dispose()
        {
        }
    }
}