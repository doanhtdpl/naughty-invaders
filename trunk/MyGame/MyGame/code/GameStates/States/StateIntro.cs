using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Xml;
using System.IO;

namespace MyGame
{
    class StateIntro : GameState
    {
        public static TEX logo = new TEX();
        public float timer = 0;

        public const int introTime = 1000;

        public override void initialize()
        {
        }

        public override void loadContent()
        {
            logo.initTEX("GUI/menu/logo", 512, 512);
        }

        public override void update()
        {
            timer += SB.dt;
            if (timer < 3000/*introTime*/)
            {
            }
            else
            {
                StateManager.clearStates();
                StateManager.enqueueState(StateManager.tGS.Prompt);
            }
        }

        public override void render()
        {
            SB.graphicsDevice.Clear(Color.White);
            SB.beginAlphaRender();
            logo.render(new Vector2(0,0), 0);
        }

        public override void dispose()
        {

        }
    }
}