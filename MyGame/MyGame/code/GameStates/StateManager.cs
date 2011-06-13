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
//#if ONLY_GAMEPLAY
#if EDITOR
            gameStates.Add(new StateGame());
//#else
//            gameStates.Add(new StateMainMenu());
//#endif
#else
            gameStates.Add(new StateIntro());
#endif
        }

        public void update()
        {
            GameState currentState = gameStates[gameStates.Count - 1];
            if (currentState.loaded)
            {
                currentState.update();
                TransitionManager.Instance.update();
            }
            else if (!currentState.loading)
            {
                currentState.loadContent();
                currentState.initialize();
                currentState.loaded = true;
            }
        }

        public void render()
        {
            GraphicsManager.Instance.graphicsDevice.Clear(Color.Black);
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
                        GraphicsManager.Instance.spriteBatch.Draw(TextureManager.Instance.getColoredTexture(Color.White), new Rectangle(-100, -100, 2000, 2000), color);
                        GraphicsManager.Instance.spriteBatch.End();
                    }
                    if (i == gameStates.Count -1 || gameStates[i].renderAlways)
                    {
                        gameStates[i].render();
                    }
                }
            }
            TransitionManager.Instance.render();
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

        public static void dequeueStates(int num)
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

        public enum tGameState { None, Intro, Prompt, Menu, Options, Credits, Scores, Pause, SkillsMenu, GameIntro, WorldMap, Game, KingTomato, EpilepticMacedonia, EndStage, EndTrial };
        public static void addState(tGameState stateType, string level = null)
        {
            GameState gameState;
            switch (stateType)
            {
                case tGameState.Intro:
                    gameState = new StateIntro();
                    break;
                case tGameState.Prompt:
                    gameState = new StatePrompt();
                    break;
                case tGameState.Menu:
                    gameState = new StateMainMenu();
                    break;
                case tGameState.Options:
                    gameState = new StateOptions();
                    break;
                case tGameState.Credits:
                    gameState = new StateCredits();
                    break;
                case tGameState.Scores:
                    gameState = new StateScores();
                    break;
                case tGameState.Pause:
                    gameState = new StatePausedGame();
                    break;
                case tGameState.SkillsMenu:
                    gameState = new StateSkillsMenu();
                    break;
                case tGameState.GameIntro:
                    gameState = new StateGameIntro();
                    break;
                case tGameState.WorldMap:
                    gameState = new StateWorldMap();
                    break;
                case tGameState.Game:
                    gameState = new StateGame(level);
                    break;
                case tGameState.KingTomato:
                    gameState = new MinigameKingTomato(level);
                    break;
                case tGameState.EpilepticMacedonia:
                    gameState = new MinigameEpilepticMacedonia(level);
                    break;
                case tGameState.EndStage:
                    gameState = new StateEndLevel();
                    break;
                default:
                    gameState = new StateIntro();
                    break;
            }
            gameState.type = stateType;
            gameStates.Add(gameState);
        }
    }
}