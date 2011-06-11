using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class StateEndLevel : GameState
    {
        float TIME = 5;

        Texture2D level;
        float time, levelFactor;
        bool fade;

        public override void initialize()
        {
            time = TIME;
            fade = false;

            transitionColor = new Color(0, 0, 0, 1);
        }

        public override void loadContent()
        {
            level = TextureManager.Instance.getTexture("GUI/ingame/complete-75");

            Vector3 position = CameraManager.Instance.getCameraPosition();
            position.Z = 0;
            ParticleManager.Instance.addParticles("winFX", position + new Vector3(-600, 0, 20), Vector3.Zero, Color.White);
            ParticleManager.Instance.addParticles("winFX", position + new Vector3(600, 0, -20), Vector3.Zero, Color.White);

            loaded = true;
        }

        public override void update()
        {
            base.update();

            ParticleManager.Instance.update();

            time -= SB.dt;

            if (time > TIME - 1)
            {
                float perc = TIME - time;
                if (perc < 0.8f)
                {
                    levelFactor = (float)Math.Pow(perc / 0.8f, 3.0f);
                }
                else if (perc < 0.85f)
                {
                    levelFactor = 1.05f + 0.05f * ((perc - 0.8f) / 0.05f);
                }
                else if (perc < 0.9f)
                {
                    levelFactor = 1.05f - 0.05f * ((perc - 0.85f) / 0.05f);
                }
                else if (perc < 0.95f)
                {
                    levelFactor = 1.05f + 0.05f * ((perc - 0.9f) / 0.05f);
                }
                else if (perc < 1.0f)
                {
                    levelFactor = 1.05f - 0.05f * ((perc - 0.95f) / 0.05f);
                }
            }
            else
            {
                levelFactor = 1;
            }

            if (time < 0)
            {
                if (!fade)
                {
                    fade = true;
                    TransitionManager.Instance.fadeIn();
                }
                else if (fade && !TransitionManager.Instance.isFading())
                {
                    StateManager.clearStates();
                    StateManager.addState(StateManager.tGameState.WorldMap);
                }
            }
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatchBegin();
            level.render2D(new Vector2(0, 0), new Vector2(level.Width, level.Height) * levelFactor, Color.White);
            GraphicsManager.Instance.spriteBatchEnd();
        }

        public override void dispose()
        {

        }
    }
}
