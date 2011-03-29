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
    class EditorState_ScaleState : EditorState
    {
        public override void update()
        {
            base.update();

            selectEntity();

            //if (keyState.GetPressedKeys().Length == 0)
            {
                if (MyEditor.Instance.anyEntitySelected() && isPosInScreen(gameScreenPos))
                {
                    Vector3 center = Vector3.Zero;
                    foreach (Entity2D ent in MyEditor.Instance.getSelectedEntities())
                    {
                        center += ent.position;
                    }
                    center /= MyEditor.Instance.getSelectedEntities().Count;

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        foreach(Entity2D ent in MyEditor.Instance.getSelectedEntities())
                        {
                            //Vector3 offset = ent.position - center;
                            Vector3 scale = new Vector3((mouseState.X - lastMouseState.X) / 100.0f, -(mouseState.Y - lastMouseState.Y) / 100.0f, 0.0f);
                            scale += Vector3.One;
                            //scale = Vector3.Transform(scale, Matrix.CreateRotationZ(-ent.orientation));
                            scale = Vector3.Transform(scale, Matrix.Identity);
                            ent.scale *= scale;
                            //ent.position = center + new Vector3(offset.X * scale.X, offset.Y * scale.Y, 0);
                        }
                    }
                    else if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        foreach (Entity2D ent in MyEditor.Instance.getSelectedEntities())
                        {
                            Vector3 offset = ent.position - center;
                            float scale = 1.0f + ((mouseState.Y - lastMouseState.Y) / 100.0f);
                            ent.scale2D *= scale;
                            ent.position = center + offset * scale;
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
