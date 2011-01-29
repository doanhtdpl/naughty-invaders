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
            this.buttonAddAnimated = new System.Windows.Forms.Button();
            this.buttonScale = new System.Windows.Forms.Button();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.buttonMove = new System.Windows.Forms.Button();
            this.buttonAddStatic = new System.Windows.Forms.Button();
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
            this.tabControl1.Size = new System.Drawing.Size(853, 60);
            this.tabControl1.TabIndex = 1;
            // 
            // DefaultPage
            // 
            this.DefaultPage.Controls.Add(this.buttonAddAnimated);
            this.DefaultPage.Controls.Add(this.buttonScale);
            this.DefaultPage.Controls.Add(this.buttonRotate);
            this.DefaultPage.Controls.Add(this.buttonMove);
            this.DefaultPage.Controls.Add(this.buttonAddStatic);
            this.DefaultPage.Location = new System.Drawing.Point(4, 22);
            this.DefaultPage.Name = "DefaultPage";
            this.DefaultPage.Padding = new System.Windows.Forms.Padding(3);
            this.DefaultPage.Size = new System.Drawing.Size(845, 34);
            this.DefaultPage.TabIndex = 0;
            this.DefaultPage.Text = "Default";
            this.DefaultPage.UseVisualStyleBackColor = true;
            // 
            // buttonAddAnimated
            // 
            this.buttonAddAnimated.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddAnimated.Location = new System.Drawing.Point(226, 5);
            this.buttonAddAnimated.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddAnimated.Name = "buttonAddAnimated";
            this.buttonAddAnimated.Size = new System.Drawing.Size(51, 28);
            this.buttonAddAnimated.TabIndex = 4;
            this.buttonAddAnimated.Text = "Add Animated Prop";
            this.buttonAddAnimated.UseVisualStyleBackColor = true;
            this.buttonAddAnimated.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonScale
            // 
            this.buttonScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonScale.Location = new System.Drawing.Point(116, 5);
            this.buttonScale.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScale.Name = "buttonScale";
            this.buttonScale.Size = new System.Drawing.Size(51, 28);
            this.buttonScale.TabIndex = 3;
            this.buttonScale.Tag = "";
            this.buttonScale.Text = "Scale";
            this.buttonScale.UseVisualStyleBackColor = true;
            this.buttonScale.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonRotate
            // 
            this.buttonRotate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRotate.Location = new System.Drawing.Point(61, 5);
            this.buttonRotate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(51, 28);
            this.buttonRotate.TabIndex = 2;
            this.buttonRotate.Tag = "";
            this.buttonRotate.Text = "Rotate";
            this.buttonRotate.UseVisualStyleBackColor = true;
            this.buttonRotate.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonMove
            // 
            this.buttonMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMove.Location = new System.Drawing.Point(5, 5);
            this.buttonMove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(51, 28);
            this.buttonMove.TabIndex = 1;
            this.buttonMove.Tag = "";
            this.buttonMove.Text = "Move";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonAddStatic
            // 
            this.buttonAddStatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddStatic.Location = new System.Drawing.Point(171, 5);
            this.buttonAddStatic.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddStatic.Name = "buttonAddStatic";
            this.buttonAddStatic.Size = new System.Drawing.Size(51, 28);
            this.buttonAddStatic.TabIndex = 0;
            this.buttonAddStatic.Text = "Add Static Prop";
            this.buttonAddStatic.UseVisualStyleBackColor = true;
            this.buttonAddStatic.Click += new System.EventHandler(this.button_Click);
            // 
            // Page2
            // 
            this.Page2.Location = new System.Drawing.Point(4, 22);
            this.Page2.Margin = new System.Windows.Forms.Padding(2);
            this.Page2.Name = "Page2";
            this.Page2.Size = new System.Drawing.Size(845, 34);
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
            this.propertiesPanel.Location = new System.Drawing.Point(857, 62);
            this.propertiesPanel.Margin = new System.Windows.Forms.Padding(2);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(261, 107);
            this.propertiesPanel.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Z";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(145, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "X";
            // 
            // textScaleY
            // 
            this.textScaleY.Location = new System.Drawing.Point(119, 65);
            this.textScaleY.Margin = new System.Windows.Forms.Padding(2);
            this.textScaleY.Name = "textScaleY";
            this.textScaleY.Size = new System.Drawing.Size(62, 20);
            this.textScaleY.TabIndex = 18;
            this.textScaleY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.textScaleY.Leave += new System.EventHandler(this.textChange);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 67);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Scale";
            // 
            // textScaleX
            // 
            this.textScaleX.Location = new System.Drawing.Point(55, 65);
            this.textScaleX.Margin = new System.Windows.Forms.Padding(2);
            this.textScaleX.Name = "textScaleX";
            this.textScaleX.Size = new System.Drawing.Size(62, 20);
            this.textScaleX.TabIndex = 16;
            this.textScaleX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.textScaleX.Leave += new System.EventHandler(this.textChange);
            // 
            // textRotZ
            // 
            this.textRotZ.Location = new System.Drawing.Point(184, 44);
            this.textRotZ.Margin = new System.Windows.Forms.Padding(2);
            this.textRotZ.Name = "textRotZ";
            this.textRotZ.Size = new System.Drawing.Size(62, 20);
            this.textRotZ.TabIndex = 7;
            this.textRotZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.textRotZ.Leave += new System.EventHandler(this.textChange);
            // 
            // textRotY
            // 
            this.textRotY.Location = new System.Drawing.Point(119, 44);
            this.textRotY.Margin = new System.Windows.Forms.Padding(2);
            this.textRotY.Name = "textRotY";
            this.textRotY.Size = new System.Drawing.Size(62, 20);
            this.textRotY.TabIndex = 6;
            this.textRotY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.textRotY.Leave += new System.EventHandler(this.textChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rotation";
            // 
            // textRotX
            // 
            this.textRotX.Location = new System.Drawing.Point(55, 44);
            this.textRotX.Margin = new System.Windows.Forms.Padding(2);
            this.textRotX.Name = "textRotX";
            this.textRotX.Size = new System.Drawing.Size(62, 20);
            this.textRotX.TabIndex = 4;
            this.textRotX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.textRotX.Leave += new System.EventHandler(this.textChange);
            // 
            // textPosZ
            // 
            this.textPosZ.Location = new System.Drawing.Point(184, 23);
            this.textPosZ.Margin = new System.Windows.Forms.Padding(2);
            this.textPosZ.Name = "textPosZ";
            this.textPosZ.Size = new System.Drawing.Size(62, 20);
            this.textPosZ.TabIndex = 3;
            this.textPosZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.textPosZ.Leave += new System.EventHandler(this.textChange);
            // 
            // textPosY
            // 
            this.textPosY.Location = new System.Drawing.Point(119, 23);
            this.textPosY.Margin = new System.Windows.Forms.Padding(2);
            this.textPosY.Name = "textPosY";
            this.textPosY.Size = new System.Drawing.Size(62, 20);
            this.textPosY.TabIndex = 2;
            this.textPosY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.textPosY.Leave += new System.EventHandler(this.textChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Position";
            // 
            // textPosX
            // 
            this.textPosX.Location = new System.Drawing.Point(55, 23);
            this.textPosX.Margin = new System.Windows.Forms.Padding(2);
            this.textPosX.Name = "textPosX";
            this.textPosX.Size = new System.Drawing.Size(62, 20);
            this.textPosX.TabIndex = 0;
            this.textPosX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.textPosX.Leave += new System.EventHandler(this.textChange);
            // 
            // myEditorControl
            // 
            this.myEditorControl.Location = new System.Drawing.Point(0, 62);
            this.myEditorControl.Margin = new System.Windows.Forms.Padding(2);
            this.myEditorControl.Name = "myEditorControl";
            this.myEditorControl.Size = new System.Drawing.Size(853, 468);
            this.myEditorControl.TabIndex = 2;
            this.myEditorControl.Text = "myEditorControl";
            // 
            // MyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1120, 536);
            this.Controls.Add(this.propertiesPanel);
            this.Controls.Add(this.myEditorControl);
            this.Controls.Add(this.tabControl1);
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
        private System.Windows.Forms.Button buttonAddStatic;
        private System.Windows.Forms.TabPage Page2;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.Button buttonScale;
        private System.Windows.Forms.Button buttonRotate;
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
        private System.Windows.Forms.Button buttonAddAnimated;
        public MyEditorControl myEditorControl;
    }
}