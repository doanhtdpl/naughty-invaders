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
    class EditorState_MoveState : EditorState
    {
        public override void update()
        {
            base.update();

            selectEntity();

            //if(keyState.GetPressedKeys().Length == 0)
            {
                if (MyEditor.Instance.anyEntitySelected()  && isPosInScreen(gameScreenPos))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        foreach (Entity2D ent in MyEditor.Instance.getSelectedEntities())
                        {
                            Vector2 current = new Vector2(mouseState.X, mouseState.Y);  
                            Vector2 last = new Vector2(lastMouseState.X, lastMouseState.Y);
                            Vector3 currentZ = EditorHelper.Instance.getMousePosInZ(current, ent.position.Z);
                            Vector3 lastZ = EditorHelper.Instance.getMousePosInZ(last, ent.position.Z);

                            ent.position += (currentZ - lastZ);
                        }
                    }
                    else if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        foreach (Entity2D ent in MyEditor.Instance.getSelectedEntities())
                        {
                            ent.position += new Vector3(0, 0, -(mouseState.Y - lastMouseState.Y));
                        }
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
#endif