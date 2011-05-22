using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class ActorEvent : CinematicEvent
    {
        public RenderableEntity2D actor { get; set; }

        bool set;
        Vector3 setAtPosition;
        Function function;

        bool move;
        Vector3 moveToPosition;
        float moveToSpeed;

        public ActorEvent(RenderableEntity2D actor, float duration = 999.0f, float activationTime = 0.3f, bool skippable = true):base(activationTime, duration)
        {
            this.actor = actor;
            this.set = false;
            this.move = false;
            this.function = null;
            this.skippable = skippable;
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
        public void addActorFunction(string name)
        {
            function = new Function(name, "ConsequenceFunctions", new object[] { actor });
        }
        public override void startEvent()
        {
            if (set)
            {
                actor.position = setAtPosition;
            }
            if (function != null)
            {
                function.execute();
            }
        }

        public override void endEvent()
        {
        }

        public override bool update(bool skip, bool forceSkip = false)
        {
            bool keepUpdating = base.update(skip);

            if (skippable || forceSkip)
            {
                if (skip)
                {
                    if (move)
                    {
                        actor.position = moveToPosition;
                    }
                    return false;
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
}
