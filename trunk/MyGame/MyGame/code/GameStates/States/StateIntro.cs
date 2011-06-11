﻿using System;
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
        Texture2D logo;
        public float timer = 0;

        public const int introTime = 3;

        public override void initialize()
        {
        }

        public override void loadContent()
        {
            logo = TextureManager.Instance.getTexture("GUI/menu/logo_UG");
        }

        public override void update()
        {
            timer += SB.dt;
            if (timer < introTime)
            {
            }
            else
            {
                StateManager.clearStates();
                //StateManager.gameStates.Add(new StatePrompt());
                StateManager.gameStates.Add(new StateMainMenu());
            }
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatchBegin();
            logo.render2D(new Vector2(0,0), new Vector2(583, 557), Color.White);
            GraphicsManager.Instance.spriteBatchEnd();
        }

        public override void dispose()
        {

        }
    }
}