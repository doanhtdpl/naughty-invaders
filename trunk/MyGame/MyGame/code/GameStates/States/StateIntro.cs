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
        public static Texture logo;
        public float timer = 0;

        public const int introTime = 1000;

        public override void initialize()
        {
        }

        public override void loadContent()
        {
            logo = TextureManager.Instance.getTexture("GUI/menu/logo");
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
            GraphicsManager.Instance.graphicsDevice.Clear(Color.White);
            //GraphicsManager.Instance.beginAlphaRender();
            logo.render(SB.getWorldMatrix(new Vector3(0.0f, 0.0f, 0.0f), 0.0f, 0.0f));
        }

        public override void dispose()
        {

        }
    }
}