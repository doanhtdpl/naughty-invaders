using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public partial class MyEditor : Form
    {
        public static MyEditor Instance;

        EditorState currentState = null;
        EditorState nextState = null;
        public Entity2D selectedEntity = null;
        public MouseState lastMouseState;
        public MouseState mouseState;
        public KeyboardState lastKeyState;
        public KeyboardState keyState;

        public MyEditor()
        {
            InitializeComponent();
            Instance = this;
        }

        public MouseState getMouseState() { return mouseState; }
        public MouseState getLastMouseState() { return lastMouseState; }
        public KeyboardState getKeyState() { return lastKeyState; }
        public KeyboardState getLastKeyState() { return keyState; }

        private void validateInputs(object sender)
        {
            if (currentState != null)
            {
                float value;
                if (float.TryParse(((TextBox)sender).Text, out value))
                {
                    propertiesChanged();
                }
                else
                {
                    updateEntityProperties();
                }
            }
        }

        public void changeState(EditorState newState)
        {
            nextState = newState;
        }

        public void doChangeState()
        {
            if (currentState != null)
                currentState.exit();

            currentState = nextState;
            currentState.enter();

            nextState = null;
        }

        #region Events
        protected void textChange(object sender, EventArgs e)
        {
            validateInputs(sender);
        }

        private void keyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // PRESS ENTER
            {
                validateInputs(sender);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender == buttonMove)
            {
                changeState(new EditorState_MoveState());
            }
            else if (sender == buttonRotate)
            {
                changeState(new EditorState_RotateState());
            }
            else if (sender == buttonScale)
            {
                changeState(new EditorState_ScaleState());
            }
            else if (sender == buttonAddStatic)
            {
                changeState(new EditorState_AddStatic());
            }
            else if (sender == buttonAddAnimated)
            {
                changeState(new EditorState_AddAnimated());
            }
            else if (sender == buttonAddEnemy)
            {
                changeState(new EditorState_AddEnemy());
            }
            selectButton(sender);
            myEditorControl.Focus();
        }

        private void buttonResetPosition_Click(object sender, EventArgs e)
        {
            resetPosition();
        }

        private void buttonResetRotation_Click(object sender, EventArgs e)
        {
            resetRotation();
        }

        private void buttonResetScale_Click(object sender, EventArgs e)
        {
            resetScale();
        }
        #endregion

        private void selectButton(object button)
        {
            buttonMove.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);
            buttonRotate.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);
            buttonScale.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);
            buttonAddStatic.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);
            buttonAddAnimated.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);
            buttonAddEnemy.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);

            ((Button)button).BackColor = System.Drawing.Color.Green;
        }

        #region Load/Save
        private void buttonLoadLevel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            string relativePath = "../../../../MyGameContent/xml/levels";
            string fullPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = fullPath;
            fileDialog.Title = "Save Level";
            fileDialog.Filter = "Level files (*.xml)|*.xml";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                EditorHelper.Instance.loadLevelFromXML(fileDialog.FileName);
                currentState = null;
            }
        }

        private void buttonSaveLevel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            string relativePath = "../../../../MyGameContent/xml/levels";
            string fullPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = fullPath;
            fileDialog.Title = "Save Level";
            fileDialog.Filter = "Level files (*.xml)|*.xml";
            fileDialog.CheckFileExists = false;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                EditorHelper.Instance.saveLevelToXML(fileDialog.FileName);
                currentState = null;
            }
        }
        #endregion

        #region Update/Render
        public void update()
        {
            if (nextState != null)
            {
                doChangeState();
            }

            mouseState = Mouse.GetState();
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Camera2D.position.X -= (mouseState.X - lastMouseState.X);
                Camera2D.position.Y += (mouseState.Y - lastMouseState.Y);
            }
            else if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Camera2D.position.Z -= (mouseState.Y - lastMouseState.Y);
            }
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.Delete))
            {
                //DELETE
                selectedEntity.delete();
                selectedEntity = null;
            }
            else if (currentState != null)
            {
                currentState.update();
            }

            lastMouseState = mouseState;
            lastKeyState = keyState;
        }

        public void render()
        {
            if (currentState != null)
                currentState.render();

            if (selectedEntity != null)
            {
                EditorHelper.Instance.renderEntityQuad(selectedEntity);
            }
        }
        #endregion

        public virtual void propertiesChanged()
        {
            if (selectedEntity != null)
            {
                selectedEntity.position = new Vector3(MyEditor.Instance.textPosX.Text.toFloat(),
                    MyEditor.Instance.textPosY.Text.toFloat(),
                    MyEditor.Instance.textPosZ.Text.toFloat());

                selectedEntity.orientation = MyEditor.Instance.textRotZ.Text.toFloat() / (float)(360 / (Math.PI * 2));
                selectedEntity.scale2D = new Vector2(MyEditor.Instance.textScaleX.Text.toFloat(),
                    MyEditor.Instance.textScaleY.Text.toFloat());
            }
        }

        public void updateEntityProperties()
        {
            if (selectedEntity != null)
            {
                MyEditor.Instance.textPosX.Text = selectedEntity.position.X.ToString();
                MyEditor.Instance.textPosY.Text = selectedEntity.position.Y.ToString();
                MyEditor.Instance.textPosZ.Text = selectedEntity.position.Z.ToString();
                //MyEditor.Instance.textRotX.Text = entity.orientation.X.ToString();
                //MyEditor.Instance.textRotY.Text = entity.orientation.Y.ToString();
                MyEditor.Instance.textRotZ.Text = (selectedEntity.orientation * (float)(360 / (Math.PI * 2))).ToString();
                MyEditor.Instance.textScaleX.Text = selectedEntity.scale2D.X.ToString();
                MyEditor.Instance.textScaleY.Text = selectedEntity.scale2D.Y.ToString();
            }
        }

        public void resetRotation()
        {
            if (selectedEntity != null)
            {
                selectedEntity.resetRotation();
            }
        }

        public void resetPosition()
        {
            if (selectedEntity != null)
            {
                selectedEntity.position = new Vector3(0, 0, 0);
            }
        }

        public void resetScale()
        {
            if (selectedEntity != null)
            {
                Texture2D texture = ((RenderableEntity2D)selectedEntity).Texture;
                selectedEntity.scale2D = selectedEntity.scale2D = new Vector2(texture.Width, texture.Height);
            }
        }

        public bool justPressedKey(Microsoft.Xna.Framework.Input.Keys key)
        {
            return keyState.IsKeyDown(key) && !lastKeyState.IsKeyDown(key);
        }
    }
}
