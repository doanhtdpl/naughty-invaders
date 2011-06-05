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

            MyEditor.Instance.enemiesCombo.Items.Clear();
            var textures = SB.content.LoadContent("xml/enemies");
            for (int i = 0; i < textures.Count(); i++)
            {
                MyEditor.Instance.enemiesCombo.Items.Add(textures[i]);
            }

            MyEditor.Instance.enemiesCombo.SelectedIndex = 0;
            MyEditor.Instance.myEditorControl.Focus();

            MyEditor.Instance.enemyCount.Show();
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
                if (zone != null)
                {
                    MyEditor.Instance.enemiesCombo.Text = zone.getEnemyName();
                    MyEditor.Instance.enemyCount.Text = zone.getTotalSpawns().ToString();
                }
            }

            if (justPressedKey(Keys.Delete) && zone != null)
            {
                EnemyManager.Instance.getEnemySpawnZones().Remove(zone);
                zone = null;
            }
            else if (zone != null && mouseState.LeftButton == ButtonState.Pressed && isPosInScreen(gameScreenPos))
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

        public void setEnemy(string name)
        {
            if(zone != null)
                zone.setEnemyName(name);
        }

        public void setCount(int count)
        {
            if (zone != null)
                zone.setTotalSpawns(count);
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