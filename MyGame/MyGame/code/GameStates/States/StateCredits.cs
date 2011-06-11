using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class StateCredits : StateGame
    {
        float time = 3;

        public StateCredits()
            : base("credits")
        {
        }

        public override void update()
        {
            base.update();

            if (CameraManager.Instance.isIdle())
            {
                time -= SB.dt;
            }

            if (GamerManager.getMainControls().B_firstPressed() || time < 0)
            {
                TransitionManager.Instance.changeStateWithFade(StateManager.tGameState.Menu, 1, null, 0.5f, Color.Black);
            }
        }

        public override void render()
        {
            GraphicsManager.Instance.graphicsDevice.Clear(SB.BGColor);

            EntityManager.Instance.render();
            LevelManager.Instance.render();
            CinematicManager.Instance.render();
        }

    }
}