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
    class EditorState_AddStatic : EditorState
    {
        int currentIndex = 0;
        private Entity2D staticEntity;

        public EditorState_AddStatic()
            : base()
        {
        }

        public EditorState_AddStatic(int _currentIndex)
            : base()
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
                LevelManager.Instance.removeStaticProp(staticEntity);
                loadEntity(currentIndex + 1);
            }
            else if (justPressedKey(Keys.Left))
            {
                LevelManager.Instance.removeStaticProp(staticEntity);
                loadEntity(currentIndex - 1);
            }
            else if (justPressedLeftButton() && isPosInScreen(gameScreenPos))
            {
                staticEntity = null;
                MyEditor.Instance.changeState(new EditorState_AddStatic(currentIndex));
            }

            if (staticEntity != null)
            {
                Vector3 near = SB.graphicsDevice.Viewport.Unproject(new Vector3(gameScreenPos.X, gameScreenPos.Y, 0.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
                Vector3 far = SB.graphicsDevice.Viewport.Unproject(new Vector3(gameScreenPos.X, gameScreenPos.Y, 1.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
                Vector3 normal = new Vector3(0, 0, -1);

                float u = Vector3.Dot(normal, Vector3.Zero - near) / Vector3.Dot(normal, far - near);
                Vector3 pos = near + u * (far - near);

                staticEntity.position2D = new Vector2(pos.X, pos.Y);
            }
        }

        public override void exit()
        {
            base.exit();

            if (staticEntity != null)
            {
                LevelManager.Instance.removeStaticProp(staticEntity);
            }
        }

        public void loadEntity(int index)
        {
#if EDITOR
            var textures = SB.content.LoadContent("textures/staticProps");
            currentIndex = (index + textures.Count) % textures.Count;
            Texture2D texture = TextureManager.Instance.getTexture("staticProps", textures[currentIndex]);
            Vector3 position = Camera2D.position;
            position.Z = 0.0f;
            staticEntity = new RenderableEntity2D("staticProps", textures[currentIndex], position, 0);
            LevelManager.Instance.addStaticProp(staticEntity);
#endif
        }

        public override void render()
        {
            if (staticEntity != null)
            {
                EditorHelper.Instance.renderEntityQuad(staticEntity);
            }
        }
    }
}
