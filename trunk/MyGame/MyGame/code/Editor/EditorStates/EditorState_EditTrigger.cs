#if EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class EditorState_EditTrigger : EditorState
    {
        Trigger selected;
        float selectedDelta;

        public EditorState_EditTrigger(Trigger selected = null)
            : base()
        {
            this.selected = selected;
        }

        public override void enter()
        {
            base.enter();

            MyEditor.Instance.availableConditions.Items.Clear();
            foreach (string functionName in ReflectionManager.Instance.getClassFunctions("ConditionFunctions"))
            {
                MyEditor.Instance.availableConditions.Items.Add(functionName);
            }

            MyEditor.Instance.availableConsecuences.Items.Clear();
            foreach (string functionName in ReflectionManager.Instance.getClassFunctions("ConsequenceFunctions"))
            {
                MyEditor.Instance.availableConsecuences.Items.Add(functionName);
            }

            updateFunctions();
        }

        public override void update()
        {
            base.update();

            if (justPressedLeftButton() && isPosInScreen(gameScreenPos)) //&& (Math.Abs(lastMouseState.X - mouseState.X) > 2 || Math.Abs(lastMouseState.Y - mouseState.Y) > 2))
            {
                selected = null;
                foreach (Trigger trigger in TriggerManager.Instance.getTriggers())
                {
                    float delta = (getMousePosInZ().toVector2() - trigger.position).Length();
                    if (delta < 25 && (selected == null || delta < selectedDelta))
                    {
                        selected = trigger;
                        selectedDelta = delta;
                    }
                }

                if (selected != null)
                {
                    updateFunctions();
                }
            }
        }

        public void addCondition(string name)
        {
            if (selected != null)
            {
                selected.addFunction(true, name, null);
                
                updateFunctions();
            }
        }

        public void addConsecuence(string name)
        {
            if (selected != null)
            {
                ReflectionManager.Instance.getParameters("ConditionFunctions", name);
                selected.addFunction(false, name, null);
                updateFunctions();
            }
        }

        public void removeCondition(int index)
        {
            if (selected != null)
            {
                selected.removeFunction(true, index);
                updateFunctions();
            }
        }

        public void removeConsecuence(int index)
        {
            if (selected != null)
            {
                selected.removeFunction(false, index);
                updateFunctions();
            }
        }

        public void updateFunctions()
        {
            if (selected != null)
            {
                MyEditor.Instance.triggerConditions.Items.Clear();
                foreach (Function func in selected.conditions)
                {
                    MyEditor.Instance.triggerConditions.Items.Add(func.functionName);
                }

                MyEditor.Instance.triggerConsecuences.Items.Clear();
                foreach (Function func in selected.executions)
                {
                    MyEditor.Instance.triggerConsecuences.Items.Add(func.functionName);
                }
            }
        }

        public override void exit()
        {
            base.exit();
        }

        public override void render()
        {
            base.render();

            foreach (Trigger trigger in TriggerManager.Instance.getTriggers())
            {
                if (selected != null && trigger == selected)
                    DebugManager.Instance.addRectangle(trigger.position - new Vector2(20, 20), trigger.position + new Vector2(20, 20), Color.Red);
                else
                    DebugManager.Instance.addRectangle(trigger.position - new Vector2(20, 20), trigger.position + new Vector2(20, 20), Color.Yellow);
            }
        }
    }
}
#endif