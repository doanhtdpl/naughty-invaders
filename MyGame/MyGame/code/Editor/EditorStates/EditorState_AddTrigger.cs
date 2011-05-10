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

            Trigger trigger = new Trigger();
            TriggerManager.Instance.addTrigger(trigger);

            MyEditor.Instance.changeState(new EditorState_EditTrigger(trigger));
        }

        public override void update()
        {
            base.update();
        }

        public override void exit()
        {
            base.exit();
        }

        public override void render()
        {
            base.render();
        }
    }
}
#endif