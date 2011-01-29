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
        public enum DefaultStates { NONE, MOVE, ROTATE, SCALE, ADD_STATIC, ADD_ANIMATED };

        DefaultStates state = DefaultStates.NONE;

        MouseState lastMouseState;
        KeyboardState lastKeyState;

        public override void update()
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            //Check state change
            if (keyState.IsKeyDown(Keys.Q))
            {
                changeState(DefaultStates.MOVE);
            }
            else if (keyState.IsKeyDown(Keys.W))
            {
                changeState(DefaultStates.ROTATE);
            }
            else if (keyState.IsKeyDown(Keys.E))
            {
                changeState(DefaultStates.SCALE);
            }

            if (keyState.IsKeyDown(Keys.Space) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Camera2D.position.X -= (mouseState.X - lastMouseState.X);
                Camera2D.position.Y += (mouseState.Y - lastMouseState.Y);
            }
            else if (keyState.IsKeyDown(Keys.Space) && mouseState.RightButton == ButtonState.Pressed)
            {
                Camera2D.position.Z -= (mouseState.Y - lastMouseState.Y);
            }
            else if (keyState.IsKeyDown(Keys.Delete) && lastKeyState.IsKeyUp(Keys.Delete))
            {
                //DELETE
                LevelManager.Instance.removeStaticProp(selectedEntity);
                selectedEntity = null;
            }
            else if(keyState.GetPressedKeys().Length == 0)
            {
                if (selectedEntity != null)
                {
                    if (state == DefaultStates.MOVE)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            selectedEntity.position += new Vector3(mouseState.X - lastMouseState.X, -(mouseState.Y - lastMouseState.Y), 0);
                        }
                        else if (mouseState.RightButton == ButtonState.Pressed)
                        {
                            selectedEntity.position += new Vector3(0, 0, -(mouseState.Y - lastMouseState.Y));
                        }
                    }
                    else if (state == DefaultStates.SCALE)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            selectedEntity.scale2D += new Vector2(mouseState.X - lastMouseState.X, -(mouseState.Y - lastMouseState.Y));
                        }
                        else if (mouseState.RightButton == ButtonState.Pressed)
                        {
                            selectedEntity.scale2D += new Vector2((mouseState.Y - lastMouseState.Y), (mouseState.Y - lastMouseState.Y));
                        }
                    }
                    else if (state == DefaultStates.ROTATE)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            selectedEntity.orientation += ((mouseState.Y - lastMouseState.Y) * 0.1f);
                        }
                    }

                }

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
            lastKeyState = keyState;
        }

        public void changeState(DefaultStates newState)
        {
            state = newState;
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
