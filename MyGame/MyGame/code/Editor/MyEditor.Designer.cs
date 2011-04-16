#if EDITOR
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
            this.staticPropertiesPanel = new System.Windows.Forms.Panel();
            this.flipVerticalCheck = new System.Windows.Forms.CheckBox();
            this.flipHorizontalCheck = new System.Windows.Forms.CheckBox();
            this.colorButton = new System.Windows.Forms.Button();
            this.colorB = new System.Windows.Forms.TextBox();
            this.colorG = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.colorA = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.colorR = new System.Windows.Forms.TextBox();
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
            this.label2 = new System.Windows.Forms.Label();
            this.textPosZ = new System.Windows.Forms.TextBox();
            this.textPosY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textPosX = new System.Windows.Forms.TextBox();
            this.canSelectPlayer = new System.Windows.Forms.CheckBox();
            this.selectGroup = new System.Windows.Forms.CheckBox();
            this.canSelectEnemy = new System.Windows.Forms.CheckBox();
            this.canSelectAnimated = new System.Windows.Forms.CheckBox();
            this.canSelectStatic = new System.Windows.Forms.CheckBox();
            this.buttonImportLevel = new System.Windows.Forms.Button();
            this.buttonSaveLevel = new System.Windows.Forms.Button();
            this.buttonLoadLevel = new System.Windows.Forms.Button();
            this.buttonAddAnimated = new System.Windows.Forms.Button();
            this.buttonAddStatic = new System.Windows.Forms.Button();
            this.buttonCreateGroup = new System.Windows.Forms.Button();
            this.buttonAddEnemy = new System.Windows.Forms.Button();
            this.buttonScale = new System.Windows.Forms.Button();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.buttonMove = new System.Windows.Forms.Button();
            this.texturesCombo = new System.Windows.Forms.ComboBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.BGColorButton = new System.Windows.Forms.Button();
            this.addDefaultCamerasButton = new System.Windows.Forms.Button();
            this.superPizzaButton = new System.Windows.Forms.Button();
            this.buttonNewLevel = new System.Windows.Forms.Button();
            this.addCameraNodeButton = new System.Windows.Forms.Button();
            this.buttonMoveCameraNode = new System.Windows.Forms.Button();
            this.cameraNodePanel = new System.Windows.Forms.Panel();
            this.buttonSetupCamera = new System.Windows.Forms.Button();
            this.linkNodeButton = new System.Windows.Forms.Button();
            this.isFirstCheck = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cameraNodeSpeed = new System.Windows.Forms.TextBox();
            this.cameraPosZ = new System.Windows.Forms.TextBox();
            this.cameraPosY = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cameraPosX = new System.Windows.Forms.TextBox();
            this.myEditorControl = new MyGame.MyEditorControl();
            this.button1 = new System.Windows.Forms.Button();
            this.staticPropertiesPanel.SuspendLayout();
            this.cameraNodePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // staticPropertiesPanel
            // 
            this.staticPropertiesPanel.Controls.Add(this.flipVerticalCheck);
            this.staticPropertiesPanel.Controls.Add(this.flipHorizontalCheck);
            this.staticPropertiesPanel.Controls.Add(this.colorButton);
            this.staticPropertiesPanel.Controls.Add(this.colorB);
            this.staticPropertiesPanel.Controls.Add(this.colorG);
            this.staticPropertiesPanel.Controls.Add(this.label8);
            this.staticPropertiesPanel.Controls.Add(this.colorA);
            this.staticPropertiesPanel.Controls.Add(this.label7);
            this.staticPropertiesPanel.Controls.Add(this.colorR);
            this.staticPropertiesPanel.Controls.Add(this.buttonResetPosition);
            this.staticPropertiesPanel.Controls.Add(this.buttonResetScale);
            this.staticPropertiesPanel.Controls.Add(this.buttonResetRotation);
            this.staticPropertiesPanel.Controls.Add(this.label6);
            this.staticPropertiesPanel.Controls.Add(this.label4);
            this.staticPropertiesPanel.Controls.Add(this.label3);
            this.staticPropertiesPanel.Controls.Add(this.textScaleY);
            this.staticPropertiesPanel.Controls.Add(this.label5);
            this.staticPropertiesPanel.Controls.Add(this.textScaleX);
            this.staticPropertiesPanel.Controls.Add(this.textRotZ);
            this.staticPropertiesPanel.Controls.Add(this.label2);
            this.staticPropertiesPanel.Controls.Add(this.textPosZ);
            this.staticPropertiesPanel.Controls.Add(this.textPosY);
            this.staticPropertiesPanel.Controls.Add(this.label1);
            this.staticPropertiesPanel.Controls.Add(this.textPosX);
            this.staticPropertiesPanel.Location = new System.Drawing.Point(724, 0);
            this.staticPropertiesPanel.Margin = new System.Windows.Forms.Padding(2);
            this.staticPropertiesPanel.Name = "staticPropertiesPanel";
            this.staticPropertiesPanel.Size = new System.Drawing.Size(492, 91);
            this.staticPropertiesPanel.TabIndex = 3;
            // 
            // flipVerticalCheck
            // 
            this.flipVerticalCheck.AutoSize = true;
            this.flipVerticalCheck.Location = new System.Drawing.Point(404, 70);
            this.flipVerticalCheck.Name = "flipVerticalCheck";
            this.flipVerticalCheck.Size = new System.Drawing.Size(46, 17);
            this.flipVerticalCheck.TabIndex = 33;
            this.flipVerticalCheck.Text = "flipV";
            this.flipVerticalCheck.UseVisualStyleBackColor = true;
            this.flipVerticalCheck.CheckedChanged += new System.EventHandler(this.flip_CheckedChanged);
            // 
            // flipHorizontalCheck
            // 
            this.flipHorizontalCheck.AutoSize = true;
            this.flipHorizontalCheck.Location = new System.Drawing.Point(340, 70);
            this.flipHorizontalCheck.Name = "flipHorizontalCheck";
            this.flipHorizontalCheck.Size = new System.Drawing.Size(47, 17);
            this.flipHorizontalCheck.TabIndex = 32;
            this.flipHorizontalCheck.Text = "flipH";
            this.flipHorizontalCheck.UseVisualStyleBackColor = true;
            this.flipHorizontalCheck.CheckedChanged += new System.EventHandler(this.flip_CheckedChanged);
            // 
            // colorButton
            // 
            this.colorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton.Location = new System.Drawing.Point(438, 22);
            this.colorButton.Margin = new System.Windows.Forms.Padding(2);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(40, 20);
            this.colorButton.TabIndex = 31;
            this.colorButton.Text = "Color";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // colorB
            // 
            this.colorB.Location = new System.Drawing.Point(404, 22);
            this.colorB.Margin = new System.Windows.Forms.Padding(2);
            this.colorB.Name = "colorB";
            this.colorB.Size = new System.Drawing.Size(30, 20);
            this.colorB.TabIndex = 30;
            this.colorB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.colorB.Leave += new System.EventHandler(this.textChange);
            // 
            // colorG
            // 
            this.colorG.Location = new System.Drawing.Point(372, 22);
            this.colorG.Margin = new System.Windows.Forms.Padding(2);
            this.colorG.Name = "colorG";
            this.colorG.Size = new System.Drawing.Size(30, 20);
            this.colorG.TabIndex = 29;
            this.colorG.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.colorG.Leave += new System.EventHandler(this.textChange);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(305, 46);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Alpha";
            // 
            // colorA
            // 
            this.colorA.Location = new System.Drawing.Point(340, 46);
            this.colorA.Margin = new System.Windows.Forms.Padding(2);
            this.colorA.Name = "colorA";
            this.colorA.Size = new System.Drawing.Size(30, 20);
            this.colorA.TabIndex = 27;
            this.colorA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.colorA.Leave += new System.EventHandler(this.textChange);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(305, 26);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Color";
            // 
            // colorR
            // 
            this.colorR.Location = new System.Drawing.Point(340, 22);
            this.colorR.Margin = new System.Windows.Forms.Padding(2);
            this.colorR.Name = "colorR";
            this.colorR.Size = new System.Drawing.Size(30, 20);
            this.colorR.TabIndex = 25;
            this.colorR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.colorR.Leave += new System.EventHandler(this.textChange);
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
            this.textRotZ.Location = new System.Drawing.Point(55, 44);
            this.textRotZ.Margin = new System.Windows.Forms.Padding(2);
            this.textRotZ.Name = "textRotZ";
            this.textRotZ.Size = new System.Drawing.Size(62, 20);
            this.textRotZ.TabIndex = 7;
            this.textRotZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.textRotZ.Leave += new System.EventHandler(this.textChange);
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
            // canSelectPlayer
            // 
            this.canSelectPlayer.AutoSize = true;
            this.canSelectPlayer.Location = new System.Drawing.Point(507, 70);
            this.canSelectPlayer.Name = "canSelectPlayer";
            this.canSelectPlayer.Size = new System.Drawing.Size(54, 17);
            this.canSelectPlayer.TabIndex = 14;
            this.canSelectPlayer.Text = "player";
            this.canSelectPlayer.UseVisualStyleBackColor = true;
            // 
            // selectGroup
            // 
            this.selectGroup.AutoSize = true;
            this.selectGroup.Location = new System.Drawing.Point(448, 70);
            this.selectGroup.Name = "selectGroup";
            this.selectGroup.Size = new System.Drawing.Size(53, 17);
            this.selectGroup.TabIndex = 12;
            this.selectGroup.Text = "group";
            this.selectGroup.UseVisualStyleBackColor = true;
            // 
            // canSelectEnemy
            // 
            this.canSelectEnemy.AutoSize = true;
            this.canSelectEnemy.Checked = true;
            this.canSelectEnemy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.canSelectEnemy.Location = new System.Drawing.Point(136, 74);
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
            this.canSelectAnimated.Location = new System.Drawing.Point(63, 74);
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
            this.canSelectStatic.Location = new System.Drawing.Point(5, 74);
            this.canSelectStatic.Name = "canSelectStatic";
            this.canSelectStatic.Size = new System.Drawing.Size(53, 17);
            this.canSelectStatic.TabIndex = 8;
            this.canSelectStatic.Text = "Static";
            this.canSelectStatic.UseVisualStyleBackColor = true;
            // 
            // buttonImportLevel
            // 
            this.buttonImportLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImportLevel.Location = new System.Drawing.Point(247, 2);
            this.buttonImportLevel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonImportLevel.Name = "buttonImportLevel";
            this.buttonImportLevel.Size = new System.Drawing.Size(51, 28);
            this.buttonImportLevel.TabIndex = 29;
            this.buttonImportLevel.Text = "Import Level";
            this.buttonImportLevel.UseVisualStyleBackColor = true;
            this.buttonImportLevel.Click += new System.EventHandler(this.buttonImportLevel_Click);
            // 
            // buttonSaveLevel
            // 
            this.buttonSaveLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveLevel.Location = new System.Drawing.Point(302, 2);
            this.buttonSaveLevel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSaveLevel.Name = "buttonSaveLevel";
            this.buttonSaveLevel.Size = new System.Drawing.Size(51, 28);
            this.buttonSaveLevel.TabIndex = 28;
            this.buttonSaveLevel.Text = "Save Level";
            this.buttonSaveLevel.UseVisualStyleBackColor = true;
            this.buttonSaveLevel.Click += new System.EventHandler(this.buttonSaveLevel_Click);
            // 
            // buttonLoadLevel
            // 
            this.buttonLoadLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadLevel.Location = new System.Drawing.Point(192, 2);
            this.buttonLoadLevel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLoadLevel.Name = "buttonLoadLevel";
            this.buttonLoadLevel.Size = new System.Drawing.Size(51, 28);
            this.buttonLoadLevel.TabIndex = 27;
            this.buttonLoadLevel.Text = "Load Level";
            this.buttonLoadLevel.UseVisualStyleBackColor = true;
            this.buttonLoadLevel.Click += new System.EventHandler(this.buttonLoadLevel_Click);
            // 
            // buttonAddAnimated
            // 
            this.buttonAddAnimated.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddAnimated.Location = new System.Drawing.Point(72, 36);
            this.buttonAddAnimated.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddAnimated.Name = "buttonAddAnimated";
            this.buttonAddAnimated.Size = new System.Drawing.Size(51, 28);
            this.buttonAddAnimated.TabIndex = 26;
            this.buttonAddAnimated.Text = "Add Animated Prop";
            this.buttonAddAnimated.UseVisualStyleBackColor = true;
            this.buttonAddAnimated.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonAddStatic
            // 
            this.buttonAddStatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddStatic.Location = new System.Drawing.Point(7, 36);
            this.buttonAddStatic.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddStatic.Name = "buttonAddStatic";
            this.buttonAddStatic.Size = new System.Drawing.Size(51, 28);
            this.buttonAddStatic.TabIndex = 25;
            this.buttonAddStatic.Text = "Add Static Prop";
            this.buttonAddStatic.UseVisualStyleBackColor = true;
            this.buttonAddStatic.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonCreateGroup
            // 
            this.buttonCreateGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateGroup.Location = new System.Drawing.Point(450, 36);
            this.buttonCreateGroup.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCreateGroup.Name = "buttonCreateGroup";
            this.buttonCreateGroup.Size = new System.Drawing.Size(51, 28);
            this.buttonCreateGroup.TabIndex = 24;
            this.buttonCreateGroup.Text = "Create Group";
            this.buttonCreateGroup.UseVisualStyleBackColor = true;
            this.buttonCreateGroup.Click += new System.EventHandler(this.buttonCreateGroup_Click);
            // 
            // buttonAddEnemy
            // 
            this.buttonAddEnemy.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddEnemy.Location = new System.Drawing.Point(136, 36);
            this.buttonAddEnemy.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddEnemy.Name = "buttonAddEnemy";
            this.buttonAddEnemy.Size = new System.Drawing.Size(51, 28);
            this.buttonAddEnemy.TabIndex = 23;
            this.buttonAddEnemy.Text = "Add Enemy";
            this.buttonAddEnemy.UseVisualStyleBackColor = true;
            this.buttonAddEnemy.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonScale
            // 
            this.buttonScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonScale.Location = new System.Drawing.Point(136, 2);
            this.buttonScale.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScale.Name = "buttonScale";
            this.buttonScale.Size = new System.Drawing.Size(51, 28);
            this.buttonScale.TabIndex = 22;
            this.buttonScale.Tag = "";
            this.buttonScale.Text = "Scale";
            this.buttonScale.UseVisualStyleBackColor = true;
            this.buttonScale.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonRotate
            // 
            this.buttonRotate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRotate.Location = new System.Drawing.Point(72, 2);
            this.buttonRotate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(51, 28);
            this.buttonRotate.TabIndex = 21;
            this.buttonRotate.Tag = "";
            this.buttonRotate.Text = "Rotate";
            this.buttonRotate.UseVisualStyleBackColor = true;
            this.buttonRotate.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonMove
            // 
            this.buttonMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMove.Location = new System.Drawing.Point(7, 2);
            this.buttonMove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(51, 28);
            this.buttonMove.TabIndex = 20;
            this.buttonMove.Tag = "";
            this.buttonMove.Text = "Move";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.button_Click);
            // 
            // texturesCombo
            // 
            this.texturesCombo.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.texturesCombo.CausesValidation = false;
            this.texturesCombo.FormattingEnabled = true;
            this.texturesCombo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.texturesCombo.Location = new System.Drawing.Point(192, 39);
            this.texturesCombo.Name = "texturesCombo";
            this.texturesCombo.Size = new System.Drawing.Size(183, 21);
            this.texturesCombo.TabIndex = 200;
            this.texturesCombo.TabStop = false;
            this.texturesCombo.SelectedIndexChanged += new System.EventHandler(this.texturesCombo_SelectedIndexChanged);
            // 
            // BGColorButton
            // 
            this.BGColorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BGColorButton.Location = new System.Drawing.Point(450, 2);
            this.BGColorButton.Margin = new System.Windows.Forms.Padding(2);
            this.BGColorButton.Name = "BGColorButton";
            this.BGColorButton.Size = new System.Drawing.Size(51, 28);
            this.BGColorButton.TabIndex = 30;
            this.BGColorButton.Text = "BG Color";
            this.BGColorButton.UseVisualStyleBackColor = true;
            this.BGColorButton.Click += new System.EventHandler(this.BGColorButton_Click);
            // 
            // addDefaultCamerasButton
            // 
            this.addDefaultCamerasButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addDefaultCamerasButton.Location = new System.Drawing.Point(514, 2);
            this.addDefaultCamerasButton.Margin = new System.Windows.Forms.Padding(2);
            this.addDefaultCamerasButton.Name = "addDefaultCamerasButton";
            this.addDefaultCamerasButton.Size = new System.Drawing.Size(60, 28);
            this.addDefaultCamerasButton.TabIndex = 31;
            this.addDefaultCamerasButton.Text = "Add Default Cameras";
            this.addDefaultCamerasButton.UseVisualStyleBackColor = true;
            this.addDefaultCamerasButton.Click += new System.EventHandler(this.addDefaultCamerasButton_Click);
            // 
            // superPizzaButton
            // 
            this.superPizzaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superPizzaButton.Location = new System.Drawing.Point(1220, 2);
            this.superPizzaButton.Margin = new System.Windows.Forms.Padding(2);
            this.superPizzaButton.Name = "superPizzaButton";
            this.superPizzaButton.Size = new System.Drawing.Size(60, 28);
            this.superPizzaButton.TabIndex = 201;
            this.superPizzaButton.Text = "SuperPizza";
            this.superPizzaButton.UseVisualStyleBackColor = true;
            this.superPizzaButton.Click += new System.EventHandler(this.superPizzaButton_Click);
            // 
            // buttonNewLevel
            // 
            this.buttonNewLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewLevel.Location = new System.Drawing.Point(357, 2);
            this.buttonNewLevel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewLevel.Name = "buttonNewLevel";
            this.buttonNewLevel.Size = new System.Drawing.Size(51, 28);
            this.buttonNewLevel.TabIndex = 202;
            this.buttonNewLevel.Text = "New Level";
            this.buttonNewLevel.UseVisualStyleBackColor = true;
            this.buttonNewLevel.Click += new System.EventHandler(this.buttonNewLevel_Click);
            // 
            // addCameraNodeButton
            // 
            this.addCameraNodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCameraNodeButton.Location = new System.Drawing.Point(587, 2);
            this.addCameraNodeButton.Margin = new System.Windows.Forms.Padding(2);
            this.addCameraNodeButton.Name = "addCameraNodeButton";
            this.addCameraNodeButton.Size = new System.Drawing.Size(60, 28);
            this.addCameraNodeButton.TabIndex = 204;
            this.addCameraNodeButton.Text = "Add Camera Node";
            this.addCameraNodeButton.UseVisualStyleBackColor = true;
            this.addCameraNodeButton.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonMoveCameraNode
            // 
            this.buttonMoveCameraNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMoveCameraNode.Location = new System.Drawing.Point(660, 2);
            this.buttonMoveCameraNode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMoveCameraNode.Name = "buttonMoveCameraNode";
            this.buttonMoveCameraNode.Size = new System.Drawing.Size(60, 28);
            this.buttonMoveCameraNode.TabIndex = 205;
            this.buttonMoveCameraNode.Text = "Edit Camera Node";
            this.buttonMoveCameraNode.UseVisualStyleBackColor = true;
            this.buttonMoveCameraNode.Click += new System.EventHandler(this.button_Click);
            // 
            // cameraNodePanel
            // 
            this.cameraNodePanel.Controls.Add(this.button1);
            this.cameraNodePanel.Controls.Add(this.buttonSetupCamera);
            this.cameraNodePanel.Controls.Add(this.linkNodeButton);
            this.cameraNodePanel.Controls.Add(this.isFirstCheck);
            this.cameraNodePanel.Controls.Add(this.label11);
            this.cameraNodePanel.Controls.Add(this.label12);
            this.cameraNodePanel.Controls.Add(this.label13);
            this.cameraNodePanel.Controls.Add(this.label14);
            this.cameraNodePanel.Controls.Add(this.cameraNodeSpeed);
            this.cameraNodePanel.Controls.Add(this.cameraPosZ);
            this.cameraNodePanel.Controls.Add(this.cameraPosY);
            this.cameraNodePanel.Controls.Add(this.label16);
            this.cameraNodePanel.Controls.Add(this.cameraPosX);
            this.cameraNodePanel.Location = new System.Drawing.Point(724, 95);
            this.cameraNodePanel.Margin = new System.Windows.Forms.Padding(2);
            this.cameraNodePanel.Name = "cameraNodePanel";
            this.cameraNodePanel.Size = new System.Drawing.Size(336, 91);
            this.cameraNodePanel.TabIndex = 206;
            // 
            // buttonSetupCamera
            // 
            this.buttonSetupCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetupCamera.Location = new System.Drawing.Point(250, 46);
            this.buttonSetupCamera.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSetupCamera.Name = "buttonSetupCamera";
            this.buttonSetupCamera.Size = new System.Drawing.Size(60, 28);
            this.buttonSetupCamera.TabIndex = 207;
            this.buttonSetupCamera.Text = "Setup Camera";
            this.buttonSetupCamera.UseVisualStyleBackColor = true;
            this.buttonSetupCamera.Click += new System.EventHandler(this.buttonSetupCamera_Click);
            // 
            // linkNodeButton
            // 
            this.linkNodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkNodeButton.Location = new System.Drawing.Point(186, 46);
            this.linkNodeButton.Margin = new System.Windows.Forms.Padding(2);
            this.linkNodeButton.Name = "linkNodeButton";
            this.linkNodeButton.Size = new System.Drawing.Size(60, 28);
            this.linkNodeButton.TabIndex = 206;
            this.linkNodeButton.Text = "Link Node";
            this.linkNodeButton.UseVisualStyleBackColor = true;
            this.linkNodeButton.Click += new System.EventHandler(this.linkNodeButton_Click);
            // 
            // isFirstCheck
            // 
            this.isFirstCheck.AutoSize = true;
            this.isFirstCheck.Location = new System.Drawing.Point(122, 47);
            this.isFirstCheck.Name = "isFirstCheck";
            this.isFirstCheck.Size = new System.Drawing.Size(52, 17);
            this.isFirstCheck.TabIndex = 22;
            this.isFirstCheck.Text = "isFirst";
            this.isFirstCheck.UseVisualStyleBackColor = true;
            this.isFirstCheck.Click += new System.EventHandler(this.isFirstCheck_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(207, 8);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Z";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(145, 8);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Y";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(78, 8);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "X";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 46);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "Speed";
            // 
            // cameraNodeSpeed
            // 
            this.cameraNodeSpeed.Location = new System.Drawing.Point(55, 44);
            this.cameraNodeSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.cameraNodeSpeed.Name = "cameraNodeSpeed";
            this.cameraNodeSpeed.Size = new System.Drawing.Size(62, 20);
            this.cameraNodeSpeed.TabIndex = 16;
            this.cameraNodeSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed_cameraNode);
            this.cameraNodeSpeed.Leave += new System.EventHandler(this.textChange_cameraNode);
            // 
            // cameraPosZ
            // 
            this.cameraPosZ.Location = new System.Drawing.Point(184, 23);
            this.cameraPosZ.Margin = new System.Windows.Forms.Padding(2);
            this.cameraPosZ.Name = "cameraPosZ";
            this.cameraPosZ.Size = new System.Drawing.Size(62, 20);
            this.cameraPosZ.TabIndex = 3;
            this.cameraPosZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed_cameraNode);
            this.cameraPosZ.Leave += new System.EventHandler(this.textChange_cameraNode);
            // 
            // cameraPosY
            // 
            this.cameraPosY.Location = new System.Drawing.Point(119, 23);
            this.cameraPosY.Margin = new System.Windows.Forms.Padding(2);
            this.cameraPosY.Name = "cameraPosY";
            this.cameraPosY.Size = new System.Drawing.Size(62, 20);
            this.cameraPosY.TabIndex = 2;
            this.cameraPosY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed_cameraNode);
            this.cameraPosY.Leave += new System.EventHandler(this.textChange_cameraNode);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 25);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Position";
            // 
            // cameraPosX
            // 
            this.cameraPosX.Location = new System.Drawing.Point(55, 23);
            this.cameraPosX.Margin = new System.Windows.Forms.Padding(2);
            this.cameraPosX.Name = "cameraPosX";
            this.cameraPosX.Size = new System.Drawing.Size(62, 20);
            this.cameraPosX.TabIndex = 0;
            this.cameraPosX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed_cameraNode);
            this.cameraPosX.Leave += new System.EventHandler(this.textChange_cameraNode);
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
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(250, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 28);
            this.button1.TabIndex = 208;
            this.button1.Text = "Set Camera byTheFace";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1284, 873);
            this.Controls.Add(this.cameraNodePanel);
            this.Controls.Add(this.buttonMoveCameraNode);
            this.Controls.Add(this.addCameraNodeButton);
            this.Controls.Add(this.buttonNewLevel);
            this.Controls.Add(this.superPizzaButton);
            this.Controls.Add(this.addDefaultCamerasButton);
            this.Controls.Add(this.BGColorButton);
            this.Controls.Add(this.texturesCombo);
            this.Controls.Add(this.selectGroup);
            this.Controls.Add(this.canSelectPlayer);
            this.Controls.Add(this.buttonImportLevel);
            this.Controls.Add(this.buttonSaveLevel);
            this.Controls.Add(this.canSelectEnemy);
            this.Controls.Add(this.buttonLoadLevel);
            this.Controls.Add(this.canSelectAnimated);
            this.Controls.Add(this.buttonAddAnimated);
            this.Controls.Add(this.canSelectStatic);
            this.Controls.Add(this.buttonAddStatic);
            this.Controls.Add(this.buttonCreateGroup);
            this.Controls.Add(this.buttonAddEnemy);
            this.Controls.Add(this.buttonScale);
            this.Controls.Add(this.buttonRotate);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.staticPropertiesPanel);
            this.Controls.Add(this.myEditorControl);
            this.Name = "MyEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyEditor";
            this.staticPropertiesPanel.ResumeLayout(false);
            this.staticPropertiesPanel.PerformLayout();
            this.cameraNodePanel.ResumeLayout(false);
            this.cameraNodePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel staticPropertiesPanel;
        public System.Windows.Forms.TextBox textScaleY;
        public System.Windows.Forms.TextBox textScaleX;
        public System.Windows.Forms.TextBox textRotZ;
        public System.Windows.Forms.TextBox textPosZ;
        public System.Windows.Forms.TextBox textPosY;
        public System.Windows.Forms.TextBox textPosX;
        public MyEditorControl myEditorControl;
        private System.Windows.Forms.Button buttonResetPosition;
        private System.Windows.Forms.Button buttonResetScale;
        private System.Windows.Forms.Button buttonResetRotation;
        public System.Windows.Forms.CheckBox canSelectEnemy;
        public System.Windows.Forms.CheckBox canSelectAnimated;
        public System.Windows.Forms.CheckBox canSelectStatic;
        public System.Windows.Forms.CheckBox selectGroup;
        public System.Windows.Forms.CheckBox canSelectPlayer;
        private System.Windows.Forms.Button buttonImportLevel;
        private System.Windows.Forms.Button buttonSaveLevel;
        private System.Windows.Forms.Button buttonLoadLevel;
        private System.Windows.Forms.Button buttonAddAnimated;
        private System.Windows.Forms.Button buttonAddStatic;
        private System.Windows.Forms.Button buttonCreateGroup;
        private System.Windows.Forms.Button buttonAddEnemy;
        private System.Windows.Forms.Button buttonScale;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.Button buttonMove;
        public System.Windows.Forms.ComboBox texturesCombo;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox colorA;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox colorR;
        private System.Windows.Forms.ColorDialog colorDialog;
        public System.Windows.Forms.TextBox colorB;
        public System.Windows.Forms.TextBox colorG;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Button BGColorButton;
        private System.Windows.Forms.Button addDefaultCamerasButton;
        private System.Windows.Forms.Button superPizzaButton;
        private System.Windows.Forms.Button buttonNewLevel;
        public System.Windows.Forms.CheckBox flipHorizontalCheck;
        public System.Windows.Forms.CheckBox flipVerticalCheck;
        private System.Windows.Forms.Button addCameraNodeButton;
        private System.Windows.Forms.Button buttonMoveCameraNode;
        public System.Windows.Forms.Panel cameraNodePanel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox cameraNodeSpeed;
        public System.Windows.Forms.TextBox cameraPosZ;
        public System.Windows.Forms.TextBox cameraPosY;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox cameraPosX;
        private System.Windows.Forms.Button linkNodeButton;
        public System.Windows.Forms.CheckBox isFirstCheck;
        private System.Windows.Forms.Button buttonSetupCamera;
        private System.Windows.Forms.Button button1;
    }
}
#endif