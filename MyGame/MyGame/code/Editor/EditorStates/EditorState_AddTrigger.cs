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
    class EditorState_AddTrigger : EditorState
    {
        public EditorState_AddTrigger()
            : base()
        {
        }

        public override void enter()
        {
            base.enter();
        }

        public override void update()
        {
            base.update();
            if (justPressedLeftButton() && isPosInScreen(gameScreenPos))
            {
                Trigger trigger = new Trigger();
                trigger.position = new Vector2(getMousePosInZ().X, getMousePosInZ().Y);
                TriggerManager.Instance.addTrigger(trigger);

                MyEditor.Instance.changeState(new EditorState_EditTrigger(trigger));
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
                DebugManager.Instance.addRectangle(trigger.position - new Vector2(20, 20), trigger.position + new Vector2(20, 20), Color.Yellow);
            }
        }
    }
}
#endif