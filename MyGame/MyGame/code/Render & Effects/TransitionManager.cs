using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class TransitionManager
    {
        static TransitionManager instance = null;
        TransitionManager()
        {
            value = 0;
        }
        public static TransitionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransitionManager();
                }
                return instance;
            }
        }

        Color color;
        float value;
        public enum tTransition { FadeIn, FadeOut, None }
        tTransition type;
        float initialTime = 0.0f;
        float time = 0.0f;

        // specific for level loading
        string level = null;
        WorldMapLocation.tLocationType locationType;
        // specific for changing state
        StateManager.tGameState state = StateManager.tGameState.None;
        int clearStates = 0;

        public void fadeIn()
        {
            addTransition(tTransition.FadeIn, 1.0f, Color.Black);
        }

        public void fadeOut()
        {
            addTransition(tTransition.FadeOut, 1.0f, Color.Black);
        }

        public void addTransition(tTransition type, float transitionTime, Color transitionColor)
        {
            this.type = type;
            this.time = transitionTime;
            this.initialTime = this.time;
            this.color = transitionColor;
        }

        public void loadLevelWithFade(string level, WorldMapLocation.tLocationType locationType, float fadeTime, Color fadeColor)
        {
            this.type = tTransition.FadeIn;
            this.time = fadeTime;
            this.initialTime = this.time;
            this.level = level;
            this.locationType = locationType;
            this.color = fadeColor;
        }

        public void changeStateWithFade( StateManager.tGameState state, int clearStates, string level, float fadeTime, Color fadeColor)
        {
            this.state = state;
            this.clearStates = clearStates;
            this.type = tTransition.FadeIn;
            this.time = fadeTime;
            this.initialTime = this.time;
            this.level = level;
            this.color = fadeColor;
        }

        public void updateSpecialFade()
        {
            if (time < 0.0f)
            {
                if (state != StateManager.tGameState.None)
                {
                    StateManager.dequeueStates(clearStates);
                    StateManager.addState(state, level);
                }
                else
                {
                    StateManager.dequeueStates(1);
                    switch (locationType)
                    {
                        case WorldMapLocation.tLocationType.Arcade:
                            StateManager.addState(StateManager.tGameState.Game, level);
                            break;
                        case WorldMapLocation.tLocationType.KingTomato:
                            StateManager.addState(StateManager.tGameState.KingTomato, level);
                            break;
                        case WorldMapLocation.tLocationType.EpilepticMacedonia:
                            StateManager.addState(StateManager.tGameState.EpilepticMacedonia, level);
                            break;
                    }
                }
                level = null;
                state = StateManager.tGameState.None;
            }
        }

        public bool isFading()
        {
            return time > 0;
        }

        public void update()
        {
            time -= SB.dt;

            if (level != null || state != StateManager.tGameState.None)
            {
                updateSpecialFade();
            }

            switch (type)
            {
                case tTransition.FadeIn:
                    value = Math.Min(1.0f, (initialTime - time) / initialTime);
                    break;
                case tTransition.FadeOut:
                    value = Math.Max(0.0f, time / initialTime);
                    break;
                default:
                    break;
            }

            if (time < 0.0f)
            {
                type = tTransition.None;
            }
        }

        public void render()
        {
            GraphicsManager.Instance.spriteBatch.Begin();
            GraphicsManager.Instance.spriteBatch.Draw(TextureManager.Instance.getColoredTexture(Color.White), new Rectangle(-1000, -1000, 4000, 4000), color * value);
            GraphicsManager.Instance.spriteBatch.End();
        }
    }
}
