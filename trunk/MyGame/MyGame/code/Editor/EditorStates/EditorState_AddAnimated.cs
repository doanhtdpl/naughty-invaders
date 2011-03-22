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
    class EditorState_AddAnimated : EditorState
    {
        int currentIndex = 0;
        private Entity2D entity;

        public EditorState_AddAnimated()
            : base()
        {
        }

        public EditorState_AddAnimated(int _currentIndex)
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
                LevelManager.Instance.removeAnimatedProp(entity);
                loadEntity(currentIndex + 1);
            }
            else if (justPressedKey(Keys.Left))
            {
                LevelManager.Instance.removeAnimatedProp(entity);
                loadEntity(currentIndex - 1);
            }
            else if (justPressedLeftButton() && isPosInScreen(gameScreenPos))
            {
                entity = null;
                MyEditor.Instance.changeState(new EditorState_AddAnimated(currentIndex));
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
                LevelManager.Instance.removeAnimatedProp(entity);
            }
        }

        public void loadEntity(int index)
        {
#if EDITOR
            var textures = SB.content.LoadContent("xml/animatedProps");
            currentIndex = (index + textures.Count) % textures.Count;
            Vector3 position = Camera2D.position;
            position.Z = 0.0f;
            AnimatedEntity2D ent = new AnimatedEntity2D("animatedProps", textures[currentIndex], position, 0, Color.White);
            LevelManager.Instance.addAnimatedProp(ent);
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
