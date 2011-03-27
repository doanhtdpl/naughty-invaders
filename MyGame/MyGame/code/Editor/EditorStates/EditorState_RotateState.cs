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
    class EditorState_RotateState : EditorState
    {
        public override void update()
        {
            base.update();

            selectEntity();

            //if (keyState.GetPressedKeys().Length == 0)
            {
                if (MyEditor.Instance.anyEntitySelected() && isPosInScreen(gameScreenPos))
                {
                    Vector3 rotatingCenter = Vector3.Zero;
                    foreach (Entity2D ent in MyEditor.Instance.getSelectedEntities())
                    {
                        rotatingCenter += ent.position;
                    }

                    rotatingCenter /= MyEditor.Instance.getSelectedEntities().Count;

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        foreach(Entity2D ent in MyEditor.Instance.getSelectedEntities())
                        {
                            //ent.orientation += ((mouseState.Y - lastMouseState.Y) * 0.1f);
                            ent.rotateInZ((mouseState.Y - lastMouseState.Y) * 0.01f, ent.position - rotatingCenter);
                        }
                    }
                    else if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        foreach (Entity2D ent in MyEditor.Instance.getSelectedEntities())
                        {
                            ent.rotateInX((mouseState.Y - lastMouseState.Y) * 0.01f, ent.position - rotatingCenter);
                            ent.rotateInY((mouseState.X - lastMouseState.X) * 0.01f, ent.position - rotatingCenter);
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
