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

        private void myEditorControl_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (currentState != null)
            {
                currentState.leftClick(new Vector2(me.X, me.Y));
            }
        }

        public void update()
        {
            currentState.update();
        }

        public void render()
        {
            currentState.render();
        }

        protected void TextChange(object sender, EventArgs e)
        {
            currentState.propertiesChanged();
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            float value;
            if (e.KeyChar == '\r') // PRESS ENTER
            {
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
    }
}
