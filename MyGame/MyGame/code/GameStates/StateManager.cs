using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace MyGame
{
    public class StateManager
    {
        public static List<GameState> gameStates = new List<GameState>();

        public const int TRANSITION_TIME = 300;

        public StateManager()
        {
            // inicializamos el primer estado del juego
#if ONLY_GAMEPLAY
            gameStates.Add(new StateGame());
#else
            gameStates.Add(new StateIntro());
#endif

        }

        public void update()
        {
            GameState currentState = gameStates[gameStates.Count - 1];
            if (currentState.loaded)
                currentState.update();
            else if (!currentState.loading)
            {
                currentState.initialize();
                currentState.loadContent();
                currentState.loaded = true;
            }
        }

        public void render()
        {
            for (int i = 0; i < gameStates.Count; i++)
            {
                if (gameStates[i].loaded)
                {
                    // si hay más de un estado se pinta una pantalla oscura encima de los de debajo
                    if (gameStates.Count > 1 && i == gameStates.Count - 1)
                    {
                        float dark = gameStates[i].darkTransition*((float)gameStates[i].timeRunning / (float)TRANSITION_TIME);
                        if (dark > gameStates[i].darkTransition)
                            dark = gameStates[i].darkTransition;
                        SB.spriteBatch.Begin();
                        SB.spriteBatch.Draw(TextureManager.Instance.getColoredTexture(Color.White), new Rectangle(-2000, -2000, 4000, 4000), new Color(211.0f * 0.00392f, 240.0f * 0.00392f, 13.0f * 0.00392f, dark));
                        SB.spriteBatch.End();
                    }
                    gameStates[i].render();
                }
                //else if (gameStates[i].longLoad)
                //    renderLoadingScreen();
            }
        }

        public static void clearStates()
        {
            foreach (GameState gs in gameStates)
            {
                gs.dispose();
            }
            gameStates.Clear();
        }

        public static GameState getCurrentState()
        {
            return gameStates[gameStates.Count - 1];
        }

        #region State definitions
        public enum tGS { Intro, Prompt, Menu, Options, Credits, Scores, Pause, Game, EndStage, EndTrial };

        #endregion
        public static void enqueueState(tGS state)
        {
            switch (state)
            {
                case tGS.Intro:
                    gameStates.Add(new StateIntro());
                    break;
                case tGS.Prompt:
                    clearStates();
                    gameStates.Add(new StatePrompt());
                    break;
                case tGS.Menu:
                    clearStates();
                    gameStates.Add(new StateMainMenu());
                    break;
                case tGS.Options:
                    gameStates.Add(new StateOptions());
                    break;
                case tGS.Scores:
                    gameStates.Add(new StateScores());
                    break;
                case tGS.Credits:
                    gameStates.Add(new StateCredits());
                    break;
                case tGS.Pause:
                    gameStates.Add(new StatePausedGame());
                    break;
                case tGS.EndTrial:
                    gameStates.Add(new StateEndTrial());
                    break;
            }
        }
        public static void dequeueState(int num)
        {
            for (int i = 0; i < num; i++)
            {
                if (gameStates.Count > 0)
                {
                    gameStates[gameStates.Count - 1].dispose();
                    gameStates.RemoveAt(gameStates.Count - 1);
                }
                else
                    return;
            }
        }
        public static void dequeueUntil(tGS state)
        {
            while(true)
            {
                if (gameStates[gameStates.Count - 1].type == state)
                    return;
                else
                {
                    gameStates[gameStates.Count - 1].dispose();
                    gameStates.RemoveAt(gameStates.Count - 1);
                }
            }
        }
    }
}