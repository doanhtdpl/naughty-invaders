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
        public enum DefaultStates { NONE, MOVE, ROTATE, SCALE, ADD_STATIC, ADD_ANIMATED, ADD_ENEMY, LOAD, SAVE };

        DefaultStates state = DefaultStates.NONE;

        MouseState lastMouseState, mouseState;
        KeyboardState lastKeyState, keyState;

        int currentIndex = -1;

        public bool isPosInScreen( Vector2 pos )
        {
            return pos.X >= 0 && pos.Y >= 0 && pos.X <= 1280 && pos.Y <= 720;
        }

        public bool justPressedKey(Keys key)
        {
            return keyState.IsKeyDown(key) && !lastKeyState.IsKeyDown(key);
        }

        public override void update()
        {
            mouseState = Mouse.GetState();
            keyState = Keyboard.GetState();

            System.Drawing.Point point = MyEditor.Instance.myEditorControl.PointToClient(new System.Drawing.Point(mouseState.X, mouseState.Y));
            Vector2 gameScreenPos = new Vector2(point.X, point.Y);

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
            else if (justPressedKey(Keys.Delete))
            {
                //DELETE
                LevelManager.Instance.removeStaticProp(selectedEntity);
                selectedEntity = null;
            }
            else if (state == DefaultStates.ADD_STATIC)
            {
                if (justPressedKey(Keys.Right))
                {
                    LevelManager.Instance.removeStaticProp(selectedEntity);
                    loadEntity(currentIndex + 1);
                }
                else if (justPressedKey(Keys.Left))
                {
                    LevelManager.Instance.removeStaticProp(selectedEntity);
                    loadEntity(currentIndex - 1);
                }
            }
            else if(keyState.GetPressedKeys().Length == 0)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    if (isPosInScreen(gameScreenPos))
                    {
                        Ray ray = EditorHelper.Instance.getMouseCursorRay(gameScreenPos);
                        selectedEntity = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getStaticProps());

                        if (selectedEntity == null)
                        {
                            selectedEntity = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getAnimatedProps());
                        }

                        if (selectedEntity != null)
                        {
                            updateEntityProperties();
                        }
                    }
                    //else
                    //{
                    //    selectedEntity = null;
                    //}
                }

                if (selectedEntity != null && isPosInScreen(gameScreenPos))
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
                        else if (mouseState.RightButton == ButtonState.Pressed)
                        {
                            selectedEntity.rotateInX((mouseState.Y - lastMouseState.Y) * 0.01f);
                            selectedEntity.rotateInY((mouseState.X - lastMouseState.X) * 0.01f);
                        }
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed || mouseState.RightButton == ButtonState.Pressed)
                    {
                        updateEntityProperties();
                    }
                }
            }
            lastMouseState = mouseState;
            lastKeyState = keyState;
        }

        public void loadEntity(int index)
        {
#if EDITOR
            var textures = SB.content.LoadContent("textures");
            currentIndex = (index + textures.Count) % textures.Count;
            Texture2D texture = TextureManager.Instance.getTexture(textures[currentIndex]);
            Entity2D ent = new RenderableEntity2D(textures[currentIndex], new Vector3(), new Vector2(texture.Width, texture.Height), 0);
            LevelManager.Instance.addStaticProp(ent);
            selectedEntity = ent;
#endif
        }

        public void loadAnimatedEntity(int index)
        {
#if EDITOR
            var textures = SB.content.LoadContent("xml/characters");
            currentIndex = (index + textures.Count) % textures.Count;
            AnimatedEntity2D ent = new AnimatedEntity2D(textures[currentIndex], new Vector3(), new Vector2(100, 100), 0);
            ent.scale2D = ent.getFrameSize();
            LevelManager.Instance.addAnimatedProp(ent);
            selectedEntity = ent;
#endif
        }

        public void loadEnemy(int index)
        {
#if EDITOR
            var textures = SB.content.LoadContent("xml/characters");
            currentIndex = (index + textures.Count) % textures.Count;
            AnimatedEntity2D ent = new AnimatedEntity2D(textures[currentIndex], new Vector3(), new Vector2(100, 100), 0);
            ent.scale2D = ent.getFrameSize();
            LevelManager.Instance.addAnimatedProp(ent);
            selectedEntity = ent;
#endif
        }

        public void changeState(DefaultStates newState)
        {
            state = newState;

            if (newState == DefaultStates.ADD_STATIC)
            {
                loadEntity(0);
            }
            else if (newState == DefaultStates.ADD_ANIMATED)
            {
                loadAnimatedEntity(0);
            }
            else if (newState == DefaultStates.ADD_ENEMY)
            {
                loadEnemy(0);
            }
        }

        public override void render()
        {
            if (selectedEntity != null)
            {
                EditorHelper.Instance.renderEntityQuad(selectedEntity);
            }
        }

        public void resetRotation()
        {
            if (selectedEntity != null)
            {
                selectedEntity.resetRotation();
            }
        }

        public void resetPosition()
        {
            if (selectedEntity != null)
            {
                selectedEntity.position = new Vector3(0,0,0);
            }
        }

        public void resetScale()
        {
            if (selectedEntity != null)
            {
                Texture2D texture = ((RenderableEntity2D)selectedEntity).Texture;
                selectedEntity.scale2D = selectedEntity.scale2D = new Vector2(texture.Width, texture.Height);
            }
        }
    }
}
