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
    class EditorState_EditColisions : EditorState
    {
        Line selectedLine = null;

        public EditorState_EditColisions()
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

            if (justPressedLeftButton())
            {
                float dist = float.MaxValue;
                foreach (Line line in LevelManager.Instance.getLevelCollisions())
                {
                    if (line.distanceSquaredToPoint(mouseInSetaZero.toVector2()) < dist && line.distanceSquaredToPoint(mouseInSetaZero.toVector2()) < 30)
                    {
                        dist = line.distanceSquaredToPoint(mouseInSetaZero.toVector2());
                        selectedLine = line;
                    }
                }
            }

            if (justPressedKey(Keys.Delete) && selectedLine != null)
            {
                LevelManager.Instance.getLevelCollisions().Remove(selectedLine);
                selectedLine = null;
            }
        }

        public override void exit()
        {
            base.exit();
        }

        public override void render()
        {
            if (selectedLine != null)
            {
                DebugManager.Instance.addLine(selectedLine.p1, selectedLine.p2, Color.Red);
            }

            foreach (Line line in LevelManager.Instance.getLevelCollisions())
            {
                if (line != selectedLine)
                {
                    DebugManager.Instance.addLine(line.p1, line.p2, Color.Blue);
                }
            }
        }
    }
}
#endif