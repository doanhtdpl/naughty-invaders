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
    class EditorState_MoveState : EditorState
    {
        public override void update()
        {
            base.update();

            selectEntity();

            if (keyState.GetPressedKeys().Length == 0)
            {
                if (MyEditor.Instance.selectedEntity != null && isPosInScreen(gameScreenPos))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        MyEditor.Instance.selectedEntity.position += new Vector3(mouseState.X - lastMouseState.X, - (mouseState.Y - lastMouseState.Y), 0);
                    }
                    else if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        MyEditor.Instance.selectedEntity.position += new Vector3(0, 0, - (mouseState.Y - lastMouseState.Y));
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed || mouseState.RightButton == ButtonState.Pressed)
                    {
                        MyEditor.Instance.updateEntityProperties();
                    }
                }
            }
        }

    }
}
