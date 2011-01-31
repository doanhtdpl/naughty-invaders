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
        #endregion

        private void myEditorControl_MouseButton(object sender, MouseEventArgs e)
        {

        }

        private void MyEditor_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void selectButton(object button)
        {
            buttonMove.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);
            buttonRotate.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);
            buttonScale.BackColor = System.Drawing.Color.FromArgb(212, 208, 200);

            ((Button)button).BackColor = System.Drawing.Color.Green;
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
    }
}
