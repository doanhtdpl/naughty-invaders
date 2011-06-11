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
        bool fade;
        float time = 3;

        public StateCredits()
            : base("credits")
        {
            fade = false;
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
                if(!fade)
                {
                    fade = true;
                    TransitionManager.Instance.fadeIn();
                }
            }

            if (fade && !TransitionManager.Instance.isFading())
            {
                StateManager.dequeueState(1);
                TransitionManager.Instance.fadeOut();
            }
        }
    }
}