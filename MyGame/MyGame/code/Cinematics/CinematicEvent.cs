using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class CinematicEvent
    {
        // the time the event has to wait in the whole cinematic to be started
        public float activationTime { get; set; }
        // if this event is skippable pressing a button
        public bool skippable { get; set; }

        public bool hasDuration { get; set; }
        public float duration { get; set; }
        public float timer { get; set; }

        public CinematicEvent(float activationTime, bool hasDuration = false, float duration = 0.0f)
        {
            this.activationTime = activationTime;
            this.hasDuration = hasDuration;
            this.duration = duration;
        }

        public virtual void startEvent() { }
        public virtual void endEvent() { }
        // updates the event and returns false when event ends
        public virtual bool update(bool skip, bool forceSkip = false)
        {
            if (hasDuration)
            {
                duration -= SB.dt;
                return duration > 0.0f;
            }
            return false;
        }
        public virtual void render()
        {
        }
    }
}