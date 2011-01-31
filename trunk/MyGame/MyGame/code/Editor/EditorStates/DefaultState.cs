using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class DefaultState : EditorState
    {
        public enum DefaultStates { NONE, MOVE, ROTATE, SCALE, ADD_STATIC, ADD_ANIMATED };

        DefaultStates state = DefaultStates.NONE;

        MouseState lastMouseState;
        KeyboardState lastKeyState;

        int currentIndex = -1;

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

            else if (keyState.IsKeyDown(Keys.Space) && mouseState.LeftButton == ButtonState.Pressed)
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
            else if (state == DefaultStates.ADD_STATIC)
            {
                if (keyState.IsKeyDown(Keys.Right) && !lastKeyState.IsKeyDown(Keys.Right))
                {
                    LevelManager.Instance.removeStaticProp(selectedEntity);
                    loadEntity(currentIndex + 1);
                }
                else if (keyState.IsKeyDown(Keys.Left) && !lastKeyState.IsKeyDown(Keys.Left))
                {
                    LevelManager.Instance.removeStaticProp(selectedEntity);
                    loadEntity(currentIndex - 1);
                }
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
                    updateEntityProperties();
                }

                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    System.Drawing.Point point = MyEditor.Instance.myEditorControl.PointToClient(new System.Drawing.Point(mouseState.X, mouseState.Y));
                    Vector2 pos = new Vector2(point.X, point.Y);
                    if (pos.X >= 0 && pos.Y >= 0 && pos.X <= 1280 && pos.Y <= 720)
                    {
                        Ray ray = EditorHelper.Instance.getMouseCursorRay(pos);
                        selectedEntity = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getStaticProps());

                        if (selectedEntity != null)
                        {
                            updateEntityProperties();
                        }
                    }
                }
            }
            lastMouseState = mouseState;
            lastKeyState = keyState;
        }

        public void loadEntity(int index)
        {
            var textures = SB.content.LoadContent<Texture2D>("textures");
            var array = textures.Values.ToArray<Texture2D>();
            currentIndex = (index + array.Length) % array.Length;
            Entity2D ent = new RenderableEntity2D(array[currentIndex], new Vector3(), new Vector2(100, 100), 0);
            LevelManager.Instance.addStaticProp(ent);
            selectedEntity = ent;
        }

        public void changeState(DefaultStates newState)
        {
            state = newState;

            if (newState == DefaultStates.ADD_STATIC)
            {
                loadEntity(0);
            }
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
