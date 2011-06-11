using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class StateOptions : GameState
    {
        public const int OPTIONS_X = -360;

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

        }

        public override void render()
        {
        }

        public override void dispose()
        {
        }
    }
}