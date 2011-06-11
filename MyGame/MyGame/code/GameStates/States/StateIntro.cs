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
        Texture2D logo;
        public float timer = 0;
        bool fade = false;

        public const int introTime = 3;

        public override void initialize()
        {
            TransitionManager.Instance.addTransition(TransitionManager.tTransition.FadeOut, 1.0f, Color.Black);
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
                if (!fade)
                {
                    TransitionManager.Instance.addTransition(TransitionManager.tTransition.FadeIn, 1.0f, Color.Black);
                    fade = true;
                }
                else if (!TransitionManager.Instance.isFading())
                {
                    StateManager.clearStates();

                    //StateManager.gameStates.Add(new StatePrompt());
                    StateManager.addState(StateManager.tGameState.Menu);
                }
            }

#if DEBUG
            if (ControlPadManager.Instance.controlPads[0].A_firstPressed()
                || ControlPadManager.Instance.controlPads[0].X_firstPressed()
                || ControlPadManager.Instance.controlPads[0].B_firstPressed()
                || ControlPadManager.Instance.controlPads[0].Y_firstPressed())
            {
                StateManager.clearStates();
                StateManager.addState(StateManager.tGameState.WorldMap);
            }
#endif
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