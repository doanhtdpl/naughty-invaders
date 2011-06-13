using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class SpecialEvent : CinematicEvent
    {
        bool playEffect;
        string effectToPlay;
        Vector3 effectPosition;
        Vector3 effectDirection;
        Color effectColor;
        float effectScale;

        bool playSong;
        string songToPlay;
        bool loopToPlay;

        bool playSoundEffect;
        string soundEffect;

        public SpecialEvent(RenderableEntity2D actor, bool skippable = true, float activationTime = 0.3f, bool hasDuration = false, float duration = 999.0f)
            : base(activationTime, hasDuration, duration)
        {
        }

        // one instant events
        public void setPlayEffect(string effectToPlay, Vector3 effectPosition, Vector3 effectDirection, Color effectColor, float effectScale)
        {
            this.playEffect = true;
            this.effectToPlay = effectToPlay;
            this.effectPosition = effectPosition;
            this.effectDirection = effectDirection;
            this.effectColor = effectColor;
            this.effectScale = effectScale;
        }

        public void setPlaySong(string songToPlay, bool loop)
        {
            this.playSong = true;
            this.songToPlay = songToPlay;
            this.loopToPlay = loop;
        }

        public void setPlaySound(string sound)
        {
            this.playSoundEffect = true;
            this.soundEffect = sound;
        }

        public override void startEvent()
        {
            if (playEffect)
            {
                ParticleManager.Instance.addParticles(effectToPlay, effectPosition, effectDirection, effectColor, effectScale);
            }
            if (playSong)
            {
                SoundManager.Instance.playWithTransition(songToPlay, loopToPlay);
            }
            if (playSoundEffect)
            {
                SoundManager.Instance.playEffect(soundEffect);
            }
        }
    }
}
