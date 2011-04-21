#if EDITOR
using System;
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
    class EditorState_AddColisions : EditorState
    {
        bool lineStarted = false;
        Vector2 initPoint;
        Vector2 currentMousePos;

        public EditorState_AddColisions()
            : base()
        {
        }

        public override void enter()
        {
            base.enter();
            MyEditor.Instance.myEditorControl.Focus();
        }

        public override void update()
        {
            base.update();

            currentMousePos = mouseInSetaZero.toVector2();

            if (isPressedKey(Keys.LeftShift) || isPressedKey(Keys.RightShift))
            { 
                float dist = float.MaxValue;
                foreach(Line line in LevelManager.Instance.getLevelCollisions())
                {
                    if((line.p1 - mouseInSetaZero.toVector2()).Length() < dist)
                    {
                        currentMousePos = line.p1;
                        dist = (line.p1 - mouseInSetaZero.toVector2()).Length();
                    }

                    if((line.p2 - mouseInSetaZero.toVector2()).Length() < dist)
                    {
                        currentMousePos = line.p2;
                        dist = (line.p2 - mouseInSetaZero.toVector2()).Length();
                    }
                }
            }

            if (isPosInScreen(gameScreenPos))
            {
                if (justPressedLeftButton())
                {
                    if (lineStarted)
                    {
                        Vector2 endPoint = currentMousePos;
                        Line line = new Line(initPoint, endPoint);
                        initPoint = endPoint;
                        LevelManager.Instance.addLevelCollision(line);
                    }
                    else
                    {
                        initPoint = currentMousePos;
                        lineStarted = true;
                    }
                }
                else if(justPressedRightButton())
                {
                    lineStarted = false;
                }
            }
        }

        public override void exit()
        {
            base.exit();
        }

        public override void render()
        {
            if (lineStarted)
            {
                DebugManager.Instance.addLine(initPoint, currentMousePos, Color.Red);
            }
            else
            {
                DebugManager.Instance.addCircle(currentMousePos, 10, 10, Color.Red);
            }
            
            foreach(Line line in LevelManager.Instance.getLevelCollisions())
            {
                DebugManager.Instance.addLine(line.p1, line.p2, Color.Blue);
            }
        }
    }
}
#endif