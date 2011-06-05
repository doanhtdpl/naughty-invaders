﻿#if EDITOR
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

        public Vector2 gameScreenPos;

        public bool drawGrid = false;
        public int gridSpacing = 128;

        public int skipFrames= 0;
        public int noUpdate = 0;
        public int exitBlockers = 0;

        string lastLoadedLevel = null;

        public MyEditor()
        {
            InitializeComponent();
            Instance = this;

            cameraNodePanel.Hide();
        }

        #region Selected Entitie(s)

        public bool anyEntitySelected() { return selectedEntities.Count > 0 && selectedEntities[0] != null; }
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
        protected void textChangeMatrix(object sender, EventArgs e)
        {
            validateMatrixInputs(sender);
        }

        protected void textChangeColor(object sender, EventArgs e)
        {
            validateColorInputs(sender);
        }

        private void keyPressedMatrix(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // PRESS ENTER
            {
                validateMatrixInputs(sender);
                myEditorControl.Focus();
            }
        }

        private void keyPressedColor(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // PRESS ENTER
            {
                validateColorInputs(sender);
                myEditorControl.Focus();
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
            else if (sender == addCameraNodeButton)
            {
                changeState(new EditorState_AddCameraNode());
            }
            else if (sender == buttonMoveCameraNode)
            {
                changeState(new EditorState_MoveCameraNode());
            }
            else if (sender == buttonAddColisions)
            {
                changeState(new EditorState_AddColisions());
            }
            else if (sender == buttonEditColisions)
            {
                changeState(new EditorState_EditColisions());
            }
            else if (sender == addEnemyZoneButton)
            {
                changeState(new EditorState_AddEnemySpawnZone());
            }
            else if (sender == editEnemyZoneButton)
            {
                changeState(new EditorState_EditEnemySpawnZone());
            }
            else if (sender == addTriggerButton)
            {
                changeState(new EditorState_AddTrigger());
            }
            else if (sender == editTriggerButton)
            {
                changeState(new EditorState_EditTrigger());
            }
            else if (sender == addEffectButton)
            {
                changeState(new EditorState_AddEffect());
            }
            else if (sender == editEffectButton)
            {
                changeState(new EditorState_EditEffect());
            }
            else if (sender == editEffectsButton)
            {
                changeState(new EditorState_EditEffects());
            }
            else if (sender == editPlayerPosButton)
            {
                changeState(new EditorState_EditPlayerPos());
            }
            myEditorControl.Focus();
        }

        private void buttonResetPosition_Click(object sender, EventArgs e)
        {
            resetPosition();
            myEditorControl.Focus();
        }

        private void buttonResetRotation_Click(object sender, EventArgs e)
        {
            resetRotation();
            myEditorControl.Focus();
        }

        private void buttonResetScale_Click(object sender, EventArgs e)
        {
            resetScale();
            myEditorControl.Focus();
        }

        private void buttonCreateGroup_Click(object sender, EventArgs e)
        {
            createGroup();
            myEditorControl.Focus();
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

            skipFrames = 3;

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
                lastLoadedLevel = fileName;
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

        private void buttonSaveLevelAs_Click(object sender, EventArgs e)
        {
            string fileName = "";
            if (openDialog("Save level", ref fileName))
            {
                EditorHelper.Instance.saveLevelToXML(fileName);
                currentState = null;
            }
        }

        private void buttonSaveLevel_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.saveLevelToXML(lastLoadedLevel);
            currentState = null;
        }

        private void buttonNewLevel_Click(object sender, EventArgs e)
        {
            LevelManager.Instance.cleanLevel();
        }
        #endregion

        #region Update/Render
        public bool update()
        {
            if (((StateGame)StateManager.getCurrentState()).gameRunning)
            {
                tabControl1.Enabled = false;
                return true;
            }

            if (!tabControl1.Enabled)
                tabControl1.Enabled = true;

            mouseState = Mouse.GetState();
            keyState = Keyboard.GetState();

            System.Drawing.Point point = MyEditor.Instance.myEditorControl.PointToClient(new System.Drawing.Point(MyEditor.Instance.getMouseState().X, MyEditor.Instance.getMouseState().Y));
            gameScreenPos = new Vector2(point.X, point.Y);

            if (noUpdate > 0)
            {
                return true;
            }

            if ( (isKeyPressed(Microsoft.Xna.Framework.Input.Keys.LeftControl) || isKeyPressed(Microsoft.Xna.Framework.Input.Keys.RightControl)) && justPressedKey(Microsoft.Xna.Framework.Input.Keys.Z))
            {
                noUpdate++;
                MessageBox.Show(MyEditor.ActiveForm, "PULSA OK PARA FORMATEAR EL DISCO DURO.", "Control SETA");
                MessageBox.Show(MyEditor.ActiveForm, "FORMATEANDO...", "Control SETA");
                noUpdate--;
            }

            //skip frame f.e. when coming back from load/save dialog
            if (skipFrames > 0)
            {
                skipFrames--;
                return true;
            }

            if (texturesCombo.Focused)
            {
                return true;
            }

            if (currentState == null)
            {
                changeState(new EditorState_MoveState());
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
            if (isKeyPressed(Microsoft.Xna.Framework.Input.Keys.H))
            {
                EntityManager.Instance.renderCollisionParts();
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
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.B))
            {
                colorButton_Click(null, null);
            }
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.D8))
            {
                CameraManager.Instance.speedMultiplier = 1.0f;
            }
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.D9))
            {
                CameraManager.Instance.speedMultiplier -= 0.5f;
            }
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.D0))
            {
                CameraManager.Instance.speedMultiplier += 0.5f;
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
            else if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
            }

            //DELETE
            else if (justPressedKey(Microsoft.Xna.Framework.Input.Keys.Delete) && anyEntitySelected())
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

                    foreach (RenderableEntity2D ent in selectedEntities)
                    {
                        RenderableEntity2D newEntity = null;
                        if (ent is Enemy)
                        {
                            newEntity = (RenderableEntity2D)EnemyManager.Instance.addEnemy(ent.entityName, ent.position);
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

                        newEntity.color = ent.color;
                        newEntity.flipHorizontal = ent.flipHorizontal;
                        newEntity.flipVertical = ent.flipVertical;

                        newEntity.worldMatrix = ent.worldMatrix;

                        if (newEntity != null)
                        {
                            newEntities.Add(newEntity);
                        }
                    }
                    selectedEntities.Clear();
                    selectedEntities = newEntities;

                    if (selectedEntities.Count > 1)
                        createGroup();
                }
            }

            //UPDATE STATE
            else if (currentState != null)
            {
                currentState.update();
            }

            if (anyEntitySelected())
            {
                currentEntityName.Text = selectedEntities[0].entityName;
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

            CameraManager.Instance.renderDebug();
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
            addCameraNodeButton.BackColor = nextState is EditorState_AddCameraNode ? SELECTED_COLOR : UNSELECTED_COLOR;
            buttonMoveCameraNode.BackColor = nextState is EditorState_MoveCameraNode ? SELECTED_COLOR : UNSELECTED_COLOR;
            buttonAddColisions.BackColor = nextState is EditorState_AddColisions ? SELECTED_COLOR : UNSELECTED_COLOR;
            buttonEditColisions.BackColor = nextState is EditorState_EditColisions ? SELECTED_COLOR : UNSELECTED_COLOR;
            addEnemyZoneButton.BackColor = nextState is EditorState_AddEnemySpawnZone ? SELECTED_COLOR : UNSELECTED_COLOR;
            editEnemyZoneButton.BackColor = nextState is EditorState_EditEnemySpawnZone ? SELECTED_COLOR : UNSELECTED_COLOR;
            addTriggerButton.BackColor = nextState is EditorState_AddTrigger ? SELECTED_COLOR : UNSELECTED_COLOR;
            editTriggerButton.BackColor = nextState is EditorState_EditTrigger ? SELECTED_COLOR : UNSELECTED_COLOR;
            addEffectButton.BackColor = nextState is EditorState_AddEffect ? SELECTED_COLOR : UNSELECTED_COLOR;
            editEffectButton.BackColor = nextState is EditorState_EditEffect ? SELECTED_COLOR : UNSELECTED_COLOR;
            editEffectsButton.BackColor = nextState is EditorState_EditEffects ? SELECTED_COLOR : UNSELECTED_COLOR;
            editPlayerPosButton.BackColor = nextState is EditorState_EditPlayerPos ? SELECTED_COLOR : UNSELECTED_COLOR;
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
        private void validateMatrixInputs(object sender)
        {
            float value;
            if (float.TryParse(((TextBox)sender).Text, out value))
            {
                if (anyEntitySelected())
                {
                    if (selectedEntities.Count == 1)
                    {
                        RenderableEntity2D ent = (RenderableEntity2D)selectedEntities[0];
                        if (ent != null)
                        {
                            ent.position = new Vector3(textPosX.Text.toFloat(), textPosY.Text.toFloat(), textPosZ.Text.toFloat());

                            ent.orientation = MyEditor.Instance.textRotZ.Text.toFloat() / (float)(360 / (Math.PI * 2));
                            ent.scale2D = new Vector2(textScaleX.Text.toFloat(), textScaleY.Text.toFloat());

                        }
                    }
                }
            }
            else
            {
                //If it's not valid, we reset the textbox
                updateEntityProperties();
            }
        }

        private void validateColorInputs(object sender)
        {
            int value;
            if (int.TryParse(((TextBox)sender).Text, out value))
            {
                if (anyEntitySelected())
                {
                    foreach (RenderableEntity2D rent in selectedEntities)
                    {
                        rent.color.A = Byte.Parse(this.colorA.Text);

                        float alpha = rent.color.A / 255.0f;

                        if (alpha == 0)
                        {
                            alpha = 1.0f;
                        }

                        rent.color.R = (byte)(Byte.Parse(this.colorR.Text) * alpha);
                        rent.color.G = (byte)(Byte.Parse(this.colorG.Text) * alpha);
                        rent.color.B = (byte)(Byte.Parse(this.colorB.Text) * alpha);
                    }
                }
            }
            else
            {
                //If it's not valid, we reset the textbox
                updateEntityProperties();
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

                    if (ent is RenderableEntity2D)
                    {
                        float alpha = ((RenderableEntity2D)ent).color.A / 255.0f;

                        if (((RenderableEntity2D)ent).color.A == 0)
                        {
                            alpha = 1;
                        }

                        this.colorR.Text = (((RenderableEntity2D)ent).color.R / alpha).toString();
                        this.colorG.Text = (((RenderableEntity2D)ent).color.G / alpha).toString();
                        this.colorB.Text = (((RenderableEntity2D)ent).color.B / alpha).toString();
                        this.colorA.Text = ((RenderableEntity2D)ent).color.A.ToString();

                        flipHorizontalCheck.Checked = ((RenderableEntity2D)ent).flipHorizontal;
                        flipVerticalCheck.Checked = ((RenderableEntity2D)ent).flipVertical;
                    }

                }
            }
        }


        private void flipHor_CheckedChanged(object sender, EventArgs e)
        {
            if (anyEntitySelected())
            {
                foreach (RenderableEntity2D rent in selectedEntities)
                {
                    rent.flipHorizontal = flipHorizontalCheck.Checked;
                }
            }

            myEditorControl.Focus();
        }

        private void flipVer_CheckedChanged(object sender, EventArgs e)
        {
            if (anyEntitySelected())
            {
                foreach (RenderableEntity2D rent in selectedEntities)
                {
                    rent.flipVertical = flipVerticalCheck.Checked;
                }
            }

            myEditorControl.Focus();
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
            //skipNextFrame = true;
            currentState.selectEntity(((ComboBox)sender).SelectedIndex);
            myEditorControl.Focus();
        }

        private System.Drawing.Color getDialogColor(System.Drawing.Color currentColor)
        {
            exitBlockers++;
            noUpdate++;

            colorDialog.FullOpen = true;
            colorDialog.Color = currentColor;
            colorDialog.ShowDialog();

            skipFrames = 2;
            exitBlockers--;
            noUpdate--;

            myEditorControl.Focus();

            return colorDialog.Color;
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (anyEntitySelected())
            {
                System.Drawing.Color newColor = getDialogColor(System.Drawing.Color.FromArgb(int.Parse(colorA.Text), int.Parse(colorR.Text), int.Parse(colorG.Text), int.Parse(colorB.Text)));
                this.colorR.Text = newColor.R.ToString();
                this.colorG.Text = newColor.G.ToString();
                this.colorB.Text = newColor.B.ToString();
                this.colorA.Text = newColor.A.ToString();

                foreach (RenderableEntity2D rent in selectedEntities)
                {
                    rent.color.A = Byte.Parse(this.colorA.Text);

                    float alpha = rent.color.A / 255.0f;

                    if (alpha == 0)
                    {
                        alpha = 1.0f;
                    }

                    rent.color.R = (byte)(Byte.Parse(this.colorR.Text) * alpha);
                    rent.color.G = (byte)(Byte.Parse(this.colorG.Text) * alpha);
                    rent.color.B = (byte)(Byte.Parse(this.colorB.Text) * alpha);
                }
            }
        }

        private void effectColorButton_Click(object sender, EventArgs e)
        {
            if (currentState is EditorState_EditEffect)
            {
                System.Drawing.Color newColor = getDialogColor(System.Drawing.Color.FromArgb(int.Parse(colorA.Text), int.Parse(colorR.Text), int.Parse(colorG.Text), int.Parse(colorB.Text)));
                this.effectColorR.Text = newColor.R.ToString();
                this.effectColorG.Text = newColor.G.ToString();
                this.effectColorB.Text = newColor.B.ToString();
                ((EditorState_EditEffect) currentState).changeColor(new Color(newColor.R, newColor.G, newColor.B));
            }
        }

        private void BGColorButton_Click(object sender, EventArgs e)
        {
            System.Drawing.Color newColor = getDialogColor(System.Drawing.Color.FromArgb(SB.BGColor.A, SB.BGColor.R, SB.BGColor.G, SB.BGColor.B));
            SB.BGColor = new Color(newColor.R, newColor.G, newColor.B, newColor.A);
        }

        private void addDefaultCamerasButton_Click(object sender, EventArgs e)
        {
            if (CameraManager.Instance.getNodes().getNodes().Count == 0)
            {
                CameraManager.Instance.loadXMLfake();
            }
        }

        private void superPizzaButton_Click(object sender, EventArgs e)
        {
            noUpdate++;
            MessageBox.Show(MyEditor.ActiveForm, "933510165\nCabrales for the win!", "SuperPizza");
            noUpdate--;
        }

        private void addCameraNode()
        {
            Vector3 pos = EditorHelper.Instance.getMousePosInZ(gameScreenPos, 0);

            NetworkNode<CameraData> newNode = new NetworkNode<CameraData>(new CameraData(pos, -1, false), pos + new Vector3(0, 0, 1000));

            CameraManager.Instance.addNode(newNode);

            if (CameraManager.Instance.getNodes().getNodes().Count == 2)
            {
                CameraManager.Instance.setupCamera();
            }
        }

        private void addCameraLink(NetworkNode<CameraData> src, NetworkNode<CameraData> dst)
        {
            src.addLinkedNode(dst);
        }


        private void linkNodeButton_Click(object sender, EventArgs e)
        {
            ((EditorState_MoveCameraNode)currentState).selectLink();
        }

        private void isFirstCheck_CheckedChanged(object sender, EventArgs e)
        {
            ((EditorState_MoveCameraNode)currentState).fieldsToCamera();
            myEditorControl.Focus();
        }

        private void keyPressed_cameraNode(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // PRESS ENTER
            {
                ((EditorState_MoveCameraNode)currentState).fieldsToCamera();
                myEditorControl.Focus();
            }
        }

        protected void textChange_cameraNode(object sender, EventArgs e)
        {
            ((EditorState_MoveCameraNode)currentState).fieldsToCamera();
            myEditorControl.Focus();
        }

        private void buttonSetupCamera_Click(object sender, EventArgs e)
        {
            CameraManager.Instance.setupCamera();
        }

        private void buttonSetCameraByTheFace_Click(object sender, EventArgs e)
        {
            ((EditorState_MoveCameraNode)currentState).setCameraByTheFace();
        }

        private void effectsCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            myEditorControl.Focus();
        }

        private void effect_TextChanged(object sender, EventArgs e)
        {

        }

        private void addConditionButton_Click(object sender, EventArgs e)
        {
            if (currentState is EditorState_EditTrigger)
            {
                ((EditorState_EditTrigger)currentState).addCondition(availableConditions.Text);
            }
        }

        private void addConsecuenceButton_Click(object sender, EventArgs e)
        {
            if (currentState is EditorState_EditTrigger)
            {
                ((EditorState_EditTrigger)currentState).addConsecuence(availableConsecuences.Text);
            }
        }

        private void removeConditionButton_Click(object sender, EventArgs e)
        {
            if (currentState is EditorState_EditTrigger)
            {
                ((EditorState_EditTrigger)currentState).removeCondition(triggerConditions.SelectedIndex);
            }
        }

        private void removeConsecuenceButton_Click(object sender, EventArgs e)
        {
            if (currentState is EditorState_EditTrigger)
            {
                ((EditorState_EditTrigger)currentState).removeConsecuence(triggerConsecuences.SelectedIndex);
            }
        }

        private void editEffectsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentState is EditorState_EditEffects)
            {
                ((EditorState_EditEffects)currentState).setSelected(editEffectsList.Text);
            }
        }

        private void saveEffectsButton_Click(object sender, EventArgs e)
        {
            ParticleManager.Instance.saveXML();
        }

        private void reloadEffectsButton_Click(object sender, EventArgs e)
        {
            ParticleManager.Instance.getParticles().Clear();
        }

        private void editEffects_Leave(object sender, EventArgs e)
        {
            if (currentState is EditorState_EditEffects)
            {
                ((EditorState_EditEffects)currentState).updateSelected();
            }
        }

        private void effectDuplicateButton_Click(object sender, EventArgs e)
        {
            if (currentState is EditorState_EditEffects)
            {
                noUpdate++;
                string name = Microsoft.VisualBasic.Interaction.InputBox("Enter a name for the new effect", "New Effect", "");
                ((EditorState_EditEffects)currentState).duplicate(name);
                noUpdate--;
            }
        }

        private void cameraModeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentState is EditorState_MoveCameraNode)
            {
                ((EditorState_MoveCameraNode)currentState).changeCameraMode(cameraModeCombo.SelectedIndex);
            }
        }

        private void myEditorControl_Click(object sender, EventArgs e)
        {
            dummyButton.Focus();
        }

        private void restartLevelButton_Click(object sender, EventArgs e)
        {
            //Reload level
            if (lastLoadedLevel != null)
            {
                EditorHelper.Instance.loadNewLevel(lastLoadedLevel);
                currentState = null;
            }
        }

        private void editEntityButton_Click(object sender, EventArgs e)
        {
            if (anyEntitySelected())
            {
                if (selectedEntities[0] is Enemy)
                {
                }
                else if (selectedEntities[0] is AnimatedEntity2D)
                {
                    string filename = System.IO.Path.GetFullPath("../../../../MyGameContent/textures/animatedProps/" + selectedEntities[0].entityName + ".png");
                    if (System.IO.File.Exists(filename))
                        System.Diagnostics.Process.Start(@filename);
                    else
                    {
                        filename = System.IO.Path.GetFullPath("../../../../MyGameContent/textures/animatedProps/" + selectedEntities[0].entityName + ".tga");
                        if (System.IO.File.Exists(filename))
                            System.Diagnostics.Process.Start(@filename);
                    }
                }
                else
                {
                    string filename = System.IO.Path.GetFullPath("../../../../MyGameContent/textures/staticProps/" + selectedEntities[0].entityName + ".png");
                    if (System.IO.File.Exists(filename))
                        System.Diagnostics.Process.Start(@filename);
                    else
                    {
                        filename = System.IO.Path.GetFullPath("../../../../MyGameContent/textures/staticProps/" + selectedEntities[0].entityName + ".tga");
                        if (System.IO.File.Exists(filename))
                            System.Diagnostics.Process.Start(@filename);
                    }
                }
            }
        }

        private void enemyCount_Leave(object sender, EventArgs e)
        {
            if (currentState is EditorState_EditEnemySpawnZone)
            {
                int aux;
                if(int.TryParse(enemyCount.Text, out aux))
                    ((EditorState_EditEnemySpawnZone)currentState).setCount(aux);
            }
        }

        private void enemyCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // PRESS ENTER
            {
                if (currentState is EditorState_EditEnemySpawnZone)
                {
                    int aux;
                    if (int.TryParse(enemyCount.Text, out aux))
                        ((EditorState_EditEnemySpawnZone)currentState).setCount(aux);
                }
            }
        }

        private void enemiesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentState is EditorState_EditEnemySpawnZone)
            {
                ((EditorState_EditEnemySpawnZone)currentState).setEnemy(enemiesCombo.Text);
            }
        }
    }
}
#endif