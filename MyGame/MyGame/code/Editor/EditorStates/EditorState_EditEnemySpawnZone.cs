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
                DebugManager.Instance.addText(e.getZone().Center.toVector2(), e.getEnemyName());
                DebugManager.Instance.addText(e.getZone().Center.toVector2() + new Vector2(0, 10), e.getTotalSpawns().ToString());
            }
        }
    }
}
#endif