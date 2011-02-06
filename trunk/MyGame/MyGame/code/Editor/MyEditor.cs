using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace MyGame
{
    public partial class MyEditor : Form
    {
        EditorState currentState;
        public static MyEditor Instance;

        public MyEditor()
        {
            InitializeComponent();
            currentState = new DefaultState();

            Instance = this;
        }

        #region functions
        public void update()
        {
            currentState.update();
        }

        public void render()
        {
            currentState.render();
        }
        #endregion

        private void validateInputs(object sender)
        {
            if (currentState != null)
            {
                float value;
                if (float.TryParse(((TextBox)sender).Text, out value))
                {
                    currentState.propertiesChanged();
                }
                else
                {
                    currentState.updateEntityProperties();
                }
            }
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
            if (currentState != null)
            {
                if (sender == buttonMove)
                {
                    ((DefaultState)currentState).changeState(DefaultState.DefaultStates.MOVE);
                }
                else if (sender == buttonRotate)
                {
                    ((DefaultState)currentState).changeState(DefaultState.DefaultStates.ROTATE);
                }
                else if (sender == buttonScale)
                {
                    ((DefaultState)currentState).changeState(DefaultState.DefaultStates.SCALE);
                }
                else if (sender == buttonAddStatic)
                {
                    ((DefaultState)currentState).changeState(DefaultState.DefaultStates.ADD_STATIC);
                }
                else if (sender == buttonAddAnimated)
                {
                    ((DefaultState)currentState).changeState(DefaultState.DefaultStates.ADD_ANIMATED);
                }
                selectButton(sender);
            }
        }
        #endregion

        private void selectButton(object button)
        {
            buttonMove.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);
            buttonRotate.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);
            buttonScale.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);

            ((Button)button).BackColor = System.Drawing.Color.Green;
        }

        private void buttonLoadLevel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            string relativePath = "../../../../MyGameContent/xml/levels";
            string fullPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = fullPath;
            fileDialog.Title = "Save Level";
            fileDialog.Filter = "Level files (*.xml)|*.xml";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                EditorHelper.Instance.loadLevelFromXML(fileDialog.FileName);
                ((DefaultState)currentState).changeState(DefaultState.DefaultStates.NONE);
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

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                EditorHelper.Instance.saveLevelToXML(fileDialog.FileName);
                ((DefaultState)currentState).changeState(DefaultState.DefaultStates.NONE);
            }
        }

        private void buttonResetPosition_Click(object sender, EventArgs e)
        {
            ((DefaultState)currentState).resetPosition();
        }

        private void buttonResetRotation_Click(object sender, EventArgs e)
        {
            ((DefaultState)currentState).resetRotation();
        }

        private void buttonResetScale_Click(object sender, EventArgs e)
        {
            ((DefaultState)currentState).resetScale();
        }
    }
}
