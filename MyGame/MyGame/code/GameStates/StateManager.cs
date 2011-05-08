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

        public const float TRANSITION_TIME = 0.3f;

        public StateManager()
        {
            // inicializamos el primer estado del juego
#if ONLY_GAMEPLAY
            gameStates.Add(new StateWorldMap());
            //gameStates.Add(new StateGame());
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
                        float opacy = ((float)gameStates[i].timeRunning / TRANSITION_TIME);
                        float maxOpacy = (float)gameStates[i].transitionColor.A / 255.0f;
                        if (opacy > maxOpacy)
                            opacy = maxOpacy;
                        Color color = gameStates[i].transitionColor;
                        color *= opacy;
                        GraphicsManager.Instance.spriteBatch.Begin();
                        GraphicsManager.Instance.spriteBatch.Draw(TextureManager.Instance.getColoredTexture(Color.White), new Rectangle(-2000, -2000, 4000, 4000), color);
                        GraphicsManager.Instance.spriteBatch.End();
                    }
                    gameStates[i].render();
                }
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

        public enum tGS { Intro, Prompt, Menu, Options, Credits, Scores, Pause, WorldMap, Game, EndStage, EndTrial };
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