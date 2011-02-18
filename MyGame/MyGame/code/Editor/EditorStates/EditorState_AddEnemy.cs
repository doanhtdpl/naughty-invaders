﻿using System;
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
                Vector3 near = SB.graphicsDevice.Viewport.Unproject(new Vector3(gameScreenPos.X, gameScreenPos.Y, 0.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
                Vector3 far = SB.graphicsDevice.Viewport.Unproject(new Vector3(gameScreenPos.X, gameScreenPos.Y, 1.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
                Vector3 normal = new Vector3(0, 0, -1);

                float u = Vector3.Dot(normal, Vector3.Zero - near) / Vector3.Dot(normal, far - near);
                Vector3 pos = near + u * (far - near);

                entity.position2D = new Vector2(pos.X, pos.Y);
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

            Entity2D ent = EnemyManager.Instance.addEnemy(textures[currentIndex], position);
            entity = ent;
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
