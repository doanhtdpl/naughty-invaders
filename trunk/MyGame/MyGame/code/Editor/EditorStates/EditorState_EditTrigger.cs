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
        Trigger selectedTrigger;

        public EditorState_EditTrigger(Trigger selectedTrigger = null)
            : base()
        {
            this.selectedTrigger = selectedTrigger;
        }

        public override void enter()
        {
            base.enter();

            TriggerManager.Instance.addTrigger(new Trigger());

            MyEditor.Instance.changeState(new EditorState_EditTrigger());
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

            TriggerManager.Instance.render();
        }
    }
}
#endif