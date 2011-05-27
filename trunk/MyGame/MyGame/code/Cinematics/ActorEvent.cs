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

        bool updateChanged;
        RenderableEntity2D.tUpdateState updateValue;

        bool renderChanged;
        RenderableEntity2D.tRenderState renderValue;

        bool positionChanged;
        bool orientWhileMoving;
        Vector3 positionValue;
        Function function;

        bool orientationChanged;
        float orientationValue;

        bool move;

        Vector3 moveToPosition;
        float moveToSpeed;

        public ActorEvent(RenderableEntity2D actor, bool skippable = true, float activationTime = 0.3f, bool hasDuration = false, float duration = 999.0f)
            : base(activationTime, hasDuration, duration)
        {
            this.actor = actor;
            this.positionChanged = false;
            this.orientWhileMoving = false;
            this.move = false;
            this.function = null;
            this.skippable = skippable;
            this.updateChanged = false;
            this.renderChanged = false;
            this.orientationChanged = false;
        }

        // one instant events
        public void setUpdate(bool value)
        {
            updateChanged = true;
            updateValue = value ? RenderableEntity2D.tUpdateState.Update : RenderableEntity2D.tUpdateState.NoUpdate;
        }
        public void setRender(bool value)
        {
            renderChanged = true;
            renderValue = value ? RenderableEntity2D.tRenderState.Render : RenderableEntity2D.tRenderState.NoRender;
        }
        public void setAt(Vector3 position)
        {
            positionChanged = true;
            positionValue = position;
        }
        public void setOrientation(float value)
        {
            orientationChanged = true;
            orientationValue = value;
        }
        public void playAction(string action)
        {
            ((AnimatedEntity2D)actor).playAction(action);
        }
        // time based events
        public void moveTo(Vector3 position, float speed, bool orientWhileMoving = false)
        {
            this.move = true;
            this.moveToPosition = position;
            this.moveToSpeed = speed;
            this.orientWhileMoving = orientWhileMoving;
        }
        public void addActorFunction(string name)
        {
            function = new Function(name, "ConsequenceFunctions", new object[] { actor });
        }
        public override void startEvent()
        {
            if (updateChanged)
            {
                actor.updateState = updateValue;
            }
            if (renderChanged)
            {
                actor.renderState = renderValue;
            }
            if (positionChanged)
            {
                actor.position = positionValue;
            }
            if (orientationChanged)
            {
                actor.orientation = orientationValue;
            }
            if (function != null)
            {
                function.execute();
            }
        }

        public override void endEvent()
        {
            if (move)
            {
                actor.position = moveToPosition;
            }
        }

        public override bool update(bool skip, bool forceSkip = false)
        {
            bool keepUpdating = false;

            if (skippable || forceSkip)
            {
                if (skip)
                {
                    endEvent();
                    return false;
                }
            }

            if (move)
            {
                keepUpdating = true;
                Vector3 newPosition = actor.position;
                if (GameplayHelper.Instance.fromToAtSpeed(ref newPosition, moveToPosition, moveToSpeed))
                {
                    keepUpdating = false;
                }
                actor.position = newPosition;
                if (orientWhileMoving)
                {
                    actor.orientation = Calc.directionToAngle((moveToPosition - actor.position).toVector2()) - Calc.PiOver2;
                    orientWhileMoving = false;
                }
            }

            return keepUpdating || base.update(skip);
        }
    }
}
