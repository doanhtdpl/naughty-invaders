using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class EditorState
    {
        public MouseState mouseState;
        public MouseState lastMouseState;
        public KeyboardState keyState;
        public KeyboardState lastKeyState;

        public Vector2 gameScreenPos;

        public virtual void enter() { }
        public virtual void exit() { }
        public virtual void update() 
        {
            mouseState = MyEditor.Instance.getMouseState();
            lastMouseState = MyEditor.Instance.getLastMouseState();
            keyState = MyEditor.Instance.getKeyState();
            lastKeyState = MyEditor.Instance.getLastKeyState();

            System.Drawing.Point point = MyEditor.Instance.myEditorControl.PointToClient(new System.Drawing.Point(MyEditor.Instance.getMouseState().X, MyEditor.Instance.getMouseState().Y));
            gameScreenPos = new Vector2(point.X, point.Y);
        }
        public virtual void render() { }

        public bool isPosInScreen(Vector2 pos)
        {
            return pos.X >= 0 && pos.Y >= 0 && pos.X <= 1280 && pos.Y <= 720;
        }

        public bool justPressedKey(Keys key)
        {
            return MyEditor.Instance.justPressedKey(key);
        }

        public bool selectEntity()
        {
            if (keyState.GetPressedKeys().Length == 0)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    if (isPosInScreen(gameScreenPos))
                    {
                        Ray ray = EditorHelper.Instance.getMouseCursorRay(gameScreenPos);
                        MyEditor.Instance.selectedEntity = EditorHelper.Instance.rayVsEntities(ray, EnemyManager.Instance.getEnemies());

                        if (MyEditor.Instance.selectedEntity == null)
                        {
                            MyEditor.Instance.selectedEntity = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getAnimatedProps());
                        }

                        if (MyEditor.Instance.selectedEntity == null)
                        {
                            MyEditor.Instance.selectedEntity = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getStaticProps());
                        }

                        if (MyEditor.Instance.selectedEntity != null)
                        {
                            MyEditor.Instance.updateEntityProperties();
                        }
                    }
                    //else
                    //{
                    //    selectedEntity = null;
                    //}
                }
            }

            return MyEditor.Instance.selectedEntity != null;
        }

        public bool justPressedLeftButton()
        {
            return MyEditor.Instance.getMouseState().LeftButton == ButtonState.Pressed && MyEditor.Instance.getLastMouseState().LeftButton == ButtonState.Released;
        }

    }
}
