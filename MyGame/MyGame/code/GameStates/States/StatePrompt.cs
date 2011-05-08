using System.Xml;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;


namespace MyGame
{
    class StatePrompt : GameState
    {
        public enum tPromptState { PressA, SigningIn, Loading, Ready }
        static tPromptState promptState;
        PlayerIndex indexWhoPrompted;

        public override void initialize()
        {
            type = StateManager.tGS.Prompt;
            GraphicsManager.Instance.initializeRender();

            // si alguien cierra la sesión podemos llegar aquí desde cualquier estado...
            promptState = tPromptState.PressA;
            SoundManager.stopMusic();
        }
        public void initializeAfterLoading()
        {
            SoundManager.playMusic("menuSong");
        }

        public override void loadContent()
        {
            loaded = true;
        }

        public override void update()
        {
            base.update();

            switch (promptState)
            {
                case tPromptState.PressA:
                    for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
                    {
                        if (GamePad.GetState(index).Buttons.Start == ButtonState.Pressed || GamePad.GetState(index).Buttons.A == ButtonState.Pressed)
                        {
                            indexWhoPrompted = index;
                            if (Gamer.SignedInGamers[index] == null)
                            {
                                promptState = tPromptState.SigningIn;
                            }
                            else
                            {
                                //SaveGameManager.loadPlayerData(index);
                                promptState = tPromptState.Loading;
                                GamerManager.createGamerEntity(index, true);
                            }
                            break;
                        }
                    }
                    break;
                case tPromptState.SigningIn:
                    if (Gamer.SignedInGamers[indexWhoPrompted] == null && !Guide.IsVisible)
                    {
                        Guide.ShowSignIn(1, false);
                    }
                    if (Gamer.SignedInGamers[indexWhoPrompted] != null && !Guide.IsVisible)
                    {
                        //if (SaveGameManager.loadPlayerData(indexWhoPrompted))
                        //{
                        //    GamerManager.createGamerEntity(indexWhoPrompted, true);
                        //    promptState = tPromptState.Loading;
                        //}
                    }
                    break;
                case tPromptState.Loading:
                    //if (SaveGameManager.infoLoaded)
                    {
                        promptState = tPromptState.Ready;
                    }
                    break;
                case tPromptState.Ready:
                    SoundManager.initVolumes();
                    initializeAfterLoading();
                    StateManager.clearStates();
                    StateManager.gameStates.Add(new StateMainMenu());
                    break;
            }
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatch.Begin();
            if (promptState == tPromptState.PressA)
            {
                TextKey.PressAToStart.Translate().renderSC
                    (Screen.getXYfromCenter(0, 0), 1.5f, Color.GreenYellow, Color.Black, StringManager.tTextAlignment.Centered);
            }
            GraphicsManager.Instance.spriteBatch.End();
        }

        public override void dispose()
        {

        }
    }
}