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
    class EditorState_AddEnemy : EditorState
    {
        int currentIndex = 0;
        private Entity2D entity;

        public EditorState_AddEnemy()
            : base()
        {
        }

        public EditorState_AddEnemy(int _currentIndex)
            : base()
        {
            currentIndex = _currentIndex;
        }

        public override void enter()
        {
            base.enter();
            loadEntity(currentIndex);

            MyEditor.Instance.texturesCombo.Items.Clear();
            var textures = SB.content.LoadContent("xml/enemies");
            for (int i = 0; i < textures.Count(); i++)
            {
                MyEditor.Instance.texturesCombo.Items.Add(textures[i]);
            }

            MyEditor.Instance.texturesCombo.SelectedIndex = currentIndex;
            MyEditor.Instance.myEditorControl.Focus();
        }

        public override void update()
        {
            base.update();

            if (justPressedKey(Keys.PageDown) || justPressedKey(Keys.O))
            {
                EnemyManager.Instance.removeEnemy(entity);
                loadEntity(currentIndex + 1);
                MyEditor.Instance.texturesCombo.SelectedIndex = currentIndex;
                MyEditor.Instance.myEditorControl.Focus();
            }
            else if (justPressedKey(Keys.PageUp) || justPressedKey(Keys.I))
            {
                EnemyManager.Instance.removeEnemy(entity);
                loadEntity(currentIndex - 1);
                MyEditor.Instance.texturesCombo.SelectedIndex = currentIndex;
                MyEditor.Instance.myEditorControl.Focus();
            }
            else if (justPressedLeftButton() && isPosInScreen(gameScreenPos))
            {
                entity = null;
                MyEditor.Instance.changeState(new EditorState_AddEnemy(currentIndex));
            }

            if (entity != null)
            {
                entity.position2D = new Vector2(mouseInSetaZero.X, mouseInSetaZero.Y);
            }
        }

        public override void exit()
        {
            base.exit();

            if (entity != null)
            {
                EnemyManager.Instance.removeEnemy(entity);
            }
        }

        public void loadEntity(int index)
        {
#if EDITOR
            var textures = SB.content.LoadContent("xml/enemies");
            currentIndex = (index + textures.Count) % textures.Count;
            Vector3 position = new Vector3(Camera2D.position.X, Camera2D.position.Y, 0.0f);

            entity = EnemyManager.Instance.addEnemy(textures[currentIndex], position);
#endif
        }

        public override void selectEntity(int index)
        {
            EnemyManager.Instance.removeEnemy(entity);
            loadEntity(index);
        }

        public override void render()
        {
            if (entity != null)
            {
                EditorHelper.Instance.renderEntityQuad(entity);
            }
        }
    }
}
