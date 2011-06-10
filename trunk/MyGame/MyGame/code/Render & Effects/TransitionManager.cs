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
        public enum tTransition { FadeIn, FadeOut }
        tTransition type;
        float initialTime = 0.0f;
        float time = 0.0f;

        // specific for level loading
        string level = null;
        WorldMapLocation.tLocationType locationType;
        bool loadLevel = false;

        public void addTransition(tTransition type, float transitionTime, Color transitionColor)
        {
            this.type = type;
            this.initialTime = transitionTime;
            this.time = transitionTime;
            this.color = transitionColor;
        }

        public void loadLevelWithFade(string level, WorldMapLocation.tLocationType locationType, float fadeTime, Color fadeColor)
        {
            this.type = tTransition.FadeIn;
            this.loadLevel = true;
            this.initialTime = fadeTime;
            this.time = fadeTime;
            this.level = level;
            this.locationType = locationType;
            this.color = fadeColor;
        }

        public void updateLoadLevel()
        {
            if (time < 0.0f)
            {
                loadLevel = false;

                StateManager.dequeueState(1);
                switch (locationType)
                {
                    case WorldMapLocation.tLocationType.Arcade:
                        StateManager.gameStates.Add(new StateGame(level));
                        break;
                    case WorldMapLocation.tLocationType.KingTomato:
                        StateManager.gameStates.Add(new MinigameKingTomato(level));
                        break;
                    case WorldMapLocation.tLocationType.EpilepticMacedonia:
                        StateManager.gameStates.Add(new MinigameEpilepticMacedonia(level));
                        break;
                }
            }
        }

        public void update()
        {
            time -= SB.dt;

            if (loadLevel)
            {
                updateLoadLevel();
            }

            if (time > 0.0f)
            {
                
                switch (type)
                {
                    case tTransition.FadeIn:
                        value = initialTime - (time / initialTime);
                        break;
                    case tTransition.FadeOut:
                        value = time / initialTime;
                        break;
                    default:
                        break;
                }
            }
        }

        public void render()
        {
            if (time > 0.0f)
            {
                GraphicsManager.Instance.spriteBatch.Begin();
                switch(type)
                {
                    case tTransition.FadeIn:
                        GraphicsManager.Instance.spriteBatch.Draw(TextureManager.Instance.getColoredTexture(Color.White), new Rectangle(-1000, -1000, 4000, 4000), color * value);
                    break;
                    case tTransition.FadeOut:
                    GraphicsManager.Instance.spriteBatch.Draw(TextureManager.Instance.getColoredTexture(Color.White), new Rectangle(-1000, -1000, 4000, 4000), color * value);
                    break;
                    default:
                    break;
                }
                GraphicsManager.Instance.spriteBatch.End();
            }
        }
    }
}
