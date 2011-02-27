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

        private Vector2 initPoint, endPoint;
        private Vector2 initPointSelect, endPointSelect;
        private bool multiSelecting = false;

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
        public virtual void render() 
        {
            if (multiSelecting)
            {
                DebugManager.Instance.addRectangle(initPoint, endPoint, Color.Green, 1.0f);
            }
        }

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

        public Vector2 getMousePosInZeroZ()
        {
            Vector3 near = SB.graphicsDevice.Viewport.Unproject(new Vector3(gameScreenPos.X, gameScreenPos.Y, 0.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
            Vector3 far = SB.graphicsDevice.Viewport.Unproject(new Vector3(gameScreenPos.X, gameScreenPos.Y, 1.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
            Vector3 normal = new Vector3(0, 0, -1);

            float u = Vector3.Dot(normal, Vector3.Zero - near) / Vector3.Dot(normal, far - near);
            Vector3 pos = near + u * (far - near);
            return new Vector2(pos.X, pos.Y);
        }

        private bool isPointInRect(Vector2 point, Vector2 initRect, Vector2 endRect)
        {
            Rectangle rect = new Rectangle((int)Math.Min(initRect.X, endRect.X), (int)Math.Min(initRect.Y, endRect.Y), (int)Math.Abs(initRect.X - endRect.X), (int)Math.Abs(initRect.Y - endRect.Y));
            return rect.Contains(new Point((int)point.X, (int)point.Y));
        }

        public bool selectEntity()
        {
            if (isPosInScreen(gameScreenPos))
            {
                if (mouseState.MiddleButton == ButtonState.Pressed && lastMouseState.MiddleButton == ButtonState.Released)
                {
                    MyEditor.Instance.getSelectedEntities().Clear();

                    initPointSelect = gameScreenPos;
                    initPoint = getMousePosInZeroZ();
                    multiSelecting = true;
                }
                else if (mouseState.MiddleButton == ButtonState.Pressed)
                {
                    endPointSelect = gameScreenPos;
                    endPoint = getMousePosInZeroZ();
                }
                else if (mouseState.MiddleButton == ButtonState.Released && lastMouseState.MiddleButton == ButtonState.Pressed)
                {
                    endPointSelect = gameScreenPos;
                    endPoint = getMousePosInZeroZ();
                    multiSelecting = false;

                    //Select entities inside the rect
                    if (MyEditor.Instance.canSelectEnemy.Checked)
                    {
                        foreach (Entity2D e in EnemyManager.Instance.getEnemies())
                        {
                            Vector3 pos = SB.graphicsDevice.Viewport.Project(e.position, Camera2D.projection, Camera2D.view, Matrix.Identity);
                            if(MyEditor.Instance.getSelectedEntities().IndexOf(e) < 0 && isPointInRect(new Vector2(pos.X, pos.Y), initPointSelect, endPointSelect))
                            {
                                MyEditor.Instance.getSelectedEntities().Add(e);
                            }
                        }
                    }
                    if (MyEditor.Instance.canSelectAnimated.Checked)
                    {
                        foreach (Entity2D e in LevelManager.Instance.getAnimatedProps())
                        {
                            Vector3 pos = SB.graphicsDevice.Viewport.Project(e.position, Camera2D.projection, Camera2D.view, Matrix.Identity);
                            if(MyEditor.Instance.getSelectedEntities().IndexOf(e) < 0 && isPointInRect(new Vector2(pos.X, pos.Y), initPointSelect, endPointSelect))
                            {
                                MyEditor.Instance.getSelectedEntities().Add(e);
                            }
                        }
                    }
                    if (MyEditor.Instance.canSelectStatic.Checked)
                    {
                        foreach (Entity2D e in LevelManager.Instance.getStaticProps())
                        {
                            Vector3 pos = SB.graphicsDevice.Viewport.Project(e.position, Camera2D.projection, Camera2D.view, Matrix.Identity);
                            if(MyEditor.Instance.getSelectedEntities().IndexOf(e) < 0 && isPointInRect(new Vector2(pos.X, pos.Y), initPointSelect, endPointSelect))
                            {
                                MyEditor.Instance.getSelectedEntities().Add(e);
                            }
                        }
                    }
                }
                else
                {

                    if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                    {
                        canSelect = true;
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed && (lastMouseState.X != mouseState.X || lastMouseState.Y != mouseState.Y))
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
                            if (MyEditor.Instance.selectGroup.Checked)
                            {
                                foreach (List<int> group in LevelManager.Instance.getGroups())
                                {
                                    foreach (int id in group)
                                    {
                                        if (id == ent.id)
                                        { 
                                            //Select the group
                                            MyEditor.Instance.getSelectedEntities().Clear();

                                            foreach(int entityId in group)
                                            {
                                                MyEditor.Instance.addEntity(EntityManager.Instance.getEntityByID(entityId));
                                            }
                                            return MyEditor.Instance.anyEntitySelected();
                                        }
                                    }
                                }
                            }
                            
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
            }

            return MyEditor.Instance.anyEntitySelected();
        }

        public bool justPressedLeftButton()
        {
            return MyEditor.Instance.getMouseState().LeftButton == ButtonState.Pressed && MyEditor.Instance.getLastMouseState().LeftButton == ButtonState.Released;
        }

    }
}
