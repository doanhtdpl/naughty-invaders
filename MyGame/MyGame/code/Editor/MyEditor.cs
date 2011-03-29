using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private List<Entity2D> selectedEntities = new List<Entity2D>();

        public static System.Drawing.Color SELECTED_COLOR = System.Drawing.Color.Green;
        public static System.Drawing.Color UNSELECTED_COLOR = System.Drawing.Color.FromArgb(212, 208, 200);

        EditorState currentState = null;
        EditorState nextState = null;
        public MouseState lastMouseState;
        public MouseState mouseState;
        public KeyboardState lastKeyState;
        public KeyboardState keyState;

        public int exitBlockers = 0;
        public bool drawGrid = false;
        public int gridSpacing = 128;

        public bool skipNextFrame = false;
        public int noUpdate = 0;

        public MyEditor()
        {
            InitializeComponent();
            Instance = this;
        }

        #region Selected Entitie(s)

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

        private void buttonCreateGroup_Click(object sender, EventArgs e)
        {
            createGroup();
        }
        #endregion

        #region Load/Save
        private bool openDialog(string title, ref string fileName)
        {
            exitBlockers++;
            noUpdate++;

            OpenFileDialog fileDialog = new OpenFileDialog();
            string relativePath = "../../../../MyGameContent/xml/levels";
            string fullPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = fullPath;
            fileDialog.Title = title;
            fileDialog.Filter = "Level files (*.xml)|*.xml";
            fileDialog.CheckFileExists = false;
            fileDialog.RestoreDirectory = true;

            skipNextFrame = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = fileDialog.FileName;
                exitBlockers--;
                noUpdate--;
                myEditorControl.Focus();
                return true;
            }
            exitBlockers--;
            noUpdate--;
            myEditorControl.Focus();
            return false;
        }

        private void buttonLoadLevel_Click(object sender, EventArgs e)
        {
            string fileName = "";
            if (openDialog("Load Level", ref fileName))
            {
                EditorHelper.Instance.loadNewLevel(fileName);
                currentState = null;
            }
        }


        private void buttonImportLevel_Click(object sender, EventArgs e)
        {
            string fileName = "";
            if (openDialog("Import level", ref fileName))
            {
                selectedEntities = EditorHelper.Instance.importLevel(fileName);
                createGroup();
            }
        }

        private void buttonSaveLevel_Click(object sender, EventArgs e)
        {
            string fileName = "";
            if (openDialog("Save level", ref fileName))
            {
                EditorHelper.Instance.saveLevelToXML(fileName);
                currentState = null;
            }
        }
        #endregion

        #region Update/Render
        public bool update()
        {
            mouseState = Mouse.GetState();
            keyState = Keyboard.GetState();

            if (noUpdate > 0)
            {
                return true;
            }

            //skip frame f.e. when coming back from load/save dialog
            if (skipNextFrame)
            {
                skipNextFrame = false;
                return true;
            }

            if (texturesCombo.Focused)
            {
                return true;
            }

            if (nextState != null)
            {
                doChangeState();
            }

            //EXIT
            if(exitBlockers <= 0 && justPressedKey(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                return false;
            }

            //GRID
            if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.G))
            {
                drawGrid = !drawGrid;
            }

            //STATE CHANGE
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                changeState(new EditorState_MoveState());
            }
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.W))
            {
                changeState(new EditorState_RotateState());
            }
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.E))
            {
                changeState(new EditorState_ScaleState());
            }

            //RESET
            //else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.R))
            //{
            //    EntityManager.Instance.reset();
            //}

            //MOVE CAMERA
            else if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Camera2D.position.X -= (mouseState.X - lastMouseState.X) * (Camera2D.position.Z / 1000);
                Camera2D.position.Y += (mouseState.Y - lastMouseState.Y) * (Camera2D.position.Z / 1000);
            }
            else if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                //Camera2D.position.Z -= (mouseState.Y - lastMouseState.Y);
                Camera2D.position.Z -= (mouseState.Y - lastMouseState.Y) * (Camera2D.position.Z / 400);
            }

            //DELETE
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.Delete) && myEditorControl.Focused)
            {
                foreach (Entity2D ent in selectedEntities)
                {
                    ent.delete();
                }
                selectedEntities.Clear();
            }

            //DUPLICATE
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.D))
            {
                if (anyEntitySelected())
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
                            newEntity = new AnimatedEntity2D("animatedProps", ent.entityName, ent.position, 0, Color.White);
                            LevelManager.Instance.addAnimatedProp(newEntity);
                        }
                        else if (ent is RenderableEntity2D)
                        {
                            newEntity = new RenderableEntity2D("staticProps", ent.entityName, ent.position, 0, Color.White);
                            LevelManager.Instance.addStaticProp(newEntity);
                        }

                        newEntity.worldMatrix = ent.worldMatrix;

                        if (newEntity != null)
                        {
                            newEntities.Add(newEntity);
                        }
                    }

                    selectedEntities.Clear();
                    selectedEntities = newEntities;
                }
            }

            //UPDATE STATE
            else if (currentState != null)
            {
                currentState.update();
            }

            lastMouseState = mouseState;
            lastKeyState = keyState;

            return true;
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

            if (drawGrid)
            {
                EditorHelper.Instance.renderGrid(gridSpacing);
            }

            CameraManager.Instance.render();
        }
        #endregion

        #region State
        public void changeState(EditorState newState)
        {
            nextState = newState;
            selectButton();
            myEditorControl.Focus();
        }

        public void doChangeState()
        {
            if (currentState != null)
                currentState.exit();

            currentState = nextState;
            currentState.enter();

            nextState = null;
        }

        private void selectButton()
        {
            buttonMove.BackColor = nextState is EditorState_MoveState ? SELECTED_COLOR : UNSELECTED_COLOR;
            buttonRotate.BackColor = nextState is EditorState_RotateState ? SELECTED_COLOR : UNSELECTED_COLOR;
            buttonScale.BackColor = nextState is EditorState_ScaleState ? SELECTED_COLOR : UNSELECTED_COLOR;
            buttonAddStatic.BackColor = nextState is EditorState_AddStatic ? SELECTED_COLOR : UNSELECTED_COLOR;
            buttonAddAnimated.BackColor = nextState is EditorState_AddAnimated ? SELECTED_COLOR : UNSELECTED_COLOR;
            buttonAddEnemy.BackColor = nextState is EditorState_AddEnemy ? SELECTED_COLOR : UNSELECTED_COLOR;
        }
        #endregion

        private void createGroup()
        {
            List<int> group = new List<int>();
            foreach (Entity2D ent in selectedEntities)
            {
                group.Add(ent.id);
            }
            LevelManager.Instance.addGroup(group);
        }

        #region Properties fields
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

        public virtual void propertiesChanged()
        {
            if (anyEntitySelected())
            {
                Entity2D ent = selectedEntities[0];
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
        #endregion

        #region Reset
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
                    if(ent is AnimatedEntity2D)
                    {
                        ent.scale2D = ((AnimatedEntity2D)ent).getFrameSize();
                    }
                    else
                    {
                        Texture2D texture = ((RenderableEntity2D)ent).Texture;
                        if (texture != null)
                        {
                            ent.scale2D = new Vector2(texture.Width, texture.Height);
                        }
                    }
                }
            }
        }
        #endregion

        #region Input
        public MouseState getMouseState() { return mouseState; }
        public MouseState getLastMouseState() { return lastMouseState; }
        public KeyboardState getKeyState() { return lastKeyState; }
        public KeyboardState getLastKeyState() { return keyState; }

        public bool justPressedKey(Microsoft.Xna.Framework.Input.Keys key)
        {
            return keyState.IsKeyDown(key) && !lastKeyState.IsKeyDown(key);
        }

        public bool isKeyPressed(Microsoft.Xna.Framework.Input.Keys key)
        {
            return keyState.IsKeyDown(key);
        }
        #endregion

        private void texturesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentState is EditorState_AddStatic)
            {
                //skipNextFrame = true;
                ((EditorState_AddStatic)currentState).selectEntity(((ComboBox)sender).SelectedIndex);
                myEditorControl.Focus();
            }
        }
    }
}
