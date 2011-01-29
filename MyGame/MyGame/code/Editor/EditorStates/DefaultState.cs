using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class DefaultState : EditorState
    {
        MouseState lastMouseState;

        public override void update()
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            if (keyState.IsKeyDown(Keys.Space) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Camera2D.position.X -= (mouseState.X - lastMouseState.X);
                Camera2D.position.Y += (mouseState.Y - lastMouseState.Y);
            }
            else if (keyState.IsKeyDown(Keys.Space) && mouseState.RightButton == ButtonState.Pressed)
            {
                Camera2D.position.Z -= (mouseState.Y - lastMouseState.Y);
            }
            else if(keyState.GetPressedKeys().Length == 0)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    System.Drawing.Point point = MyEditor.Instance.myEditorControl.PointToClient(new System.Drawing.Point(mouseState.X, mouseState.Y));
                    Vector2 pos = new Vector2(point.X, point.Y);
                    Ray ray = EditorHelper.Instance.getMouseCursorRay(pos);
                    selectedEntity = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getStaticProps());

                    if (selectedEntity != null)
                    {
                        updateEntityProperties();
                    }
                }
            }
            lastMouseState = mouseState;
        }

        public override void render()
        {
            if (selectedEntity != null)
            {
                EditorHelper.Instance.renderEntityQuad(selectedEntity);
            }
        }
    }
}
