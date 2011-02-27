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
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.buttonResetPosition = new System.Windows.Forms.Button();
            this.buttonResetScale = new System.Windows.Forms.Button();
            this.buttonResetRotation = new System.Windows.Forms.Button();
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
            this.DefaultPage = new System.Windows.Forms.TabPage();
            this.buttonImportLevel = new System.Windows.Forms.Button();
            this.canSelectEnemy = new System.Windows.Forms.CheckBox();
            this.canSelectAnimated = new System.Windows.Forms.CheckBox();
            this.canSelectStatic = new System.Windows.Forms.CheckBox();
            this.buttonAddEnemy = new System.Windows.Forms.Button();
            this.buttonSaveLevel = new System.Windows.Forms.Button();
            this.buttonLoadLevel = new System.Windows.Forms.Button();
            this.buttonAddAnimated = new System.Windows.Forms.Button();
            this.buttonScale = new System.Windows.Forms.Button();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.buttonMove = new System.Windows.Forms.Button();
            this.buttonAddStatic = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.myEditorControl = new MyGame.MyEditorControl();
            this.selectGroup = new System.Windows.Forms.CheckBox();
            this.propertiesPanel.SuspendLayout();
            this.DefaultPage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Controls.Add(this.buttonResetPosition);
            this.propertiesPanel.Controls.Add(this.buttonResetScale);
            this.propertiesPanel.Controls.Add(this.buttonResetRotation);
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
            this.propertiesPanel.Location = new System.Drawing.Point(855, 0);
            this.propertiesPanel.Margin = new System.Windows.Forms.Padding(2);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(310, 91);
            this.propertiesPanel.TabIndex = 3;
            // 
            // buttonResetPosition
            // 
            this.buttonResetPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonResetPosition.Location = new System.Drawing.Point(250, 23);
            this.buttonResetPosition.Margin = new System.Windows.Forms.Padding(2);
            this.buttonResetPosition.Name = "buttonResetPosition";
            this.buttonResetPosition.Size = new System.Drawing.Size(51, 20);
            this.buttonResetPosition.TabIndex = 24;
            this.buttonResetPosition.Text = "Reset";
            this.buttonResetPosition.UseVisualStyleBackColor = true;
            this.buttonResetPosition.Click += new System.EventHandler(this.buttonResetPosition_Click);
            // 
            // buttonResetScale
            // 
            this.buttonResetScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonResetScale.Location = new System.Drawing.Point(250, 65);
            this.buttonResetScale.Margin = new System.Windows.Forms.Padding(2);
            this.buttonResetScale.Name = "buttonResetScale";
            this.buttonResetScale.Size = new System.Drawing.Size(51, 20);
            this.buttonResetScale.TabIndex = 23;
            this.buttonResetScale.Text = "Reset";
            this.buttonResetScale.UseVisualStyleBackColor = true;
            this.buttonResetScale.Click += new System.EventHandler(this.buttonResetScale_Click);
            // 
            // buttonResetRotation
            // 
            this.buttonResetRotation.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonResetRotation.Location = new System.Drawing.Point(250, 44);
            this.buttonResetRotation.Margin = new System.Windows.Forms.Padding(2);
            this.buttonResetRotation.Name = "buttonResetRotation";
            this.buttonResetRotation.Size = new System.Drawing.Size(51, 20);
            this.buttonResetRotation.TabIndex = 22;
            this.buttonResetRotation.Text = "Reset";
            this.buttonResetRotation.UseVisualStyleBackColor = true;
            this.buttonResetRotation.Click += new System.EventHandler(this.buttonResetRotation_Click);
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
            // DefaultPage
            // 
            this.DefaultPage.Controls.Add(this.selectGroup);
            this.DefaultPage.Controls.Add(this.buttonImportLevel);
            this.DefaultPage.Controls.Add(this.canSelectEnemy);
            this.DefaultPage.Controls.Add(this.canSelectAnimated);
            this.DefaultPage.Controls.Add(this.canSelectStatic);
            this.DefaultPage.Controls.Add(this.buttonAddEnemy);
            this.DefaultPage.Controls.Add(this.buttonSaveLevel);
            this.DefaultPage.Controls.Add(this.buttonLoadLevel);
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
            // buttonImportLevel
            // 
            this.buttonImportLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImportLevel.Location = new System.Drawing.Point(390, 6);
            this.buttonImportLevel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonImportLevel.Name = "buttonImportLevel";
            this.buttonImportLevel.Size = new System.Drawing.Size(51, 28);
            this.buttonImportLevel.TabIndex = 11;
            this.buttonImportLevel.Text = "Import Level";
            this.buttonImportLevel.UseVisualStyleBackColor = true;
            this.buttonImportLevel.Click += new System.EventHandler(this.buttonImportLevel_Click);
            // 
            // canSelectEnemy
            // 
            this.canSelectEnemy.AutoSize = true;
            this.canSelectEnemy.Checked = true;
            this.canSelectEnemy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.canSelectEnemy.Location = new System.Drawing.Point(647, 10);
            this.canSelectEnemy.Name = "canSelectEnemy";
            this.canSelectEnemy.Size = new System.Drawing.Size(58, 17);
            this.canSelectEnemy.TabIndex = 10;
            this.canSelectEnemy.Text = "Enemy";
            this.canSelectEnemy.UseVisualStyleBackColor = true;
            // 
            // canSelectAnimated
            // 
            this.canSelectAnimated.AutoSize = true;
            this.canSelectAnimated.Checked = true;
            this.canSelectAnimated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.canSelectAnimated.Location = new System.Drawing.Point(574, 10);
            this.canSelectAnimated.Name = "canSelectAnimated";
            this.canSelectAnimated.Size = new System.Drawing.Size(70, 17);
            this.canSelectAnimated.TabIndex = 9;
            this.canSelectAnimated.Text = "Animated";
            this.canSelectAnimated.UseVisualStyleBackColor = true;
            // 
            // canSelectStatic
            // 
            this.canSelectStatic.AutoSize = true;
            this.canSelectStatic.Checked = true;
            this.canSelectStatic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.canSelectStatic.Location = new System.Drawing.Point(515, 10);
            this.canSelectStatic.Name = "canSelectStatic";
            this.canSelectStatic.Size = new System.Drawing.Size(53, 17);
            this.canSelectStatic.TabIndex = 8;
            this.canSelectStatic.Text = "Static";
            this.canSelectStatic.UseVisualStyleBackColor = true;
            // 
            // buttonAddEnemy
            // 
            this.buttonAddEnemy.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddEnemy.Location = new System.Drawing.Point(281, 5);
            this.buttonAddEnemy.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddEnemy.Name = "buttonAddEnemy";
            this.buttonAddEnemy.Size = new System.Drawing.Size(51, 28);
            this.buttonAddEnemy.TabIndex = 7;
            this.buttonAddEnemy.Text = "Add Enemy";
            this.buttonAddEnemy.UseVisualStyleBackColor = true;
            this.buttonAddEnemy.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonSaveLevel
            // 
            this.buttonSaveLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveLevel.Location = new System.Drawing.Point(445, 6);
            this.buttonSaveLevel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSaveLevel.Name = "buttonSaveLevel";
            this.buttonSaveLevel.Size = new System.Drawing.Size(51, 28);
            this.buttonSaveLevel.TabIndex = 6;
            this.buttonSaveLevel.Text = "Save Level";
            this.buttonSaveLevel.UseVisualStyleBackColor = true;
            this.buttonSaveLevel.Click += new System.EventHandler(this.buttonSaveLevel_Click);
            // 
            // buttonLoadLevel
            // 
            this.buttonLoadLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadLevel.Location = new System.Drawing.Point(336, 5);
            this.buttonLoadLevel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLoadLevel.Name = "buttonLoadLevel";
            this.buttonLoadLevel.Size = new System.Drawing.Size(51, 28);
            this.buttonLoadLevel.TabIndex = 5;
            this.buttonLoadLevel.Text = "Load Level";
            this.buttonLoadLevel.UseVisualStyleBackColor = true;
            this.buttonLoadLevel.Click += new System.EventHandler(this.buttonLoadLevel_Click);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.DefaultPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(853, 60);
            this.tabControl1.TabIndex = 1;
            // 
            // myEditorControl
            // 
            this.myEditorControl.Location = new System.Drawing.Point(0, 92);
            this.myEditorControl.Margin = new System.Windows.Forms.Padding(2);
            this.myEditorControl.Name = "myEditorControl";
            this.myEditorControl.Size = new System.Drawing.Size(1280, 720);
            this.myEditorControl.TabIndex = 2;
            this.myEditorControl.Text = "myEditorControl";
            // 
            // selectGroup
            // 
            this.selectGroup.AutoSize = true;
            this.selectGroup.Location = new System.Drawing.Point(711, 10);
            this.selectGroup.Name = "selectGroup";
            this.selectGroup.Size = new System.Drawing.Size(53, 17);
            this.selectGroup.TabIndex = 12;
            this.selectGroup.Text = "group";
            this.selectGroup.UseVisualStyleBackColor = true;
            // 
            // MyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1284, 873);
            this.Controls.Add(this.propertiesPanel);
            this.Controls.Add(this.myEditorControl);
            this.Controls.Add(this.tabControl1);
            this.Name = "MyEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyEditor";
            this.propertiesPanel.ResumeLayout(false);
            this.propertiesPanel.PerformLayout();
            this.DefaultPage.ResumeLayout(false);
            this.DefaultPage.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

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
        public MyEditorControl myEditorControl;
        private System.Windows.Forms.Button buttonResetPosition;
        private System.Windows.Forms.Button buttonResetScale;
        private System.Windows.Forms.Button buttonResetRotation;
        private System.Windows.Forms.TabPage DefaultPage;
        private System.Windows.Forms.Button buttonAddEnemy;
        private System.Windows.Forms.Button buttonSaveLevel;
        private System.Windows.Forms.Button buttonLoadLevel;
        private System.Windows.Forms.Button buttonAddAnimated;
        private System.Windows.Forms.Button buttonScale;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.Button buttonAddStatic;
        private System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.CheckBox canSelectEnemy;
        public System.Windows.Forms.CheckBox canSelectAnimated;
        public System.Windows.Forms.CheckBox canSelectStatic;
        private System.Windows.Forms.Button buttonImportLevel;
        public System.Windows.Forms.CheckBox selectGroup;
    }
}