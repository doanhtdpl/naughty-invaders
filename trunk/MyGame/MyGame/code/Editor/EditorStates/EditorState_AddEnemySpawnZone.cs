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
    class EditorState_AddEnemySpawnZone : EditorState
    {
        Rectangle rect;
        bool rectStarted = false;

        public EditorState_AddEnemySpawnZone()
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

            if (justPressedKey(Keys.PageDown) || justPressedKey(Keys.O))
            {
                MyEditor.Instance.texturesCombo.SelectedIndex++;
                MyEditor.Instance.myEditorControl.Focus();
            }
            else if (justPressedKey(Keys.PageUp) || justPressedKey(Keys.I))
            {
                MyEditor.Instance.texturesCombo.SelectedIndex--;
            }
            else if (justPressedLeftButton() && isPosInScreen(gameScreenPos))
            {
                rect = new Rectangle((int)mouseInSetaZero.X, (int)mouseInSetaZero.Y, 0, 0);
                rectStarted = true;
            }
            else if (justReleasedLeftButton() && rectStarted)
            {
                rect.Width = (int)mouseInSetaZero.X - rect.X;
                rect.Height = (int)mouseInSetaZero.Y - rect.Y;
                if (rect.Height < 0)
                {
                    rect.Y += rect.Height;
                    rect.Height = -rect.Height;
                }

                if (rect.Width < 0)
                {
                    rect.X += rect.Width;
                    rect.Width = -rect.Width;
                }

                EnemyManager.Instance.addEnemySpawnZone(new EnemySpawnZone(MyEditor.Instance.enemiesCombo.Text, rect, MyEditor.Instance.enemyCount.Text.toInt()));

                rectStarted = false;
            }
            else if (rectStarted)
            {
                rect.Width = (int)mouseInSetaZero.X - rect.X;
                rect.Height = (int)mouseInSetaZero.Y - rect.Y;
            }
        }

        public override void exit()
        {
            base.exit();

            //MyEditor.Instance.enemyCount.Hide();
        }

        public override void render()
        {
            if (rectStarted)
            {
                DebugManager.Instance.addRectangle(rect, Color.Yellow, 1.0f);
            }

            foreach (EnemySpawnZone e in EnemyManager.Instance.getEnemySpawnZones())
            {
                DebugManager.Instance.addRectangle(e.getZone(), Color.Blue, 1.0f);
            }
        }
    }
}
#endif