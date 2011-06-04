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
    class EditorState_EditEnemySpawnZone : EditorState
    {
        EnemySpawnZone zone = null;

        public EditorState_EditEnemySpawnZone()
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

            if (justPressedLeftButton() && isPosInScreen(gameScreenPos))
            {
                zone = null;
                Point p = mouseInSetaZero.toVector2().toPoint();
                foreach (EnemySpawnZone e in EnemyManager.Instance.getEnemySpawnZones())
                {
                    if (e.getZone().Contains(p))
                    {
                        zone = e;
                        break;
                    }
                }
            }

            if (justPressedKey(Keys.Delete) && zone != null)
            {
                EnemyManager.Instance.getEnemySpawnZones().Remove(zone);
                zone = null;
            }
            else if (zone != null && mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 current = new Vector2(mouseState.X, mouseState.Y);
                Vector2 last = new Vector2(lastMouseState.X, lastMouseState.Y);

                Rectangle rect = zone.getZone();
                Point location = rect.Location;
                location.X += (int)(current.X - last.X);
                location.Y -= (int)(current.Y - last.Y);
                rect.Location = location;
                zone. setZone(rect);
            }

        }

        public override void exit()
        {
            base.exit();
        }

        public override void render()
        {
            foreach (EnemySpawnZone e in EnemyManager.Instance.getEnemySpawnZones())
            {
                DebugManager.Instance.addRectangle(e.getZone(), e == zone ? Color.Yellow : Color.Blue, 1.0f);

                Vector3 pos = GraphicsManager.Instance.graphicsDevice.Viewport.Project(e.getZone().Center.toVector3(), Camera2D.projection, Camera2D.view, Matrix.Identity);
                DebugManager.Instance.addText(pos.toVector2(), e.getEnemyName());
                DebugManager.Instance.addText(pos.toVector2() + new Vector2(0, 15), e.getTotalSpawns().ToString());
            }
        }
    }
}
#endif