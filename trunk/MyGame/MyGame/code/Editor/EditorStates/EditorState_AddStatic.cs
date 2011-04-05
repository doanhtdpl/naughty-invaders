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

            MyEditor.Instance.texturesCombo.Items.Clear();
            var textures = SB.content.LoadContent("textures/staticProps");
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
                LevelManager.Instance.removeStaticProp(staticEntity);
                loadEntity(currentIndex + 1);
                MyEditor.Instance.texturesCombo.SelectedIndex = currentIndex;
                MyEditor.Instance.myEditorControl.Focus();
            }
            else if (justPressedKey(Keys.PageUp) || justPressedKey(Keys.I))
            {
                LevelManager.Instance.removeStaticProp(staticEntity);
                loadEntity(currentIndex - 1);
                MyEditor.Instance.texturesCombo.SelectedIndex = currentIndex;
                MyEditor.Instance.myEditorControl.Focus();
            }
            else if (justPressedLeftButton() && isPosInScreen(gameScreenPos))
            {
                staticEntity = null;
                MyEditor.Instance.changeState(new EditorState_AddStatic(currentIndex));
            }

            if (staticEntity != null)
            {
                staticEntity.position2D = new Vector2(mouseInSetaZero.X, mouseInSetaZero.Y);
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
            staticEntity = new RenderableEntity2D("staticProps", textures[currentIndex], position, 0, Color.White);
            LevelManager.Instance.addStaticProp(staticEntity);
#endif
        }

        public override void selectEntity(int index)
        {
            LevelManager.Instance.removeStaticProp(staticEntity);
            loadEntity(index);
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