using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private Vector3 initPoint, endPoint;
        private Vector2 initPointSelect, endPointSelect;
        private bool multiSelecting = false;

        public Vector2 gameScreenPos;
        public Vector3 mouseInSetaZero;

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

            //Calculate the position in Z = 0;
            mouseInSetaZero = getMousePosInZ();

            // if using the grid mode
            if (MyEditor.Instance.drawGrid && (isPressedKey(Keys.LeftShift) || isPressedKey(Keys.RightShift)))
            {
                int gridSpacing = MyEditor.Instance.gridSpacing;
                // find the 4 nearest points of the grid
                Vector3 p1;
                p1.X = mouseInSetaZero.X - ((mouseInSetaZero.X + (gridSpacing * 10000.0f)) % gridSpacing) + (gridSpacing / 2);
                p1.Y = mouseInSetaZero.Y - ((mouseInSetaZero.Y + (gridSpacing * 10000.0f)) % gridSpacing) + (gridSpacing / 2);
                p1.Z = 0.0f;
                if (mouseInSetaZero.X > p1.X)
                {
                    if (mouseInSetaZero.Y > p1.Y)
                    {
                        mouseInSetaZero = p1 + new Vector3(gridSpacing / 2, gridSpacing / 2, 0.0f);
                    }
                    else
                    {
                        mouseInSetaZero = p1 + new Vector3(gridSpacing / 2, -gridSpacing / 2, 0.0f);
                    }
                }
                else
                {
                    if (mouseInSetaZero.Y > p1.Y)
                    {
                        mouseInSetaZero = p1 + new Vector3(-gridSpacing / 2, gridSpacing / 2, 0.0f);
                    }
                    else
                    {
                        mouseInSetaZero = p1 + new Vector3(-gridSpacing / 2, -gridSpacing / 2, 0.0f);
                    }
                }
                Vector3 aux = SB.graphicsDevice.Viewport.Project(new Vector3(mouseInSetaZero.X, mouseInSetaZero.Y, 0.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
                gameScreenPos.X = aux.X;
                gameScreenPos.Y = aux.Y;
            }
        }
        public virtual void render() 
        {
            if (multiSelecting)
            {
                DebugManager.Instance.addRectangle(new Vector2(initPoint.X, initPoint.Y), new Vector2(endPoint.X, endPoint.Y), Color.Green, 1.0f);
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

        public Vector3 getMousePosInZ(Vector2 mousePos, float z = 0.0f)
        {
            Vector3 near = SB.graphicsDevice.Viewport.Unproject(new Vector3(mousePos.X, mousePos.Y, 0.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
            Vector3 far = SB.graphicsDevice.Viewport.Unproject(new Vector3(mousePos.X, mousePos.Y, 1.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
            Vector3 normal = new Vector3(0, 0, -1);

            float u = Vector3.Dot(normal, new Vector3(0.0f, 0.0f, z) - near) / Vector3.Dot(normal, far - near);
            Vector3 pos = near + u * (far - near);
            return new Vector3(pos.X, pos.Y, z);
        }

        public Vector3 getMousePosInZ(float z = 0.0f)
        {
            return getMousePosInZ(gameScreenPos, z);
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
                // RECT MULTISELECT
                if (mouseState.MiddleButton == ButtonState.Pressed && lastMouseState.MiddleButton == ButtonState.Released)
                {
                    MyEditor.Instance.getSelectedEntities().Clear();

                    initPointSelect = gameScreenPos;
                    initPoint = mouseInSetaZero;
                    multiSelecting = true;
                }
                else if (mouseState.MiddleButton == ButtonState.Pressed)
                {
                    endPointSelect = gameScreenPos;
                    endPoint = mouseInSetaZero;
                }
                else if (mouseState.MiddleButton == ButtonState.Released && lastMouseState.MiddleButton == ButtonState.Pressed)
                {
                    endPointSelect = gameScreenPos;
                    endPoint = mouseInSetaZero;
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

                //POINT CLICK SELECT
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

                        if (MyEditor.Instance.canSelectAnimated.Checked)
                            ent = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getAnimatedProps(), ent);

                        if (MyEditor.Instance.canSelectStatic.Checked)
                            ent = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getStaticProps(), ent);


                        if (ent != null)
                        {
                            //GROUP SELECT
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
                            
                            //CONTROL MULTISELECT
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
