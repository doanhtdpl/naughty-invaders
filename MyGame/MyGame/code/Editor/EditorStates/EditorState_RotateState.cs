﻿using System;
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
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        foreach(Entity2D ent in MyEditor.Instance.getSelectedEntities())
                        {
                            ent.orientation += ((mouseState.Y - lastMouseState.Y) * 0.1f);
                        }
                    }
                    else if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        foreach (Entity2D ent in MyEditor.Instance.getSelectedEntities())
                        {
                            ent.rotateInX((mouseState.Y - lastMouseState.Y) * 0.01f);
                            ent.rotateInY((mouseState.X - lastMouseState.X) * 0.01f);
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