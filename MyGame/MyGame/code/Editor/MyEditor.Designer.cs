namespace MyGame
{
    partial class MyEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DefaultPage = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Page2 = new System.Windows.Forms.TabPage();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textScaleY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textScaleX = new System.Windows.Forms.TextBox();
            this.textRotZ = new System.Windows.Forms.TextBox();
            this.textRotY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textRotX = new System.Windows.Forms.TextBox();
            this.textPosZ = new System.Windows.Forms.TextBox();
            this.textPosY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textPosX = new System.Windows.Forms.TextBox();
            this.myEditorControl = new MyGame.MyEditorControl();
            this.tabControl1.SuspendLayout();
            this.DefaultPage.SuspendLayout();
            this.propertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.DefaultPage);
            this.tabControl1.Controls.Add(this.Page2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1280, 92);
            this.tabControl1.TabIndex = 1;
            // 
            // DefaultPage
            // 
            this.DefaultPage.Controls.Add(this.button4);
            this.DefaultPage.Controls.Add(this.button3);
            this.DefaultPage.Controls.Add(this.button2);
            this.DefaultPage.Controls.Add(this.button1);
            this.DefaultPage.Location = new System.Drawing.Point(4, 29);
            this.DefaultPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DefaultPage.Name = "DefaultPage";
            this.DefaultPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DefaultPage.Size = new System.Drawing.Size(1272, 59);
            this.DefaultPage.TabIndex = 0;
            this.DefaultPage.Text = "Default";
            this.DefaultPage.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(174, 8);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 43);
            this.button4.TabIndex = 3;
            this.button4.Tag = "";
            this.button4.Text = "Scale";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(91, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 43);
            this.button3.TabIndex = 2;
            this.button3.Tag = "";
            this.button3.Text = "Rotate";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(8, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 43);
            this.button2.TabIndex = 1;
            this.button2.Tag = "";
            this.button2.Text = "Move";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(257, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Static Prop";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Page2
            // 
            this.Page2.Location = new System.Drawing.Point(4, 29);
            this.Page2.Name = "Page2";
            this.Page2.Size = new System.Drawing.Size(1272, 59);
            this.Page2.TabIndex = 1;
            this.Page2.Text = "Page2";
            this.Page2.UseVisualStyleBackColor = true;
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Controls.Add(this.label6);
            this.propertiesPanel.Controls.Add(this.label4);
            this.propertiesPanel.Controls.Add(this.label3);
            this.propertiesPanel.Controls.Add(this.textScaleY);
            this.propertiesPanel.Controls.Add(this.label5);
            this.propertiesPanel.Controls.Add(this.textScaleX);
            this.propertiesPanel.Controls.Add(this.textRotZ);
            this.propertiesPanel.Controls.Add(this.textRotY);
            this.propertiesPanel.Controls.Add(this.label2);
            this.propertiesPanel.Controls.Add(this.textRotX);
            this.propertiesPanel.Controls.Add(this.textPosZ);
            this.propertiesPanel.Controls.Add(this.textPosY);
            this.propertiesPanel.Controls.Add(this.label1);
            this.propertiesPanel.Controls.Add(this.textPosX);
            this.propertiesPanel.Location = new System.Drawing.Point(0, 96);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(407, 165);
            this.propertiesPanel.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(310, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Z";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "X";
            // 
            // textScaleY
            // 
            this.textScaleY.Location = new System.Drawing.Point(179, 100);
            this.textScaleY.Name = "textScaleY";
            this.textScaleY.Size = new System.Drawing.Size(91, 26);
            this.textScaleY.TabIndex = 18;
            this.textScaleY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "Scale";
            // 
            // textScaleX
            // 
            this.textScaleX.Location = new System.Drawing.Point(82, 100);
            this.textScaleX.Name = "textScaleX";
            this.textScaleX.Size = new System.Drawing.Size(91, 26);
            this.textScaleX.TabIndex = 16;
            this.textScaleX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
            // 
            // textRotZ
            // 
            this.textRotZ.Location = new System.Drawing.Point(276, 68);
            this.textRotZ.Name = "textRotZ";
            this.textRotZ.Size = new System.Drawing.Size(91, 26);
            this.textRotZ.TabIndex = 7;
            this.textRotZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
            // 
            // textRotY
            // 
            this.textRotY.Location = new System.Drawing.Point(179, 68);
            this.textRotY.Name = "textRotY";
            this.textRotY.Size = new System.Drawing.Size(91, 26);
            this.textRotY.TabIndex = 6;
            this.textRotY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rotation";
            // 
            // textRotX
            // 
            this.textRotX.Location = new System.Drawing.Point(82, 68);
            this.textRotX.Name = "textRotX";
            this.textRotX.Size = new System.Drawing.Size(91, 26);
            this.textRotX.TabIndex = 4;
            this.textRotX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
            // 
            // textPosZ
            // 
            this.textPosZ.Location = new System.Drawing.Point(276, 36);
            this.textPosZ.Name = "textPosZ";
            this.textPosZ.Size = new System.Drawing.Size(91, 26);
            this.textPosZ.TabIndex = 3;
            this.textPosZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
            // 
            // textPosY
            // 
            this.textPosY.Location = new System.Drawing.Point(179, 36);
            this.textPosY.Name = "textPosY";
            this.textPosY.Size = new System.Drawing.Size(91, 26);
            this.textPosY.TabIndex = 2;
            this.textPosY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Position";
            // 
            // textPosX
            // 
            this.textPosX.Location = new System.Drawing.Point(82, 36);
            this.textPosX.Name = "textPosX";
            this.textPosX.Size = new System.Drawing.Size(91, 26);
            this.textPosX.TabIndex = 0;
            this.textPosX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
            // 
            // myEditorControl
            // 
            this.myEditorControl.Location = new System.Drawing.Point(0, 96);
            this.myEditorControl.Name = "myEditorControl";
            this.myEditorControl.Size = new System.Drawing.Size(1280, 720);
            this.myEditorControl.TabIndex = 2;
            this.myEditorControl.Text = "myEditorControl";
            this.myEditorControl.Click += new System.EventHandler(this.myEditorControl_Click);
            // 
            // MyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1284, 775);
            this.Controls.Add(this.propertiesPanel);
            this.Controls.Add(this.myEditorControl);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MyEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyEditor";
            this.tabControl1.ResumeLayout(false);
            this.DefaultPage.ResumeLayout(false);
            this.propertiesPanel.ResumeLayout(false);
            this.propertiesPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage DefaultPage;
        private System.Windows.Forms.Button button1;
        private MyEditorControl myEditorControl;
        private System.Windows.Forms.TabPage Page2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel propertiesPanel;
        public System.Windows.Forms.TextBox textScaleY;
        public System.Windows.Forms.TextBox textScaleX;
        public System.Windows.Forms.TextBox textRotZ;
        public System.Windows.Forms.TextBox textRotY;
        public System.Windows.Forms.TextBox textRotX;
        public System.Windows.Forms.TextBox textPosZ;
        public System.Windows.Forms.TextBox textPosY;
        public System.Windows.Forms.TextBox textPosX;
    }
}