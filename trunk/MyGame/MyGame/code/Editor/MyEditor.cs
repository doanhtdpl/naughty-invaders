using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

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

        private void MyEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentState != null)
            {
                currentState.mouseMove(new Vector2(e.X, e.Y));
            }
        }

        private void myEditorControl_MouseButton(object sender, MouseEventArgs e)
        {
            if (currentState != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    currentState.leftClick(new Vector2(e.X, e.Y));
                }
                else if (e.Button == MouseButtons.Right)
                {
                    currentState.rightClick(new Vector2(e.X, e.Y));
                }
                else
                {
                    currentState.mouseReleased();
                }
            }
        }
        #endregion
    }
}
