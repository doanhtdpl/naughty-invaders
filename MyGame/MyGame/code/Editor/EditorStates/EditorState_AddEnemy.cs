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

        public EditorState_AddEnemy():base()
        {
            currentIndex = -1;
        }

        public EditorState_AddEnemy(int _currentIndex):base()
        {
            currentIndex = _currentIndex;
        }

        public override void enter()
        {
            base.enter();
            loadEntity(currentIndex);
        }

        public override void update()
        {
            base.update();

            if (justPressedKey(Keys.Right))
            {
                EnemyManager.Instance.removeEnemy(entity);
                loadEntity(currentIndex + 1);
            }
            else if (justPressedKey(Keys.Left))
            {
                EnemyManager.Instance.removeEnemy(entity);
                loadEntity(currentIndex - 1);
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

        public override void render()
        {
            if (entity != null)
            {
                EditorHelper.Instance.renderEntityQuad(entity);
            }
        }
    }
}
