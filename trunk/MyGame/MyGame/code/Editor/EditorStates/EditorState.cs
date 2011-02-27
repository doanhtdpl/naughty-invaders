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

        public bool canSelect = true;

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

        public bool isPressedKey(Keys key)
        {
            return MyEditor.Instance.isKeyPressed(key);
        }

        public bool selectEntity()
        {
            if (isPosInScreen(gameScreenPos))
            {

                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    canSelect = true;
                }

                if(mouseState.LeftButton == ButtonState.Pressed && (lastMouseState.X != mouseState.X || lastMouseState.Y != mouseState.Y))
                {
                    canSelect = false;
                }

                if (mouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed && canSelect)
                {
                    Ray ray = EditorHelper.Instance.getMouseCursorRay(gameScreenPos);
                    Entity2D ent = null;

                    if (MyEditor.Instance.canSelectEnemy.Checked)
                        ent = EditorHelper.Instance.rayVsEntities(ray, EnemyManager.Instance.getEnemies());

                    if (ent == null && MyEditor.Instance.canSelectAnimated.Checked)
                    {
                        ent = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getAnimatedProps());
                    }

                    if (ent == null && MyEditor.Instance.canSelectStatic.Checked)
                    {
                        ent = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getStaticProps());
                    }

                    if (ent != null)
                    {
                        if (isPressedKey(Keys.LeftControl))
                            MyEditor.Instance.addEntity(ent);
                        else
                            MyEditor.Instance.selectEntity(ent);

                        MyEditor.Instance.updateEntityProperties();
                    }
                    else
                    {
                        if (!isPressedKey(Keys.LeftControl))
                            MyEditor.Instance.getSelectedEntities().Clear();
                    }
                }
            }

            return MyEditor.Instance.anyEntitySelected();
        }

        public bool justPressedLeftButton()
        {
            return MyEditor.Instance.getMouseState().LeftButton == ButtonState.Pressed && MyEditor.Instance.getLastMouseState().LeftButton == ButtonState.Released;
        }

    }
}
