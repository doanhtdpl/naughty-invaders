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
    class EditorState_EditPlayerPos : EditorState
    {
        public override void enter()
        {
            base.enter();
        }

        public override void exit()
        {
            base.exit();
        }

        public override void update()
        {
            base.update();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 current = new Vector2(mouseState.X, mouseState.Y);
                Vector2 last = new Vector2(lastMouseState.X, lastMouseState.Y);
                Vector3 currentZ = EditorHelper.Instance.getMousePosInZ(current, GamerManager.getMainPlayer().initPos.Z);
                Vector3 lastZ = EditorHelper.Instance.getMousePosInZ(last, GamerManager.getMainPlayer().initPos.Z);

                GamerManager.getMainPlayer().initPos += (currentZ - lastZ);
            }
            else if (mouseState.RightButton == ButtonState.Pressed)
            {
                GamerManager.getMainPlayer().initPos += new Vector3(0, 0, -(mouseState.Y - lastMouseState.Y));
            }
        }
        public override void render()
        {
            DebugManager.Instance.addRectangle(GamerManager.getMainPlayer().initPos - new Vector3(50, 50, 0), GamerManager.getMainPlayer().initPos + new Vector3(50, 50, 0), Color.Yellow);
        }
    }
}
#endif