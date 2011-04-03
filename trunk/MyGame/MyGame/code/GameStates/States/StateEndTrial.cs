using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class StateEndTrial : GameState
    {
        public static TEX texTrial = new TEX();
        public override void initialize()
        {
            type = StateManager.tGS.Credits;
        }

        public override void loadContent()
        {
            Menu.loadContent();
            loaded = true;
            //texTrial.initTEX("GUI/menu/trial", 800, 800);
        }

        public override void update()
        {
            base.update();
            GamerManager.updateTrialMessage();
            if (GamerManager.getMainControls().B_firstPressed())
            {
                // dequeue end trial and game states
                StateManager.dequeueState(2);
                StateManager.enqueueState(StateManager.tGS.Menu);
            }
            // if the gamer buys the game, go back to the end of the stage to save his progress
            if (!GamerManager.isTrial())
            {
                StateManager.dequeueState(1);
                StateManager.enqueueState(StateManager.tGS.EndStage);
            }
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatch.Begin();
            GraphicsManager.Instance.spriteBatch.Draw(texTrial.texture, Screen.getXYfromCenter(-550, 330), null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            TextKey.Trial.Translate().render
                (Screen.getXYfromCenter(new Vector2(250, 250)), 0.75f, Color.LightBlue, StringManager.tTextAlignment.Centered,
                SB.font, 500, 35, Color.Blue, 1.0f, new Vector2(1, 1), StringManager.tStyle.Border);
            GamerManager.renderTrialMessage(new Vector2(0, -160), 1.5f);
            TextKey.PressBToMenu.Translate().render(
                    Screen.getXYfromCenter(new Vector2(0, -230)), 0.9f, Color.Gray, StringManager.tTextAlignment.Centered,
                    SB.font, 1000, 0, Color.Black, 1.0f, new Vector2(2, 2), StringManager.tStyle.Shadowed);

            GraphicsManager.Instance.spriteBatch.End();
        }

        public override void dispose()
        {

        }
    }
}