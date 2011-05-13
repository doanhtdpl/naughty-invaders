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
        public enum tState { Waiting, Updating, Ended };
        public tState state { get; set; }

        // the time the event has to wait in the whole cinematic to be started
        public float activationTime { get; set; }
        // if this event is skippable pressing a button
        bool skippable;

        public float duration { get; set; }
        public float timer { get; set; }

        public CinematicEvent(float activationTime, float duration)
        {
            this.activationTime = activationTime;
            this.duration = duration;
        }

        // updates the event and returns false when event ends
        public virtual bool update()
        {
            return false;
        }
        public virtual void render()
        {
        }
    }
    class ActorEvent : CinematicEvent
    {
        public RenderableEntity2D actor { get; set; }

        bool firstUpdate;

        bool set;
        Vector3 setAtPosition;

        bool move;
        Vector3 moveToPosition;
        float moveToSpeed;

        public ActorEvent(float activationTime, float duration, RenderableEntity2D actor):base(activationTime, duration)
        {
            this.actor = actor;
            this.firstUpdate = true;
            this.set = false;
            this.move = false;
        }

        // one instant events
        public void setRender(bool value)
        {
            if (value)
            {
                actor.renderState = RenderableEntity2D.tRenderState.Render;
            }
            else
            {
                actor.renderState = RenderableEntity2D.tRenderState.NoRender;
            }
        }
        public void setAt(Vector3 position)
        {
            set = true;
            setAtPosition = position;
        }
        public void playAction(string action)
        {
            ((AnimatedEntity2D)actor).playAction(action);
        }
        // time based events
        public void moveTo(Vector3 position, float speed)
        {
            move = true;
            moveToPosition = position;
            moveToSpeed = speed;
        }

        public override bool update()
        {
            bool keepUpdating = true;

            if (firstUpdate)
            {
                firstUpdate = false;
                if (set)
                {
                    actor.position = setAtPosition;
                }
            }

            if (move)
            {
                Vector3 newPosition = actor.position;
                if (GameplayHelper.Instance.fromToAtSpeed(ref newPosition, moveToPosition, moveToSpeed))
                {
                    keepUpdating = false;
                }
                actor.position = newPosition;
            }
            return keepUpdating;
        }
    }

    public enum tDialogCharacter { Wish, OnionElder, KingTomato }
    class DialogEvent : CinematicEvent
    {
        bool textComplete;
        public tDialogCharacter character { get; set; }
        public string text { get; set; }
        // text speed at characters per second
        public float textSpeed { get; set; }


        public DialogEvent(float activationTime, float durationAfterText, tDialogCharacter character, string text, float textSpeed)
            :base(activationTime, (text.Length / textSpeed) + durationAfterText)
        {
            this.character = character;
            this.text = text;
            this.textSpeed = textSpeed;

            this.textComplete = false;
        }

        public override bool update()
        {
            bool keepUpdating = true;

            timer += SB.dt;

            Vector2 position = new Vector2(0.0f, 0.0f);
            if (textComplete)
            {
                text.renderNI(position, 0.1f);
            }
            else
            {
                float timeBuildingText = text.Length / textSpeed;
                int charactersToShow = (int)(duration * textSpeed);
                if (charactersToShow > text.Length)
                {
                    charactersToShow = text.Length;
                    textComplete = true;
                }
                text.Substring(0, charactersToShow).renderNI(position, 1.0f);
            }

            if (timer > duration)
            {
                keepUpdating = false;
            }

            return keepUpdating;
        }

        public override void render()
        {

        }
    }
}
