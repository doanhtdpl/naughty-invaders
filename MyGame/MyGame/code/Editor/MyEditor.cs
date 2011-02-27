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

        #region Selected Entitie(s)
        private List<Entity2D> selectedEntities = new List<Entity2D>();

        public bool anyEntitySelected() { return selectedEntities.Count > 0; }
        public List<Entity2D> getSelectedEntities() { return selectedEntities; }

        public void selectEntity(Entity2D entity)
        {
            selectedEntities.Clear();
            selectedEntities.Add(entity);
        }

        public void addEntity(Entity2D entity)
        {
            if (selectedEntities.IndexOf(entity) == -1)
                selectedEntities.Add(entity);
            else
                selectedEntities.Remove(entity);
        }

        #endregion

 EditorState currentState = null;
        EditorState nextState = null;
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
                foreach (Entity2D ent in selectedEntities)
                {
                    ent.delete();
                }
                selectedEntities.Clear();
            }
            else if(justPressedKey(Microsoft.Xna.Framework.Input.Keys.D))
            {
                //Duplicate
                if(anyEntitySelected())
                {
                    List<Entity2D> newEntities = new List<Entity2D>();

                    foreach (Entity2D ent in selectedEntities)
                    {
                        Entity2D newEntity = null;
                        if (ent is Enemy)
                        {
                            newEntity = EnemyManager.Instance.addEnemy(ent.entityName, ent.position);
                        }
                        else if (ent is AnimatedEntity2D)
                        {
                            newEntity = new AnimatedEntity2D("animatedProps", ent.entityName, ent.position, 0);
                            LevelManager.Instance.addAnimatedProp(newEntity);
                        }
                        else if (ent is RenderableEntity2D)
                        {
                            newEntity = new RenderableEntity2D("staticProps", ent.entityName, ent.position, 0);
                            LevelManager.Instance.addStaticProp(newEntity);
                        }

                        if (newEntity != null)
                        {
                            newEntities.Add(newEntity);
                        }
                    }

                    selectedEntities.Clear();
                    selectedEntities = newEntities;
                }
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

            foreach (Entity2D ent in selectedEntities)
            {
                if (ent != null)
                {
                    EditorHelper.Instance.renderEntityQuad(ent);
                }
            }
        }
        #endregion

        public virtual void propertiesChanged()
        {
            foreach (Entity2D ent in selectedEntities)
            {
                if (ent != null)
                {
                    ent.position = new Vector3(MyEditor.Instance.textPosX.Text.toFloat(),
                        MyEditor.Instance.textPosY.Text.toFloat(),
                        MyEditor.Instance.textPosZ.Text.toFloat());

                    ent.orientation = MyEditor.Instance.textRotZ.Text.toFloat() / (float)(360 / (Math.PI * 2));
                    ent.scale2D = new Vector2(MyEditor.Instance.textScaleX.Text.toFloat(),
                        MyEditor.Instance.textScaleY.Text.toFloat());
                }
            }
        }

        public void updateEntityProperties()
        {
            if (anyEntitySelected())
            {
                Entity2D ent = selectedEntities[0];
                if (ent != null)
                {
                    MyEditor.Instance.textPosX.Text = ent.position.X.ToString();
                    MyEditor.Instance.textPosY.Text = ent.position.Y.ToString();
                    MyEditor.Instance.textPosZ.Text = ent.position.Z.ToString();
                    MyEditor.Instance.textRotZ.Text = (ent.orientation * (float)(360 / (Math.PI * 2))).ToString();
                    MyEditor.Instance.textScaleX.Text = ent.scale2D.X.ToString();
                    MyEditor.Instance.textScaleY.Text = ent.scale2D.Y.ToString();
                }
            }
        }

        public void resetRotation()
        {
            foreach (Entity2D ent in selectedEntities)
            {
                if (ent != null)
                {
                    ent.resetRotation();
                }
            }
        }

        public void resetPosition()
        {
            foreach (Entity2D ent in selectedEntities)
            {
                if (ent != null)
                {
                    ent.position = new Vector3(0, 0, 0);
                }
            }
        }

        public void resetScale()
        {
            foreach (Entity2D ent in selectedEntities)
            {
                if (ent != null)
                {
                    Texture2D texture = ((RenderableEntity2D)ent).Texture;
                    ent.scale2D = ent.scale2D = new Vector2(texture.Width, texture.Height);
                }
            }
        }

        public bool justPressedKey(Microsoft.Xna.Framework.Input.Keys key)
        {
            return keyState.IsKeyDown(key) && !lastKeyState.IsKeyDown(key);
        }

        public bool isKeyPressed(Microsoft.Xna.Framework.Input.Keys key)
        {
            return keyState.IsKeyDown(key);
        }
    }
}
