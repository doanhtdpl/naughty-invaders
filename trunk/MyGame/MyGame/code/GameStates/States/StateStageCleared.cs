using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class StateStageCleared : GameState
    {
        public override void initialize()
        {
            transitionColor = new Color(30, 30, 30, 30);
            type = StateManager.tGS.EndStage;
            renderAlways = false;
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
