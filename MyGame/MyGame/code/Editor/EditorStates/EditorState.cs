﻿#if EDITOR
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

        public long timeLastMouseClick = 0;

        public Vector2 lastScreenPos;
        public List<Entity2D> ignoreEntities = new List<Entity2D>();

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

            gameScreenPos = MyEditor.Instance.gameScreenPos;

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
                Vector3 aux = GraphicsManager.Instance.graphicsDevice.Viewport.Project(new Vector3(mouseInSetaZero.X, mouseInSetaZero.Y, 0.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
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

        public Vector3 getMousePosInZ(float z = 0.0f)
        {
            return EditorHelper.Instance.getMousePosInZ(gameScreenPos, z);
        }

        private bool isPointInRect(Vector2 point, Vector2 initRect, Vector2 endRect)
        {
            Rectangle rect = new Rectangle((int)Math.Min(initRect.X, endRect.X), (int)Math.Min(initRect.Y, endRect.Y), (int)Math.Abs(initRect.X - endRect.X), (int)Math.Abs(initRect.Y - endRect.Y));
            return rect.Contains(new Point((int)point.X, (int)point.Y));
        }

        public Entity2D selectPoint(Vector2 pos)
        {
            Ray ray = EditorHelper.Instance.getMouseCursorRay(pos);
            Entity2D ent = null;

            if (MyEditor.Instance.canSelectEnemy.Checked)
                ent = EditorHelper.Instance.rayVsEntities(ray, EnemyManager.Instance.getEnemies(), null, ignoreEntities);

            if (MyEditor.Instance.canSelectAnimated.Checked)
                ent = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getAnimatedProps(), ent, ignoreEntities);

            if (MyEditor.Instance.canSelectStatic.Checked)
                ent = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getStaticProps(), ent, ignoreEntities);

            return ent;
        }

        public bool selectEntity()
        {
            if (!isPosInScreen(gameScreenPos))
            {
                return MyEditor.Instance.anyEntitySelected();
            }
    
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
                        Vector3 pos = GraphicsManager.Instance.graphicsDevice.Viewport.Project(e.position, Camera2D.projection, Camera2D.view, Matrix.Identity);
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
                        Vector3 pos = GraphicsManager.Instance.graphicsDevice.Viewport.Project(e.position, Camera2D.projection, Camera2D.view, Matrix.Identity);
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
                        Vector3 pos = GraphicsManager.Instance.graphicsDevice.Viewport.Project(e.position, Camera2D.projection, Camera2D.view, Matrix.Identity);
                        if (MyEditor.Instance.getSelectedEntities().IndexOf(e) < 0 && isPointInRect(new Vector2(pos.X, pos.Y), initPointSelect, endPointSelect))
                        {
                            MyEditor.Instance.getSelectedEntities().Add(e);
                        }
                    }
                }
            }

            else if (justPressedKey(Keys.M))
            {
                Entity2D ent = selectPoint(lastScreenPos);
                ignoreEntities.Add(ent);

                if (MyEditor.Instance.selectGroup.Checked && ent != null)
                {
                    foreach (List<int> group in LevelManager.Instance.getGroups())
                    {
                        foreach (int id in group)
                        {
                            if (id == ent.id)
                            {
                                //Select the group
                                MyEditor.Instance.getSelectedEntities().Clear();

                                foreach (int entityId in group)
                                {
                                    MyEditor.Instance.addEntity(EntityManager.Instance.getEntityByID(entityId));
                                }

                                return MyEditor.Instance.anyEntitySelected();
                            }
                        }
                    }

                }

                MyEditor.Instance.selectEntity(ent);
                MyEditor.Instance.updateEntityProperties();
            }

            //POINT CLICK SELECT
            else
            {

                if (justPressedLeftButton())
                {
                    canSelect = true;
                }

                if (mouseState.LeftButton == ButtonState.Pressed && (Math.Abs(lastMouseState.X - mouseState.X) > 2 || Math.Abs(lastMouseState.Y - mouseState.Y) > 2))
                {
                    canSelect = false;
                }

                if (justReleasedLeftButton() && canSelect)
                {
                    ignoreEntities.Clear();
                    Entity2D ent = selectPoint(gameScreenPos);
                    ignoreEntities.Add(ent);
                    lastScreenPos = gameScreenPos;

                    if (ent != null)
                    {
                        //GROUP SELECT
                        if (MyEditor.Instance.selectGroup.Checked || MyEditor.Instance.myEditorControl.totalTime.ElapsedMilliseconds - timeLastMouseClick <= 400 )
                        {
                            foreach (List<int> group in LevelManager.Instance.getGroups())
                            {
                                foreach (int id in group)
                                {
                                    if (id == ent.id)
                                    { 
                                        //Select the group
                                        if (!isPressedKey(Keys.LeftControl) && !isPressedKey(Keys.RightControl) && !isPressedKey(Keys.LeftShift) && !isPressedKey(Keys.RightShift))
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
                        if (isPressedKey(Keys.LeftControl) || isPressedKey(Keys.RightControl) || isPressedKey(Keys.LeftShift) || isPressedKey(Keys.RightShift))
                        {
                            MyEditor.Instance.addEntity(ent);
                        }
                        else
                            MyEditor.Instance.selectEntity(ent);

                        MyEditor.Instance.updateEntityProperties();
                    }
                    else
                    {
                        if (!isPressedKey(Keys.LeftControl) && !isPressedKey(Keys.RightControl) && !isPressedKey(Keys.LeftShift) && !isPressedKey(Keys.RightShift))
                            MyEditor.Instance.getSelectedEntities().Clear();
                    }

                    timeLastMouseClick = MyEditor.Instance.myEditorControl.totalTime.ElapsedMilliseconds;
                }
            }

            return MyEditor.Instance.anyEntitySelected();
        }

        public bool justPressedRightButton()
        {
            return MyEditor.Instance.getMouseState().RightButton == ButtonState.Pressed && MyEditor.Instance.getLastMouseState().RightButton == ButtonState.Released;
        }

        public bool justPressedLeftButton()
        {
            return MyEditor.Instance.getMouseState().LeftButton == ButtonState.Pressed && MyEditor.Instance.getLastMouseState().LeftButton == ButtonState.Released;
        }

        public bool justReleasedLeftButton()
        {
            return MyEditor.Instance.getMouseState().LeftButton == ButtonState.Released && MyEditor.Instance.getLastMouseState().LeftButton == ButtonState.Pressed;
        }

        public virtual void selectEntity(int index)
        {
        }
    }
}
#endif