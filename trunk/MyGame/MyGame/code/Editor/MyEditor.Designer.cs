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
            this.buttonImportLevel = new System.Windows.Forms.Button();
            this.buttonSaveLevel = new System.Windows.Forms.Button();
            this.buttonLoadLevel = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.BGColorButton = new System.Windows.Forms.Button();
            this.superPizzaButton = new System.Windows.Forms.Button();
            this.buttonNewLevel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.entities = new System.Windows.Forms.TabPage();
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
            this.texturesCombo = new System.Windows.Forms.ComboBox();
            this.selectGroup = new System.Windows.Forms.CheckBox();
            this.canSelectEnemy = new System.Windows.Forms.CheckBox();
            this.canSelectAnimated = new System.Windows.Forms.CheckBox();
            this.buttonAddAnimated = new System.Windows.Forms.Button();
            this.canSelectStatic = new System.Windows.Forms.CheckBox();
            this.buttonAddStatic = new System.Windows.Forms.Button();
            this.buttonCreateGroup = new System.Windows.Forms.Button();
            this.buttonAddEnemy = new System.Windows.Forms.Button();
            this.buttonScale = new System.Windows.Forms.Button();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.buttonMove = new System.Windows.Forms.Button();
            this.cameras = new System.Windows.Forms.TabPage();
            this.buttonMoveCameraNode = new System.Windows.Forms.Button();
            this.addCameraNodeButton = new System.Windows.Forms.Button();
            this.addDefaultCamerasButton = new System.Windows.Forms.Button();
            this.cameraNodePanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
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
            this.colisions = new System.Windows.Forms.TabPage();
            this.buttonEditColisions = new System.Windows.Forms.Button();
            this.buttonAddColisions = new System.Windows.Forms.Button();
            this.enemyZones = new System.Windows.Forms.TabPage();
            this.enemyCount = new System.Windows.Forms.TextBox();
            this.enemiesCombo = new System.Windows.Forms.ComboBox();
            this.addEnemyZoneButton = new System.Windows.Forms.Button();
            this.triggers = new System.Windows.Forms.TabPage();
            this.removeConsecuenceButton = new System.Windows.Forms.Button();
            this.removeConditionButton = new System.Windows.Forms.Button();
            this.addConsecuenceButton = new System.Windows.Forms.Button();
            this.addConditionButton = new System.Windows.Forms.Button();
            this.triggerConsecuences = new System.Windows.Forms.ComboBox();
            this.triggerConditions = new System.Windows.Forms.ComboBox();
            this.editTriggerButton = new System.Windows.Forms.Button();
            this.availableConsecuences = new System.Windows.Forms.ComboBox();
            this.availableConditions = new System.Windows.Forms.ComboBox();
            this.addTriggerButton = new System.Windows.Forms.Button();
            this.effects = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.effectLifetime = new System.Windows.Forms.TextBox();
            this.NumParticles = new System.Windows.Forms.Label();
            this.effectNumParticles = new System.Windows.Forms.TextBox();
            this.effectDirectionZ = new System.Windows.Forms.TextBox();
            this.effectDirectionY = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.effectColorB = new System.Windows.Forms.TextBox();
            this.effectColorG = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.effectColorR = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.effectScale = new System.Windows.Forms.TextBox();
            this.effectDirectionX = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.effectPositionZ = new System.Windows.Forms.TextBox();
            this.effectPositionY = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.effectPositionX = new System.Windows.Forms.TextBox();
            this.editEffectButton = new System.Windows.Forms.Button();
            this.effectsCombo = new System.Windows.Forms.ComboBox();
            this.addEffectButton = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.effectDuplicateButton = new System.Windows.Forms.Button();
            this.label47 = new System.Windows.Forms.Label();
            this.ee_FadeOut = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.ee_FadeIn = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.ee_Life = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.ee_SizeEnd = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.ee_SizeIni = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.ee_size = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.ee_RotSpeedVar = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.ee_RotSpeed = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.ee_RotVar = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.ee_Rot = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.ee_SystemLife = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.ee_NumPart = new System.Windows.Forms.TextBox();
            this.ee_ColorMaxA = new System.Windows.Forms.TextBox();
            this.ee_ColorMinA = new System.Windows.Forms.TextBox();
            this.ee_ColorA = new System.Windows.Forms.TextBox();
            this.ee_ColorMaxB = new System.Windows.Forms.TextBox();
            this.ee_ColorMaxG = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.ee_ColorMaxR = new System.Windows.Forms.TextBox();
            this.ee_ColorMinB = new System.Windows.Forms.TextBox();
            this.ee_ColorMinG = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.ee_ColorMinR = new System.Windows.Forms.TextBox();
            this.ee_ColorB = new System.Windows.Forms.TextBox();
            this.ee_ColorG = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.ee_ColorR = new System.Windows.Forms.TextBox();
            this.ee_AccMaxZ = new System.Windows.Forms.TextBox();
            this.ee_AccMaxY = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.ee_AccMaxX = new System.Windows.Forms.TextBox();
            this.ee_AccMinZ = new System.Windows.Forms.TextBox();
            this.ee_AccMinY = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.ee_AccMinX = new System.Windows.Forms.TextBox();
            this.ee_AccZ = new System.Windows.Forms.TextBox();
            this.ee_AccY = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.ee_AccX = new System.Windows.Forms.TextBox();
            this.ee_DirMaxZ = new System.Windows.Forms.TextBox();
            this.ee_DirMaxY = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.ee_DirMaxX = new System.Windows.Forms.TextBox();
            this.ee_DirMinZ = new System.Windows.Forms.TextBox();
            this.ee_DirMinY = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.ee_DirMinX = new System.Windows.Forms.TextBox();
            this.ee_DirZ = new System.Windows.Forms.TextBox();
            this.ee_DirY = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.ee_DirX = new System.Windows.Forms.TextBox();
            this.ee_PosMaxZ = new System.Windows.Forms.TextBox();
            this.ee_PosMaxY = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.ee_PosMaxX = new System.Windows.Forms.TextBox();
            this.ee_PosMinZ = new System.Windows.Forms.TextBox();
            this.ee_PosMinY = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.ee_PosMinX = new System.Windows.Forms.TextBox();
            this.ee_PosZ = new System.Windows.Forms.TextBox();
            this.ee_PosY = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.ee_posX = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.ee_render = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.ee_type = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ee_texture = new System.Windows.Forms.ComboBox();
            this.reloadEffectsButton = new System.Windows.Forms.Button();
            this.editEffectsButton = new System.Windows.Forms.Button();
            this.editEffectsList = new System.Windows.Forms.ComboBox();
            this.saveEffectsButton = new System.Windows.Forms.Button();
            this.myEditorControl = new MyGame.MyEditorControl();
            this.editEnemyZoneButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.entities.SuspendLayout();
            this.staticPropertiesPanel.SuspendLayout();
            this.cameras.SuspendLayout();
            this.cameraNodePanel.SuspendLayout();
            this.colisions.SuspendLayout();
            this.enemyZones.SuspendLayout();
            this.triggers.SuspendLayout();
            this.effects.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonImportLevel
            // 
            this.buttonImportLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImportLevel.Location = new System.Drawing.Point(1167, 2);
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
            this.buttonSaveLevel.Location = new System.Drawing.Point(1112, 34);
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
            this.buttonLoadLevel.Location = new System.Drawing.Point(1112, 2);
            this.buttonLoadLevel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLoadLevel.Name = "buttonLoadLevel";
            this.buttonLoadLevel.Size = new System.Drawing.Size(51, 28);
            this.buttonLoadLevel.TabIndex = 27;
            this.buttonLoadLevel.Text = "Load Level";
            this.buttonLoadLevel.UseVisualStyleBackColor = true;
            this.buttonLoadLevel.Click += new System.EventHandler(this.buttonLoadLevel_Click);
            // 
            // BGColorButton
            // 
            this.BGColorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BGColorButton.Location = new System.Drawing.Point(1222, 34);
            this.BGColorButton.Margin = new System.Windows.Forms.Padding(2);
            this.BGColorButton.Name = "BGColorButton";
            this.BGColorButton.Size = new System.Drawing.Size(51, 28);
            this.BGColorButton.TabIndex = 30;
            this.BGColorButton.Text = "BG Color";
            this.BGColorButton.UseVisualStyleBackColor = true;
            this.BGColorButton.Click += new System.EventHandler(this.BGColorButton_Click);
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
            this.buttonNewLevel.Location = new System.Drawing.Point(1167, 34);
            this.buttonNewLevel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewLevel.Name = "buttonNewLevel";
            this.buttonNewLevel.Size = new System.Drawing.Size(51, 28);
            this.buttonNewLevel.TabIndex = 202;
            this.buttonNewLevel.Text = "New Level";
            this.buttonNewLevel.UseVisualStyleBackColor = true;
            this.buttonNewLevel.Click += new System.EventHandler(this.buttonNewLevel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.entities);
            this.tabControl1.Controls.Add(this.cameras);
            this.tabControl1.Controls.Add(this.colisions);
            this.tabControl1.Controls.Add(this.enemyZones);
            this.tabControl1.Controls.Add(this.triggers);
            this.tabControl1.Controls.Add(this.effects);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1100, 126);
            this.tabControl1.TabIndex = 209;
            // 
            // entities
            // 
            this.entities.Controls.Add(this.staticPropertiesPanel);
            this.entities.Controls.Add(this.texturesCombo);
            this.entities.Controls.Add(this.selectGroup);
            this.entities.Controls.Add(this.canSelectEnemy);
            this.entities.Controls.Add(this.canSelectAnimated);
            this.entities.Controls.Add(this.buttonAddAnimated);
            this.entities.Controls.Add(this.canSelectStatic);
            this.entities.Controls.Add(this.buttonAddStatic);
            this.entities.Controls.Add(this.buttonCreateGroup);
            this.entities.Controls.Add(this.buttonAddEnemy);
            this.entities.Controls.Add(this.buttonScale);
            this.entities.Controls.Add(this.buttonRotate);
            this.entities.Controls.Add(this.buttonMove);
            this.entities.Location = new System.Drawing.Point(4, 22);
            this.entities.Name = "entities";
            this.entities.Padding = new System.Windows.Forms.Padding(3);
            this.entities.Size = new System.Drawing.Size(1092, 100);
            this.entities.TabIndex = 0;
            this.entities.Text = "Entities";
            this.entities.UseVisualStyleBackColor = true;
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
            this.staticPropertiesPanel.Location = new System.Drawing.Point(380, 5);
            this.staticPropertiesPanel.Margin = new System.Windows.Forms.Padding(2);
            this.staticPropertiesPanel.Name = "staticPropertiesPanel";
            this.staticPropertiesPanel.Size = new System.Drawing.Size(492, 91);
            this.staticPropertiesPanel.TabIndex = 213;
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
            this.flipVerticalCheck.CheckStateChanged += new System.EventHandler(this.flip_CheckedChanged);
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
            this.flipHorizontalCheck.CheckStateChanged += new System.EventHandler(this.flip_CheckedChanged);
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
            // texturesCombo
            // 
            this.texturesCombo.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.texturesCombo.CausesValidation = false;
            this.texturesCombo.FormattingEnabled = true;
            this.texturesCombo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.texturesCombo.Location = new System.Drawing.Point(192, 42);
            this.texturesCombo.Name = "texturesCombo";
            this.texturesCombo.Size = new System.Drawing.Size(183, 21);
            this.texturesCombo.TabIndex = 212;
            this.texturesCombo.TabStop = false;
            this.texturesCombo.SelectedIndexChanged += new System.EventHandler(this.texturesCombo_SelectedIndexChanged);
            // 
            // selectGroup
            // 
            this.selectGroup.AutoSize = true;
            this.selectGroup.Location = new System.Drawing.Point(200, 77);
            this.selectGroup.Name = "selectGroup";
            this.selectGroup.Size = new System.Drawing.Size(53, 17);
            this.selectGroup.TabIndex = 204;
            this.selectGroup.Text = "group";
            this.selectGroup.UseVisualStyleBackColor = true;
            // 
            // canSelectEnemy
            // 
            this.canSelectEnemy.AutoSize = true;
            this.canSelectEnemy.Checked = true;
            this.canSelectEnemy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.canSelectEnemy.Location = new System.Drawing.Point(136, 77);
            this.canSelectEnemy.Name = "canSelectEnemy";
            this.canSelectEnemy.Size = new System.Drawing.Size(58, 17);
            this.canSelectEnemy.TabIndex = 203;
            this.canSelectEnemy.Text = "Enemy";
            this.canSelectEnemy.UseVisualStyleBackColor = true;
            // 
            // canSelectAnimated
            // 
            this.canSelectAnimated.AutoSize = true;
            this.canSelectAnimated.Checked = true;
            this.canSelectAnimated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.canSelectAnimated.Location = new System.Drawing.Point(63, 77);
            this.canSelectAnimated.Name = "canSelectAnimated";
            this.canSelectAnimated.Size = new System.Drawing.Size(70, 17);
            this.canSelectAnimated.TabIndex = 202;
            this.canSelectAnimated.Text = "Animated";
            this.canSelectAnimated.UseVisualStyleBackColor = true;
            // 
            // buttonAddAnimated
            // 
            this.buttonAddAnimated.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddAnimated.Location = new System.Drawing.Point(72, 39);
            this.buttonAddAnimated.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddAnimated.Name = "buttonAddAnimated";
            this.buttonAddAnimated.Size = new System.Drawing.Size(51, 28);
            this.buttonAddAnimated.TabIndex = 211;
            this.buttonAddAnimated.Text = "Add Animated Prop";
            this.buttonAddAnimated.UseVisualStyleBackColor = true;
            this.buttonAddAnimated.Click += new System.EventHandler(this.button_Click);
            // 
            // canSelectStatic
            // 
            this.canSelectStatic.AutoSize = true;
            this.canSelectStatic.Checked = true;
            this.canSelectStatic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.canSelectStatic.Location = new System.Drawing.Point(5, 77);
            this.canSelectStatic.Name = "canSelectStatic";
            this.canSelectStatic.Size = new System.Drawing.Size(53, 17);
            this.canSelectStatic.TabIndex = 201;
            this.canSelectStatic.Text = "Static";
            this.canSelectStatic.UseVisualStyleBackColor = true;
            // 
            // buttonAddStatic
            // 
            this.buttonAddStatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddStatic.Location = new System.Drawing.Point(7, 39);
            this.buttonAddStatic.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddStatic.Name = "buttonAddStatic";
            this.buttonAddStatic.Size = new System.Drawing.Size(51, 28);
            this.buttonAddStatic.TabIndex = 210;
            this.buttonAddStatic.Text = "Add Static Prop";
            this.buttonAddStatic.UseVisualStyleBackColor = true;
            this.buttonAddStatic.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonCreateGroup
            // 
            this.buttonCreateGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateGroup.Location = new System.Drawing.Point(192, 5);
            this.buttonCreateGroup.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCreateGroup.Name = "buttonCreateGroup";
            this.buttonCreateGroup.Size = new System.Drawing.Size(51, 28);
            this.buttonCreateGroup.TabIndex = 209;
            this.buttonCreateGroup.Text = "Create Group";
            this.buttonCreateGroup.UseVisualStyleBackColor = true;
            this.buttonCreateGroup.Click += new System.EventHandler(this.buttonCreateGroup_Click);
            // 
            // buttonAddEnemy
            // 
            this.buttonAddEnemy.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddEnemy.Location = new System.Drawing.Point(136, 39);
            this.buttonAddEnemy.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddEnemy.Name = "buttonAddEnemy";
            this.buttonAddEnemy.Size = new System.Drawing.Size(51, 28);
            this.buttonAddEnemy.TabIndex = 208;
            this.buttonAddEnemy.Text = "Add Enemy";
            this.buttonAddEnemy.UseVisualStyleBackColor = true;
            this.buttonAddEnemy.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonScale
            // 
            this.buttonScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonScale.Location = new System.Drawing.Point(136, 5);
            this.buttonScale.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScale.Name = "buttonScale";
            this.buttonScale.Size = new System.Drawing.Size(51, 28);
            this.buttonScale.TabIndex = 207;
            this.buttonScale.Tag = "";
            this.buttonScale.Text = "Scale";
            this.buttonScale.UseVisualStyleBackColor = true;
            this.buttonScale.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonRotate
            // 
            this.buttonRotate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRotate.Location = new System.Drawing.Point(72, 5);
            this.buttonRotate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(51, 28);
            this.buttonRotate.TabIndex = 206;
            this.buttonRotate.Tag = "";
            this.buttonRotate.Text = "Rotate";
            this.buttonRotate.UseVisualStyleBackColor = true;
            this.buttonRotate.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonMove
            // 
            this.buttonMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMove.Location = new System.Drawing.Point(7, 5);
            this.buttonMove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(51, 28);
            this.buttonMove.TabIndex = 205;
            this.buttonMove.Tag = "";
            this.buttonMove.Text = "Move";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.button_Click);
            // 
            // cameras
            // 
            this.cameras.Controls.Add(this.buttonMoveCameraNode);
            this.cameras.Controls.Add(this.addCameraNodeButton);
            this.cameras.Controls.Add(this.addDefaultCamerasButton);
            this.cameras.Controls.Add(this.cameraNodePanel);
            this.cameras.Location = new System.Drawing.Point(4, 22);
            this.cameras.Name = "cameras";
            this.cameras.Padding = new System.Windows.Forms.Padding(3);
            this.cameras.Size = new System.Drawing.Size(1092, 100);
            this.cameras.TabIndex = 1;
            this.cameras.Text = "Cameras";
            this.cameras.UseVisualStyleBackColor = true;
            // 
            // buttonMoveCameraNode
            // 
            this.buttonMoveCameraNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMoveCameraNode.Location = new System.Drawing.Point(7, 46);
            this.buttonMoveCameraNode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMoveCameraNode.Name = "buttonMoveCameraNode";
            this.buttonMoveCameraNode.Size = new System.Drawing.Size(60, 28);
            this.buttonMoveCameraNode.TabIndex = 210;
            this.buttonMoveCameraNode.Text = "Edit Camera Node";
            this.buttonMoveCameraNode.UseVisualStyleBackColor = true;
            this.buttonMoveCameraNode.Click += new System.EventHandler(this.button_Click);
            // 
            // addCameraNodeButton
            // 
            this.addCameraNodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCameraNodeButton.Location = new System.Drawing.Point(7, 8);
            this.addCameraNodeButton.Margin = new System.Windows.Forms.Padding(2);
            this.addCameraNodeButton.Name = "addCameraNodeButton";
            this.addCameraNodeButton.Size = new System.Drawing.Size(60, 28);
            this.addCameraNodeButton.TabIndex = 209;
            this.addCameraNodeButton.Text = "Add Camera Node";
            this.addCameraNodeButton.UseVisualStyleBackColor = true;
            this.addCameraNodeButton.Click += new System.EventHandler(this.button_Click);
            // 
            // addDefaultCamerasButton
            // 
            this.addDefaultCamerasButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addDefaultCamerasButton.Location = new System.Drawing.Point(80, 8);
            this.addDefaultCamerasButton.Margin = new System.Windows.Forms.Padding(2);
            this.addDefaultCamerasButton.Name = "addDefaultCamerasButton";
            this.addDefaultCamerasButton.Size = new System.Drawing.Size(60, 28);
            this.addDefaultCamerasButton.TabIndex = 208;
            this.addDefaultCamerasButton.Text = "Add Default Cameras";
            this.addDefaultCamerasButton.UseVisualStyleBackColor = true;
            this.addDefaultCamerasButton.Click += new System.EventHandler(this.addDefaultCamerasButton_Click);
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
            this.cameraNodePanel.Location = new System.Drawing.Point(161, 8);
            this.cameraNodePanel.Margin = new System.Windows.Forms.Padding(2);
            this.cameraNodePanel.Name = "cameraNodePanel";
            this.cameraNodePanel.Size = new System.Drawing.Size(336, 91);
            this.cameraNodePanel.TabIndex = 207;
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
            this.button1.Click += new System.EventHandler(this.buttonSetCameraByTheFace_Click);
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
            this.isFirstCheck.CheckStateChanged += new System.EventHandler(this.isFirstCheck_CheckedChanged);
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
            // colisions
            // 
            this.colisions.Controls.Add(this.buttonEditColisions);
            this.colisions.Controls.Add(this.buttonAddColisions);
            this.colisions.Location = new System.Drawing.Point(4, 22);
            this.colisions.Name = "colisions";
            this.colisions.Size = new System.Drawing.Size(1092, 100);
            this.colisions.TabIndex = 2;
            this.colisions.Text = "Colisions";
            this.colisions.UseVisualStyleBackColor = true;
            // 
            // buttonEditColisions
            // 
            this.buttonEditColisions.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditColisions.Location = new System.Drawing.Point(7, 42);
            this.buttonEditColisions.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEditColisions.Name = "buttonEditColisions";
            this.buttonEditColisions.Size = new System.Drawing.Size(60, 28);
            this.buttonEditColisions.TabIndex = 210;
            this.buttonEditColisions.Text = "Edit Colisions";
            this.buttonEditColisions.UseVisualStyleBackColor = true;
            this.buttonEditColisions.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonAddColisions
            // 
            this.buttonAddColisions.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddColisions.Location = new System.Drawing.Point(7, 10);
            this.buttonAddColisions.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddColisions.Name = "buttonAddColisions";
            this.buttonAddColisions.Size = new System.Drawing.Size(60, 28);
            this.buttonAddColisions.TabIndex = 209;
            this.buttonAddColisions.Text = "Add Colisions";
            this.buttonAddColisions.UseVisualStyleBackColor = true;
            this.buttonAddColisions.Click += new System.EventHandler(this.button_Click);
            // 
            // enemyZones
            // 
            this.enemyZones.Controls.Add(this.editEnemyZoneButton);
            this.enemyZones.Controls.Add(this.enemyCount);
            this.enemyZones.Controls.Add(this.enemiesCombo);
            this.enemyZones.Controls.Add(this.addEnemyZoneButton);
            this.enemyZones.Location = new System.Drawing.Point(4, 22);
            this.enemyZones.Name = "enemyZones";
            this.enemyZones.Size = new System.Drawing.Size(1092, 100);
            this.enemyZones.TabIndex = 3;
            this.enemyZones.Text = "EnemyZones";
            this.enemyZones.UseVisualStyleBackColor = true;
            // 
            // enemyCount
            // 
            this.enemyCount.Location = new System.Drawing.Point(261, 14);
            this.enemyCount.Name = "enemyCount";
            this.enemyCount.Size = new System.Drawing.Size(50, 20);
            this.enemyCount.TabIndex = 214;
            // 
            // enemiesCombo
            // 
            this.enemiesCombo.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.enemiesCombo.CausesValidation = false;
            this.enemiesCombo.FormattingEnabled = true;
            this.enemiesCombo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.enemiesCombo.Location = new System.Drawing.Point(72, 13);
            this.enemiesCombo.Name = "enemiesCombo";
            this.enemiesCombo.Size = new System.Drawing.Size(183, 21);
            this.enemiesCombo.TabIndex = 213;
            this.enemiesCombo.TabStop = false;
            // 
            // addEnemyZoneButton
            // 
            this.addEnemyZoneButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addEnemyZoneButton.Location = new System.Drawing.Point(7, 10);
            this.addEnemyZoneButton.Margin = new System.Windows.Forms.Padding(2);
            this.addEnemyZoneButton.Name = "addEnemyZoneButton";
            this.addEnemyZoneButton.Size = new System.Drawing.Size(60, 28);
            this.addEnemyZoneButton.TabIndex = 210;
            this.addEnemyZoneButton.Text = "Add Enemy Zone";
            this.addEnemyZoneButton.UseVisualStyleBackColor = true;
            this.addEnemyZoneButton.Click += new System.EventHandler(this.button_Click);
            // 
            // triggers
            // 
            this.triggers.Controls.Add(this.removeConsecuenceButton);
            this.triggers.Controls.Add(this.removeConditionButton);
            this.triggers.Controls.Add(this.addConsecuenceButton);
            this.triggers.Controls.Add(this.addConditionButton);
            this.triggers.Controls.Add(this.triggerConsecuences);
            this.triggers.Controls.Add(this.triggerConditions);
            this.triggers.Controls.Add(this.editTriggerButton);
            this.triggers.Controls.Add(this.availableConsecuences);
            this.triggers.Controls.Add(this.availableConditions);
            this.triggers.Controls.Add(this.addTriggerButton);
            this.triggers.Location = new System.Drawing.Point(4, 22);
            this.triggers.Name = "triggers";
            this.triggers.Size = new System.Drawing.Size(1092, 100);
            this.triggers.TabIndex = 4;
            this.triggers.Text = "Triggers";
            this.triggers.UseVisualStyleBackColor = true;
            // 
            // removeConsecuenceButton
            // 
            this.removeConsecuenceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeConsecuenceButton.Location = new System.Drawing.Point(518, 53);
            this.removeConsecuenceButton.Margin = new System.Windows.Forms.Padding(2);
            this.removeConsecuenceButton.Name = "removeConsecuenceButton";
            this.removeConsecuenceButton.Size = new System.Drawing.Size(45, 21);
            this.removeConsecuenceButton.TabIndex = 222;
            this.removeConsecuenceButton.Text = "Remove";
            this.removeConsecuenceButton.UseVisualStyleBackColor = true;
            this.removeConsecuenceButton.Click += new System.EventHandler(this.removeConsecuenceButton_Click);
            // 
            // removeConditionButton
            // 
            this.removeConditionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeConditionButton.Location = new System.Drawing.Point(518, 13);
            this.removeConditionButton.Margin = new System.Windows.Forms.Padding(2);
            this.removeConditionButton.Name = "removeConditionButton";
            this.removeConditionButton.Size = new System.Drawing.Size(45, 21);
            this.removeConditionButton.TabIndex = 221;
            this.removeConditionButton.Text = "Remove";
            this.removeConditionButton.UseVisualStyleBackColor = true;
            this.removeConditionButton.Click += new System.EventHandler(this.removeConditionButton_Click);
            // 
            // addConsecuenceButton
            // 
            this.addConsecuenceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addConsecuenceButton.Location = new System.Drawing.Point(271, 53);
            this.addConsecuenceButton.Margin = new System.Windows.Forms.Padding(2);
            this.addConsecuenceButton.Name = "addConsecuenceButton";
            this.addConsecuenceButton.Size = new System.Drawing.Size(45, 21);
            this.addConsecuenceButton.TabIndex = 220;
            this.addConsecuenceButton.Text = "Add";
            this.addConsecuenceButton.UseVisualStyleBackColor = true;
            this.addConsecuenceButton.Click += new System.EventHandler(this.addConsecuenceButton_Click);
            // 
            // addConditionButton
            // 
            this.addConditionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addConditionButton.Location = new System.Drawing.Point(271, 13);
            this.addConditionButton.Margin = new System.Windows.Forms.Padding(2);
            this.addConditionButton.Name = "addConditionButton";
            this.addConditionButton.Size = new System.Drawing.Size(45, 21);
            this.addConditionButton.TabIndex = 219;
            this.addConditionButton.Text = "Add";
            this.addConditionButton.UseVisualStyleBackColor = true;
            this.addConditionButton.Click += new System.EventHandler(this.addConditionButton_Click);
            // 
            // triggerConsecuences
            // 
            this.triggerConsecuences.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.triggerConsecuences.CausesValidation = false;
            this.triggerConsecuences.FormattingEnabled = true;
            this.triggerConsecuences.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.triggerConsecuences.Location = new System.Drawing.Point(321, 53);
            this.triggerConsecuences.Name = "triggerConsecuences";
            this.triggerConsecuences.Size = new System.Drawing.Size(183, 21);
            this.triggerConsecuences.TabIndex = 218;
            this.triggerConsecuences.TabStop = false;
            // 
            // triggerConditions
            // 
            this.triggerConditions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.triggerConditions.CausesValidation = false;
            this.triggerConditions.FormattingEnabled = true;
            this.triggerConditions.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.triggerConditions.Location = new System.Drawing.Point(321, 13);
            this.triggerConditions.Name = "triggerConditions";
            this.triggerConditions.Size = new System.Drawing.Size(183, 21);
            this.triggerConditions.TabIndex = 217;
            this.triggerConditions.TabStop = false;
            // 
            // editTriggerButton
            // 
            this.editTriggerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editTriggerButton.Location = new System.Drawing.Point(7, 50);
            this.editTriggerButton.Margin = new System.Windows.Forms.Padding(2);
            this.editTriggerButton.Name = "editTriggerButton";
            this.editTriggerButton.Size = new System.Drawing.Size(60, 28);
            this.editTriggerButton.TabIndex = 216;
            this.editTriggerButton.Text = "Edit Trigger";
            this.editTriggerButton.UseVisualStyleBackColor = true;
            this.editTriggerButton.Click += new System.EventHandler(this.button_Click);
            // 
            // availableConsecuences
            // 
            this.availableConsecuences.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.availableConsecuences.CausesValidation = false;
            this.availableConsecuences.FormattingEnabled = true;
            this.availableConsecuences.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.availableConsecuences.Location = new System.Drawing.Point(83, 53);
            this.availableConsecuences.Name = "availableConsecuences";
            this.availableConsecuences.Size = new System.Drawing.Size(183, 21);
            this.availableConsecuences.TabIndex = 215;
            this.availableConsecuences.TabStop = false;
            // 
            // availableConditions
            // 
            this.availableConditions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.availableConditions.CausesValidation = false;
            this.availableConditions.FormattingEnabled = true;
            this.availableConditions.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.availableConditions.Location = new System.Drawing.Point(83, 13);
            this.availableConditions.Name = "availableConditions";
            this.availableConditions.Size = new System.Drawing.Size(183, 21);
            this.availableConditions.TabIndex = 214;
            this.availableConditions.TabStop = false;
            // 
            // addTriggerButton
            // 
            this.addTriggerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addTriggerButton.Location = new System.Drawing.Point(7, 10);
            this.addTriggerButton.Margin = new System.Windows.Forms.Padding(2);
            this.addTriggerButton.Name = "addTriggerButton";
            this.addTriggerButton.Size = new System.Drawing.Size(60, 28);
            this.addTriggerButton.TabIndex = 211;
            this.addTriggerButton.Text = "Add Trigger";
            this.addTriggerButton.UseVisualStyleBackColor = true;
            this.addTriggerButton.Click += new System.EventHandler(this.button_Click);
            // 
            // effects
            // 
            this.effects.Controls.Add(this.panel1);
            this.effects.Controls.Add(this.editEffectButton);
            this.effects.Controls.Add(this.effectsCombo);
            this.effects.Controls.Add(this.addEffectButton);
            this.effects.Location = new System.Drawing.Point(4, 22);
            this.effects.Name = "effects";
            this.effects.Size = new System.Drawing.Size(1092, 100);
            this.effects.TabIndex = 5;
            this.effects.Text = "Effects";
            this.effects.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.effectLifetime);
            this.panel1.Controls.Add(this.NumParticles);
            this.panel1.Controls.Add(this.effectNumParticles);
            this.panel1.Controls.Add(this.effectDirectionZ);
            this.panel1.Controls.Add(this.effectDirectionY);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.effectColorB);
            this.panel1.Controls.Add(this.effectColorG);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.effectColorR);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.effectScale);
            this.panel1.Controls.Add(this.effectDirectionX);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.effectPositionZ);
            this.panel1.Controls.Add(this.effectPositionY);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.effectPositionX);
            this.panel1.Location = new System.Drawing.Point(383, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 91);
            this.panel1.TabIndex = 217;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(305, 68);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(43, 13);
            this.label22.TabIndex = 39;
            this.label22.Text = "Lifetime";
            // 
            // effectLifetime
            // 
            this.effectLifetime.Location = new System.Drawing.Point(404, 64);
            this.effectLifetime.Margin = new System.Windows.Forms.Padding(2);
            this.effectLifetime.Name = "effectLifetime";
            this.effectLifetime.Size = new System.Drawing.Size(30, 20);
            this.effectLifetime.TabIndex = 38;
            this.effectLifetime.Leave += new System.EventHandler(this.effect_TextChanged);
            // 
            // NumParticles
            // 
            this.NumParticles.AutoSize = true;
            this.NumParticles.Location = new System.Drawing.Point(305, 46);
            this.NumParticles.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NumParticles.Name = "NumParticles";
            this.NumParticles.Size = new System.Drawing.Size(69, 13);
            this.NumParticles.TabIndex = 37;
            this.NumParticles.Text = "NumParticles";
            // 
            // effectNumParticles
            // 
            this.effectNumParticles.Location = new System.Drawing.Point(404, 43);
            this.effectNumParticles.Margin = new System.Windows.Forms.Padding(2);
            this.effectNumParticles.Name = "effectNumParticles";
            this.effectNumParticles.Size = new System.Drawing.Size(30, 20);
            this.effectNumParticles.TabIndex = 36;
            this.effectNumParticles.Leave += new System.EventHandler(this.effect_TextChanged);
            // 
            // effectDirectionZ
            // 
            this.effectDirectionZ.Location = new System.Drawing.Point(184, 44);
            this.effectDirectionZ.Margin = new System.Windows.Forms.Padding(2);
            this.effectDirectionZ.Name = "effectDirectionZ";
            this.effectDirectionZ.Size = new System.Drawing.Size(62, 20);
            this.effectDirectionZ.TabIndex = 35;
            this.effectDirectionZ.Leave += new System.EventHandler(this.effect_TextChanged);
            // 
            // effectDirectionY
            // 
            this.effectDirectionY.Location = new System.Drawing.Point(119, 44);
            this.effectDirectionY.Margin = new System.Windows.Forms.Padding(2);
            this.effectDirectionY.Name = "effectDirectionY";
            this.effectDirectionY.Size = new System.Drawing.Size(62, 20);
            this.effectDirectionY.TabIndex = 34;
            this.effectDirectionY.Leave += new System.EventHandler(this.effect_TextChanged);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(438, 22);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 20);
            this.button2.TabIndex = 31;
            this.button2.Text = "Color";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // effectColorB
            // 
            this.effectColorB.Location = new System.Drawing.Point(404, 22);
            this.effectColorB.Margin = new System.Windows.Forms.Padding(2);
            this.effectColorB.Name = "effectColorB";
            this.effectColorB.Size = new System.Drawing.Size(30, 20);
            this.effectColorB.TabIndex = 30;
            // 
            // effectColorG
            // 
            this.effectColorG.Location = new System.Drawing.Point(372, 22);
            this.effectColorG.Margin = new System.Windows.Forms.Padding(2);
            this.effectColorG.Name = "effectColorG";
            this.effectColorG.Size = new System.Drawing.Size(30, 20);
            this.effectColorG.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(305, 26);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Color";
            // 
            // effectColorR
            // 
            this.effectColorR.Location = new System.Drawing.Point(340, 22);
            this.effectColorR.Margin = new System.Windows.Forms.Padding(2);
            this.effectColorR.Name = "effectColorR";
            this.effectColorR.Size = new System.Drawing.Size(30, 20);
            this.effectColorR.TabIndex = 25;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(250, 23);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(51, 20);
            this.button3.TabIndex = 24;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(250, 65);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(51, 20);
            this.button4.TabIndex = 23;
            this.button4.Text = "Reset";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(250, 44);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(51, 20);
            this.button5.TabIndex = 22;
            this.button5.Text = "Reset";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(207, 8);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 13);
            this.label15.TabIndex = 21;
            this.label15.Text = "Z";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(145, 8);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 13);
            this.label17.TabIndex = 20;
            this.label17.Text = "Y";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(78, 8);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 13);
            this.label18.TabIndex = 19;
            this.label18.Text = "X";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 67);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 17;
            this.label19.Text = "Scale";
            // 
            // effectScale
            // 
            this.effectScale.Location = new System.Drawing.Point(55, 65);
            this.effectScale.Margin = new System.Windows.Forms.Padding(2);
            this.effectScale.Name = "effectScale";
            this.effectScale.Size = new System.Drawing.Size(62, 20);
            this.effectScale.TabIndex = 16;
            this.effectScale.Leave += new System.EventHandler(this.effect_TextChanged);
            // 
            // effectDirectionX
            // 
            this.effectDirectionX.Location = new System.Drawing.Point(55, 44);
            this.effectDirectionX.Margin = new System.Windows.Forms.Padding(2);
            this.effectDirectionX.Name = "effectDirectionX";
            this.effectDirectionX.Size = new System.Drawing.Size(62, 20);
            this.effectDirectionX.TabIndex = 7;
            this.effectDirectionX.Leave += new System.EventHandler(this.effect_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 46);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 13);
            this.label20.TabIndex = 5;
            this.label20.Text = "Direction";
            // 
            // effectPositionZ
            // 
            this.effectPositionZ.Enabled = false;
            this.effectPositionZ.Location = new System.Drawing.Point(184, 23);
            this.effectPositionZ.Margin = new System.Windows.Forms.Padding(2);
            this.effectPositionZ.Name = "effectPositionZ";
            this.effectPositionZ.Size = new System.Drawing.Size(62, 20);
            this.effectPositionZ.TabIndex = 3;
            // 
            // effectPositionY
            // 
            this.effectPositionY.Enabled = false;
            this.effectPositionY.Location = new System.Drawing.Point(119, 23);
            this.effectPositionY.Margin = new System.Windows.Forms.Padding(2);
            this.effectPositionY.Name = "effectPositionY";
            this.effectPositionY.Size = new System.Drawing.Size(62, 20);
            this.effectPositionY.TabIndex = 2;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 25);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(44, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "Position";
            // 
            // effectPositionX
            // 
            this.effectPositionX.Enabled = false;
            this.effectPositionX.Location = new System.Drawing.Point(55, 23);
            this.effectPositionX.Margin = new System.Windows.Forms.Padding(2);
            this.effectPositionX.Name = "effectPositionX";
            this.effectPositionX.Size = new System.Drawing.Size(62, 20);
            this.effectPositionX.TabIndex = 0;
            // 
            // editEffectButton
            // 
            this.editEffectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editEffectButton.Location = new System.Drawing.Point(7, 42);
            this.editEffectButton.Margin = new System.Windows.Forms.Padding(2);
            this.editEffectButton.Name = "editEffectButton";
            this.editEffectButton.Size = new System.Drawing.Size(60, 28);
            this.editEffectButton.TabIndex = 216;
            this.editEffectButton.Text = "Edit Effect";
            this.editEffectButton.UseVisualStyleBackColor = true;
            this.editEffectButton.Click += new System.EventHandler(this.button_Click);
            // 
            // effectsCombo
            // 
            this.effectsCombo.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.effectsCombo.CausesValidation = false;
            this.effectsCombo.FormattingEnabled = true;
            this.effectsCombo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.effectsCombo.Location = new System.Drawing.Point(72, 13);
            this.effectsCombo.Name = "effectsCombo";
            this.effectsCombo.Size = new System.Drawing.Size(183, 21);
            this.effectsCombo.TabIndex = 215;
            this.effectsCombo.TabStop = false;
            this.effectsCombo.SelectedValueChanged += new System.EventHandler(this.effectsCombo_SelectedValueChanged);
            // 
            // addEffectButton
            // 
            this.addEffectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addEffectButton.Location = new System.Drawing.Point(7, 10);
            this.addEffectButton.Margin = new System.Windows.Forms.Padding(2);
            this.addEffectButton.Name = "addEffectButton";
            this.addEffectButton.Size = new System.Drawing.Size(60, 28);
            this.addEffectButton.TabIndex = 212;
            this.addEffectButton.Text = "Add Effect";
            this.addEffectButton.UseVisualStyleBackColor = true;
            this.addEffectButton.Click += new System.EventHandler(this.button_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.effectDuplicateButton);
            this.tabPage1.Controls.Add(this.label47);
            this.tabPage1.Controls.Add(this.ee_FadeOut);
            this.tabPage1.Controls.Add(this.label48);
            this.tabPage1.Controls.Add(this.ee_FadeIn);
            this.tabPage1.Controls.Add(this.label45);
            this.tabPage1.Controls.Add(this.ee_Life);
            this.tabPage1.Controls.Add(this.label46);
            this.tabPage1.Controls.Add(this.ee_SizeEnd);
            this.tabPage1.Controls.Add(this.label43);
            this.tabPage1.Controls.Add(this.ee_SizeIni);
            this.tabPage1.Controls.Add(this.label44);
            this.tabPage1.Controls.Add(this.ee_size);
            this.tabPage1.Controls.Add(this.label41);
            this.tabPage1.Controls.Add(this.ee_RotSpeedVar);
            this.tabPage1.Controls.Add(this.label42);
            this.tabPage1.Controls.Add(this.ee_RotSpeed);
            this.tabPage1.Controls.Add(this.label39);
            this.tabPage1.Controls.Add(this.ee_RotVar);
            this.tabPage1.Controls.Add(this.label40);
            this.tabPage1.Controls.Add(this.ee_Rot);
            this.tabPage1.Controls.Add(this.label38);
            this.tabPage1.Controls.Add(this.ee_SystemLife);
            this.tabPage1.Controls.Add(this.label37);
            this.tabPage1.Controls.Add(this.ee_NumPart);
            this.tabPage1.Controls.Add(this.ee_ColorMaxA);
            this.tabPage1.Controls.Add(this.ee_ColorMinA);
            this.tabPage1.Controls.Add(this.ee_ColorA);
            this.tabPage1.Controls.Add(this.ee_ColorMaxB);
            this.tabPage1.Controls.Add(this.ee_ColorMaxG);
            this.tabPage1.Controls.Add(this.label34);
            this.tabPage1.Controls.Add(this.ee_ColorMaxR);
            this.tabPage1.Controls.Add(this.ee_ColorMinB);
            this.tabPage1.Controls.Add(this.ee_ColorMinG);
            this.tabPage1.Controls.Add(this.label35);
            this.tabPage1.Controls.Add(this.ee_ColorMinR);
            this.tabPage1.Controls.Add(this.ee_ColorB);
            this.tabPage1.Controls.Add(this.ee_ColorG);
            this.tabPage1.Controls.Add(this.label36);
            this.tabPage1.Controls.Add(this.ee_ColorR);
            this.tabPage1.Controls.Add(this.ee_AccMaxZ);
            this.tabPage1.Controls.Add(this.ee_AccMaxY);
            this.tabPage1.Controls.Add(this.label31);
            this.tabPage1.Controls.Add(this.ee_AccMaxX);
            this.tabPage1.Controls.Add(this.ee_AccMinZ);
            this.tabPage1.Controls.Add(this.ee_AccMinY);
            this.tabPage1.Controls.Add(this.label32);
            this.tabPage1.Controls.Add(this.ee_AccMinX);
            this.tabPage1.Controls.Add(this.ee_AccZ);
            this.tabPage1.Controls.Add(this.ee_AccY);
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.ee_AccX);
            this.tabPage1.Controls.Add(this.ee_DirMaxZ);
            this.tabPage1.Controls.Add(this.ee_DirMaxY);
            this.tabPage1.Controls.Add(this.label28);
            this.tabPage1.Controls.Add(this.ee_DirMaxX);
            this.tabPage1.Controls.Add(this.ee_DirMinZ);
            this.tabPage1.Controls.Add(this.ee_DirMinY);
            this.tabPage1.Controls.Add(this.label29);
            this.tabPage1.Controls.Add(this.ee_DirMinX);
            this.tabPage1.Controls.Add(this.ee_DirZ);
            this.tabPage1.Controls.Add(this.ee_DirY);
            this.tabPage1.Controls.Add(this.label30);
            this.tabPage1.Controls.Add(this.ee_DirX);
            this.tabPage1.Controls.Add(this.ee_PosMaxZ);
            this.tabPage1.Controls.Add(this.ee_PosMaxY);
            this.tabPage1.Controls.Add(this.label27);
            this.tabPage1.Controls.Add(this.ee_PosMaxX);
            this.tabPage1.Controls.Add(this.ee_PosMinZ);
            this.tabPage1.Controls.Add(this.ee_PosMinY);
            this.tabPage1.Controls.Add(this.label26);
            this.tabPage1.Controls.Add(this.ee_PosMinX);
            this.tabPage1.Controls.Add(this.ee_PosZ);
            this.tabPage1.Controls.Add(this.ee_PosY);
            this.tabPage1.Controls.Add(this.label25);
            this.tabPage1.Controls.Add(this.ee_posX);
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.ee_render);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.ee_type);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.ee_texture);
            this.tabPage1.Controls.Add(this.reloadEffectsButton);
            this.tabPage1.Controls.Add(this.editEffectsButton);
            this.tabPage1.Controls.Add(this.editEffectsList);
            this.tabPage1.Controls.Add(this.saveEffectsButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1092, 100);
            this.tabPage1.TabIndex = 6;
            this.tabPage1.Text = "EditEffects";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // effectDuplicateButton
            // 
            this.effectDuplicateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.effectDuplicateButton.Location = new System.Drawing.Point(136, 42);
            this.effectDuplicateButton.Margin = new System.Windows.Forms.Padding(2);
            this.effectDuplicateButton.Name = "effectDuplicateButton";
            this.effectDuplicateButton.Size = new System.Drawing.Size(60, 28);
            this.effectDuplicateButton.TabIndex = 300;
            this.effectDuplicateButton.Text = "Duplicate";
            this.effectDuplicateButton.UseVisualStyleBackColor = true;
            this.effectDuplicateButton.Click += new System.EventHandler(this.effectDuplicateButton_Click);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(905, 83);
            this.label47.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(48, 13);
            this.label47.TabIndex = 299;
            this.label47.Text = "FadeOut";
            // 
            // ee_FadeOut
            // 
            this.ee_FadeOut.Location = new System.Drawing.Point(959, 80);
            this.ee_FadeOut.Margin = new System.Windows.Forms.Padding(2);
            this.ee_FadeOut.Name = "ee_FadeOut";
            this.ee_FadeOut.Size = new System.Drawing.Size(30, 20);
            this.ee_FadeOut.TabIndex = 298;
            this.ee_FadeOut.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(905, 64);
            this.label48.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(40, 13);
            this.label48.TabIndex = 297;
            this.label48.Text = "FadeIn";
            // 
            // ee_FadeIn
            // 
            this.ee_FadeIn.Location = new System.Drawing.Point(959, 61);
            this.ee_FadeIn.Margin = new System.Windows.Forms.Padding(2);
            this.ee_FadeIn.Name = "ee_FadeIn";
            this.ee_FadeIn.Size = new System.Drawing.Size(30, 20);
            this.ee_FadeIn.TabIndex = 296;
            this.ee_FadeIn.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(823, 83);
            this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(24, 13);
            this.label45.TabIndex = 295;
            this.label45.Text = "Life";
            // 
            // ee_Life
            // 
            this.ee_Life.Location = new System.Drawing.Point(869, 80);
            this.ee_Life.Margin = new System.Windows.Forms.Padding(2);
            this.ee_Life.Name = "ee_Life";
            this.ee_Life.Size = new System.Drawing.Size(30, 20);
            this.ee_Life.TabIndex = 294;
            this.ee_Life.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(823, 64);
            this.label46.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(46, 13);
            this.label46.TabIndex = 293;
            this.label46.Text = "SizeEnd";
            // 
            // ee_SizeEnd
            // 
            this.ee_SizeEnd.Location = new System.Drawing.Point(869, 61);
            this.ee_SizeEnd.Margin = new System.Windows.Forms.Padding(2);
            this.ee_SizeEnd.Name = "ee_SizeEnd";
            this.ee_SizeEnd.Size = new System.Drawing.Size(30, 20);
            this.ee_SizeEnd.TabIndex = 292;
            this.ee_SizeEnd.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(738, 83);
            this.label43.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(38, 13);
            this.label43.TabIndex = 291;
            this.label43.Text = "SizeIni";
            // 
            // ee_SizeIni
            // 
            this.ee_SizeIni.Location = new System.Drawing.Point(784, 80);
            this.ee_SizeIni.Margin = new System.Windows.Forms.Padding(2);
            this.ee_SizeIni.Name = "ee_SizeIni";
            this.ee_SizeIni.Size = new System.Drawing.Size(30, 20);
            this.ee_SizeIni.TabIndex = 290;
            this.ee_SizeIni.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(738, 64);
            this.label44.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(25, 13);
            this.label44.TabIndex = 289;
            this.label44.Text = "size";
            // 
            // ee_size
            // 
            this.ee_size.Location = new System.Drawing.Point(784, 61);
            this.ee_size.Margin = new System.Windows.Forms.Padding(2);
            this.ee_size.Name = "ee_size";
            this.ee_size.Size = new System.Drawing.Size(30, 20);
            this.ee_size.TabIndex = 288;
            this.ee_size.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(627, 83);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(59, 13);
            this.label41.TabIndex = 287;
            this.label41.Text = "RotSpeVar";
            // 
            // ee_RotSpeedVar
            // 
            this.ee_RotSpeedVar.Location = new System.Drawing.Point(697, 80);
            this.ee_RotSpeedVar.Margin = new System.Windows.Forms.Padding(2);
            this.ee_RotSpeedVar.Name = "ee_RotSpeedVar";
            this.ee_RotSpeedVar.Size = new System.Drawing.Size(30, 20);
            this.ee_RotSpeedVar.TabIndex = 286;
            this.ee_RotSpeedVar.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(627, 64);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(43, 13);
            this.label42.TabIndex = 285;
            this.label42.Text = "RotSpe";
            // 
            // ee_RotSpeed
            // 
            this.ee_RotSpeed.Location = new System.Drawing.Point(697, 61);
            this.ee_RotSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.ee_RotSpeed.Name = "ee_RotSpeed";
            this.ee_RotSpeed.Size = new System.Drawing.Size(30, 20);
            this.ee_RotSpeed.TabIndex = 284;
            this.ee_RotSpeed.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(547, 83);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(40, 13);
            this.label39.TabIndex = 283;
            this.label39.Text = "RotVar";
            // 
            // ee_RotVar
            // 
            this.ee_RotVar.Location = new System.Drawing.Point(593, 80);
            this.ee_RotVar.Margin = new System.Windows.Forms.Padding(2);
            this.ee_RotVar.Name = "ee_RotVar";
            this.ee_RotVar.Size = new System.Drawing.Size(30, 20);
            this.ee_RotVar.TabIndex = 282;
            this.ee_RotVar.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(547, 64);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(24, 13);
            this.label40.TabIndex = 281;
            this.label40.Text = "Rot";
            // 
            // ee_Rot
            // 
            this.ee_Rot.Location = new System.Drawing.Point(593, 61);
            this.ee_Rot.Margin = new System.Windows.Forms.Padding(2);
            this.ee_Rot.Name = "ee_Rot";
            this.ee_Rot.Size = new System.Drawing.Size(30, 20);
            this.ee_Rot.TabIndex = 280;
            this.ee_Rot.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(467, 83);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(41, 13);
            this.label38.TabIndex = 279;
            this.label38.Text = "SysLife";
            // 
            // ee_SystemLife
            // 
            this.ee_SystemLife.Location = new System.Drawing.Point(513, 80);
            this.ee_SystemLife.Margin = new System.Windows.Forms.Padding(2);
            this.ee_SystemLife.Name = "ee_SystemLife";
            this.ee_SystemLife.Size = new System.Drawing.Size(30, 20);
            this.ee_SystemLife.TabIndex = 278;
            this.ee_SystemLife.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(467, 64);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(40, 13);
            this.label37.TabIndex = 277;
            this.label37.Text = "NoPart";
            // 
            // ee_NumPart
            // 
            this.ee_NumPart.Location = new System.Drawing.Point(513, 61);
            this.ee_NumPart.Margin = new System.Windows.Forms.Padding(2);
            this.ee_NumPart.Name = "ee_NumPart";
            this.ee_NumPart.Size = new System.Drawing.Size(30, 20);
            this.ee_NumPart.TabIndex = 276;
            this.ee_NumPart.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_ColorMaxA
            // 
            this.ee_ColorMaxA.Location = new System.Drawing.Point(1059, 42);
            this.ee_ColorMaxA.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorMaxA.Name = "ee_ColorMaxA";
            this.ee_ColorMaxA.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorMaxA.TabIndex = 275;
            this.ee_ColorMaxA.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_ColorMinA
            // 
            this.ee_ColorMinA.Location = new System.Drawing.Point(1059, 22);
            this.ee_ColorMinA.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorMinA.Name = "ee_ColorMinA";
            this.ee_ColorMinA.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorMinA.TabIndex = 274;
            this.ee_ColorMinA.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_ColorA
            // 
            this.ee_ColorA.Location = new System.Drawing.Point(1059, 2);
            this.ee_ColorA.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorA.Name = "ee_ColorA";
            this.ee_ColorA.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorA.TabIndex = 273;
            this.ee_ColorA.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_ColorMaxB
            // 
            this.ee_ColorMaxB.Location = new System.Drawing.Point(1027, 42);
            this.ee_ColorMaxB.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorMaxB.Name = "ee_ColorMaxB";
            this.ee_ColorMaxB.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorMaxB.TabIndex = 272;
            this.ee_ColorMaxB.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_ColorMaxG
            // 
            this.ee_ColorMaxG.Location = new System.Drawing.Point(993, 42);
            this.ee_ColorMaxG.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorMaxG.Name = "ee_ColorMaxG";
            this.ee_ColorMaxG.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorMaxG.TabIndex = 271;
            this.ee_ColorMaxG.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(905, 44);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(51, 13);
            this.label34.TabIndex = 270;
            this.label34.Text = "ColorMax";
            // 
            // ee_ColorMaxR
            // 
            this.ee_ColorMaxR.Location = new System.Drawing.Point(959, 42);
            this.ee_ColorMaxR.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorMaxR.Name = "ee_ColorMaxR";
            this.ee_ColorMaxR.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorMaxR.TabIndex = 269;
            this.ee_ColorMaxR.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_ColorMinB
            // 
            this.ee_ColorMinB.Location = new System.Drawing.Point(1027, 22);
            this.ee_ColorMinB.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorMinB.Name = "ee_ColorMinB";
            this.ee_ColorMinB.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorMinB.TabIndex = 268;
            this.ee_ColorMinB.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_ColorMinG
            // 
            this.ee_ColorMinG.Location = new System.Drawing.Point(993, 22);
            this.ee_ColorMinG.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorMinG.Name = "ee_ColorMinG";
            this.ee_ColorMinG.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorMinG.TabIndex = 267;
            this.ee_ColorMinG.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(905, 24);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(48, 13);
            this.label35.TabIndex = 266;
            this.label35.Text = "ColorMin";
            // 
            // ee_ColorMinR
            // 
            this.ee_ColorMinR.Location = new System.Drawing.Point(959, 22);
            this.ee_ColorMinR.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorMinR.Name = "ee_ColorMinR";
            this.ee_ColorMinR.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorMinR.TabIndex = 265;
            // 
            // ee_ColorB
            // 
            this.ee_ColorB.Location = new System.Drawing.Point(1027, 2);
            this.ee_ColorB.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorB.Name = "ee_ColorB";
            this.ee_ColorB.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorB.TabIndex = 264;
            this.ee_ColorB.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_ColorG
            // 
            this.ee_ColorG.Location = new System.Drawing.Point(993, 2);
            this.ee_ColorG.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorG.Name = "ee_ColorG";
            this.ee_ColorG.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorG.TabIndex = 263;
            this.ee_ColorG.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(905, 4);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(31, 13);
            this.label36.TabIndex = 262;
            this.label36.Text = "Color";
            // 
            // ee_ColorR
            // 
            this.ee_ColorR.Location = new System.Drawing.Point(959, 2);
            this.ee_ColorR.Margin = new System.Windows.Forms.Padding(2);
            this.ee_ColorR.Name = "ee_ColorR";
            this.ee_ColorR.Size = new System.Drawing.Size(30, 20);
            this.ee_ColorR.TabIndex = 261;
            // 
            // ee_AccMaxZ
            // 
            this.ee_AccMaxZ.Location = new System.Drawing.Point(869, 42);
            this.ee_AccMaxZ.Margin = new System.Windows.Forms.Padding(2);
            this.ee_AccMaxZ.Name = "ee_AccMaxZ";
            this.ee_AccMaxZ.Size = new System.Drawing.Size(30, 20);
            this.ee_AccMaxZ.TabIndex = 260;
            this.ee_AccMaxZ.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_AccMaxY
            // 
            this.ee_AccMaxY.Location = new System.Drawing.Point(835, 42);
            this.ee_AccMaxY.Margin = new System.Windows.Forms.Padding(2);
            this.ee_AccMaxY.Name = "ee_AccMaxY";
            this.ee_AccMaxY.Size = new System.Drawing.Size(30, 20);
            this.ee_AccMaxY.TabIndex = 259;
            this.ee_AccMaxY.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(755, 44);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(46, 13);
            this.label31.TabIndex = 258;
            this.label31.Text = "AccMax";
            // 
            // ee_AccMaxX
            // 
            this.ee_AccMaxX.Location = new System.Drawing.Point(801, 42);
            this.ee_AccMaxX.Margin = new System.Windows.Forms.Padding(2);
            this.ee_AccMaxX.Name = "ee_AccMaxX";
            this.ee_AccMaxX.Size = new System.Drawing.Size(30, 20);
            this.ee_AccMaxX.TabIndex = 257;
            this.ee_AccMaxX.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_AccMinZ
            // 
            this.ee_AccMinZ.Location = new System.Drawing.Point(869, 22);
            this.ee_AccMinZ.Margin = new System.Windows.Forms.Padding(2);
            this.ee_AccMinZ.Name = "ee_AccMinZ";
            this.ee_AccMinZ.Size = new System.Drawing.Size(30, 20);
            this.ee_AccMinZ.TabIndex = 256;
            this.ee_AccMinZ.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_AccMinY
            // 
            this.ee_AccMinY.Location = new System.Drawing.Point(835, 22);
            this.ee_AccMinY.Margin = new System.Windows.Forms.Padding(2);
            this.ee_AccMinY.Name = "ee_AccMinY";
            this.ee_AccMinY.Size = new System.Drawing.Size(30, 20);
            this.ee_AccMinY.TabIndex = 255;
            this.ee_AccMinY.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(755, 24);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(43, 13);
            this.label32.TabIndex = 254;
            this.label32.Text = "AccMin";
            // 
            // ee_AccMinX
            // 
            this.ee_AccMinX.Location = new System.Drawing.Point(801, 22);
            this.ee_AccMinX.Margin = new System.Windows.Forms.Padding(2);
            this.ee_AccMinX.Name = "ee_AccMinX";
            this.ee_AccMinX.Size = new System.Drawing.Size(30, 20);
            this.ee_AccMinX.TabIndex = 253;
            this.ee_AccMinX.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_AccZ
            // 
            this.ee_AccZ.Location = new System.Drawing.Point(869, 2);
            this.ee_AccZ.Margin = new System.Windows.Forms.Padding(2);
            this.ee_AccZ.Name = "ee_AccZ";
            this.ee_AccZ.Size = new System.Drawing.Size(30, 20);
            this.ee_AccZ.TabIndex = 252;
            this.ee_AccZ.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_AccY
            // 
            this.ee_AccY.Location = new System.Drawing.Point(835, 2);
            this.ee_AccY.Margin = new System.Windows.Forms.Padding(2);
            this.ee_AccY.Name = "ee_AccY";
            this.ee_AccY.Size = new System.Drawing.Size(30, 20);
            this.ee_AccY.TabIndex = 251;
            this.ee_AccY.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(755, 4);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(26, 13);
            this.label33.TabIndex = 250;
            this.label33.Text = "Acc";
            // 
            // ee_AccX
            // 
            this.ee_AccX.Location = new System.Drawing.Point(801, 2);
            this.ee_AccX.Margin = new System.Windows.Forms.Padding(2);
            this.ee_AccX.Name = "ee_AccX";
            this.ee_AccX.Size = new System.Drawing.Size(30, 20);
            this.ee_AccX.TabIndex = 249;
            this.ee_AccX.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_DirMaxZ
            // 
            this.ee_DirMaxZ.Location = new System.Drawing.Point(721, 42);
            this.ee_DirMaxZ.Margin = new System.Windows.Forms.Padding(2);
            this.ee_DirMaxZ.Name = "ee_DirMaxZ";
            this.ee_DirMaxZ.Size = new System.Drawing.Size(30, 20);
            this.ee_DirMaxZ.TabIndex = 248;
            this.ee_DirMaxZ.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_DirMaxY
            // 
            this.ee_DirMaxY.Location = new System.Drawing.Point(687, 42);
            this.ee_DirMaxY.Margin = new System.Windows.Forms.Padding(2);
            this.ee_DirMaxY.Name = "ee_DirMaxY";
            this.ee_DirMaxY.Size = new System.Drawing.Size(30, 20);
            this.ee_DirMaxY.TabIndex = 247;
            this.ee_DirMaxY.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(613, 44);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(40, 13);
            this.label28.TabIndex = 246;
            this.label28.Text = "DirMax";
            // 
            // ee_DirMaxX
            // 
            this.ee_DirMaxX.Location = new System.Drawing.Point(653, 42);
            this.ee_DirMaxX.Margin = new System.Windows.Forms.Padding(2);
            this.ee_DirMaxX.Name = "ee_DirMaxX";
            this.ee_DirMaxX.Size = new System.Drawing.Size(30, 20);
            this.ee_DirMaxX.TabIndex = 245;
            this.ee_DirMaxX.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_DirMinZ
            // 
            this.ee_DirMinZ.Location = new System.Drawing.Point(721, 22);
            this.ee_DirMinZ.Margin = new System.Windows.Forms.Padding(2);
            this.ee_DirMinZ.Name = "ee_DirMinZ";
            this.ee_DirMinZ.Size = new System.Drawing.Size(30, 20);
            this.ee_DirMinZ.TabIndex = 244;
            this.ee_DirMinZ.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_DirMinY
            // 
            this.ee_DirMinY.Location = new System.Drawing.Point(687, 22);
            this.ee_DirMinY.Margin = new System.Windows.Forms.Padding(2);
            this.ee_DirMinY.Name = "ee_DirMinY";
            this.ee_DirMinY.Size = new System.Drawing.Size(30, 20);
            this.ee_DirMinY.TabIndex = 243;
            this.ee_DirMinY.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(613, 25);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(37, 13);
            this.label29.TabIndex = 242;
            this.label29.Text = "DirMin";
            // 
            // ee_DirMinX
            // 
            this.ee_DirMinX.Location = new System.Drawing.Point(653, 22);
            this.ee_DirMinX.Margin = new System.Windows.Forms.Padding(2);
            this.ee_DirMinX.Name = "ee_DirMinX";
            this.ee_DirMinX.Size = new System.Drawing.Size(30, 20);
            this.ee_DirMinX.TabIndex = 241;
            this.ee_DirMinX.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_DirZ
            // 
            this.ee_DirZ.Location = new System.Drawing.Point(721, 2);
            this.ee_DirZ.Margin = new System.Windows.Forms.Padding(2);
            this.ee_DirZ.Name = "ee_DirZ";
            this.ee_DirZ.Size = new System.Drawing.Size(30, 20);
            this.ee_DirZ.TabIndex = 240;
            this.ee_DirZ.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_DirY
            // 
            this.ee_DirY.Location = new System.Drawing.Point(687, 2);
            this.ee_DirY.Margin = new System.Windows.Forms.Padding(2);
            this.ee_DirY.Name = "ee_DirY";
            this.ee_DirY.Size = new System.Drawing.Size(30, 20);
            this.ee_DirY.TabIndex = 239;
            this.ee_DirY.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(613, 5);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(20, 13);
            this.label30.TabIndex = 238;
            this.label30.Text = "Dir";
            // 
            // ee_DirX
            // 
            this.ee_DirX.Location = new System.Drawing.Point(653, 2);
            this.ee_DirX.Margin = new System.Windows.Forms.Padding(2);
            this.ee_DirX.Name = "ee_DirX";
            this.ee_DirX.Size = new System.Drawing.Size(30, 20);
            this.ee_DirX.TabIndex = 237;
            this.ee_DirX.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_PosMaxZ
            // 
            this.ee_PosMaxZ.Location = new System.Drawing.Point(581, 42);
            this.ee_PosMaxZ.Margin = new System.Windows.Forms.Padding(2);
            this.ee_PosMaxZ.Name = "ee_PosMaxZ";
            this.ee_PosMaxZ.Size = new System.Drawing.Size(30, 20);
            this.ee_PosMaxZ.TabIndex = 236;
            this.ee_PosMaxZ.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_PosMaxY
            // 
            this.ee_PosMaxY.Location = new System.Drawing.Point(547, 42);
            this.ee_PosMaxY.Margin = new System.Windows.Forms.Padding(2);
            this.ee_PosMaxY.Name = "ee_PosMaxY";
            this.ee_PosMaxY.Size = new System.Drawing.Size(30, 20);
            this.ee_PosMaxY.TabIndex = 235;
            this.ee_PosMaxY.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(467, 44);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(45, 13);
            this.label27.TabIndex = 234;
            this.label27.Text = "PosMax";
            // 
            // ee_PosMaxX
            // 
            this.ee_PosMaxX.Location = new System.Drawing.Point(513, 42);
            this.ee_PosMaxX.Margin = new System.Windows.Forms.Padding(2);
            this.ee_PosMaxX.Name = "ee_PosMaxX";
            this.ee_PosMaxX.Size = new System.Drawing.Size(30, 20);
            this.ee_PosMaxX.TabIndex = 233;
            this.ee_PosMaxX.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_PosMinZ
            // 
            this.ee_PosMinZ.Location = new System.Drawing.Point(581, 22);
            this.ee_PosMinZ.Margin = new System.Windows.Forms.Padding(2);
            this.ee_PosMinZ.Name = "ee_PosMinZ";
            this.ee_PosMinZ.Size = new System.Drawing.Size(30, 20);
            this.ee_PosMinZ.TabIndex = 232;
            this.ee_PosMinZ.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_PosMinY
            // 
            this.ee_PosMinY.Location = new System.Drawing.Point(547, 22);
            this.ee_PosMinY.Margin = new System.Windows.Forms.Padding(2);
            this.ee_PosMinY.Name = "ee_PosMinY";
            this.ee_PosMinY.Size = new System.Drawing.Size(30, 20);
            this.ee_PosMinY.TabIndex = 231;
            this.ee_PosMinY.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(467, 24);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(42, 13);
            this.label26.TabIndex = 230;
            this.label26.Text = "PosMin";
            // 
            // ee_PosMinX
            // 
            this.ee_PosMinX.Location = new System.Drawing.Point(513, 22);
            this.ee_PosMinX.Margin = new System.Windows.Forms.Padding(2);
            this.ee_PosMinX.Name = "ee_PosMinX";
            this.ee_PosMinX.Size = new System.Drawing.Size(30, 20);
            this.ee_PosMinX.TabIndex = 229;
            this.ee_PosMinX.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_PosZ
            // 
            this.ee_PosZ.Location = new System.Drawing.Point(581, 2);
            this.ee_PosZ.Margin = new System.Windows.Forms.Padding(2);
            this.ee_PosZ.Name = "ee_PosZ";
            this.ee_PosZ.Size = new System.Drawing.Size(30, 20);
            this.ee_PosZ.TabIndex = 228;
            this.ee_PosZ.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // ee_PosY
            // 
            this.ee_PosY.Location = new System.Drawing.Point(547, 2);
            this.ee_PosY.Margin = new System.Windows.Forms.Padding(2);
            this.ee_PosY.Name = "ee_PosY";
            this.ee_PosY.Size = new System.Drawing.Size(30, 20);
            this.ee_PosY.TabIndex = 227;
            this.ee_PosY.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(467, 4);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(44, 13);
            this.label25.TabIndex = 226;
            this.label25.Text = "Position";
            // 
            // ee_posX
            // 
            this.ee_posX.Location = new System.Drawing.Point(513, 2);
            this.ee_posX.Margin = new System.Windows.Forms.Padding(2);
            this.ee_posX.Name = "ee_posX";
            this.ee_posX.Size = new System.Drawing.Size(30, 20);
            this.ee_posX.TabIndex = 225;
            this.ee_posX.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(261, 52);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(42, 13);
            this.label24.TabIndex = 224;
            this.label24.Text = "Render";
            // 
            // ee_render
            // 
            this.ee_render.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ee_render.CausesValidation = false;
            this.ee_render.FormattingEnabled = true;
            this.ee_render.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ee_render.Items.AddRange(new object[] {
            "normal",
            "additive"});
            this.ee_render.Location = new System.Drawing.Point(310, 49);
            this.ee_render.Name = "ee_render";
            this.ee_render.Size = new System.Drawing.Size(149, 21);
            this.ee_render.TabIndex = 223;
            this.ee_render.TabStop = false;
            this.ee_render.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(261, 30);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(31, 13);
            this.label23.TabIndex = 222;
            this.label23.Text = "Type";
            // 
            // ee_type
            // 
            this.ee_type.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ee_type.CausesValidation = false;
            this.ee_type.FormattingEnabled = true;
            this.ee_type.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ee_type.Items.AddRange(new object[] {
            "burst",
            "fountain"});
            this.ee_type.Location = new System.Drawing.Point(310, 27);
            this.ee_type.Name = "ee_type";
            this.ee_type.Size = new System.Drawing.Size(149, 21);
            this.ee_type.TabIndex = 221;
            this.ee_type.TabStop = false;
            this.ee_type.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(261, 8);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 220;
            this.label9.Text = "Texture";
            // 
            // ee_texture
            // 
            this.ee_texture.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ee_texture.CausesValidation = false;
            this.ee_texture.FormattingEnabled = true;
            this.ee_texture.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ee_texture.Location = new System.Drawing.Point(310, 5);
            this.ee_texture.Name = "ee_texture";
            this.ee_texture.Size = new System.Drawing.Size(149, 21);
            this.ee_texture.TabIndex = 219;
            this.ee_texture.TabStop = false;
            this.ee_texture.Leave += new System.EventHandler(this.editEffects_Leave);
            // 
            // reloadEffectsButton
            // 
            this.reloadEffectsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reloadEffectsButton.Location = new System.Drawing.Point(72, 42);
            this.reloadEffectsButton.Margin = new System.Windows.Forms.Padding(2);
            this.reloadEffectsButton.Name = "reloadEffectsButton";
            this.reloadEffectsButton.Size = new System.Drawing.Size(60, 28);
            this.reloadEffectsButton.TabIndex = 218;
            this.reloadEffectsButton.Text = "Reload";
            this.reloadEffectsButton.UseVisualStyleBackColor = true;
            this.reloadEffectsButton.Click += new System.EventHandler(this.reloadEffectsButton_Click);
            // 
            // editEffectsButton
            // 
            this.editEffectsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editEffectsButton.Location = new System.Drawing.Point(7, 10);
            this.editEffectsButton.Margin = new System.Windows.Forms.Padding(2);
            this.editEffectsButton.Name = "editEffectsButton";
            this.editEffectsButton.Size = new System.Drawing.Size(60, 28);
            this.editEffectsButton.TabIndex = 217;
            this.editEffectsButton.Text = "Edit Effects";
            this.editEffectsButton.UseVisualStyleBackColor = true;
            this.editEffectsButton.Click += new System.EventHandler(this.button_Click);
            // 
            // editEffectsList
            // 
            this.editEffectsList.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.editEffectsList.CausesValidation = false;
            this.editEffectsList.FormattingEnabled = true;
            this.editEffectsList.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.editEffectsList.Location = new System.Drawing.Point(72, 13);
            this.editEffectsList.Name = "editEffectsList";
            this.editEffectsList.Size = new System.Drawing.Size(183, 21);
            this.editEffectsList.TabIndex = 216;
            this.editEffectsList.TabStop = false;
            this.editEffectsList.SelectedIndexChanged += new System.EventHandler(this.editEffectsList_SelectedIndexChanged);
            // 
            // saveEffectsButton
            // 
            this.saveEffectsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveEffectsButton.Location = new System.Drawing.Point(7, 42);
            this.saveEffectsButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveEffectsButton.Name = "saveEffectsButton";
            this.saveEffectsButton.Size = new System.Drawing.Size(60, 28);
            this.saveEffectsButton.TabIndex = 213;
            this.saveEffectsButton.Text = "Save Effects";
            this.saveEffectsButton.UseVisualStyleBackColor = true;
            this.saveEffectsButton.Click += new System.EventHandler(this.saveEffectsButton_Click);
            // 
            // myEditorControl
            // 
            this.myEditorControl.Location = new System.Drawing.Point(0, 129);
            this.myEditorControl.Margin = new System.Windows.Forms.Padding(2);
            this.myEditorControl.Name = "myEditorControl";
            this.myEditorControl.Size = new System.Drawing.Size(1280, 720);
            this.myEditorControl.TabIndex = 2;
            this.myEditorControl.Text = "myEditorControl";
            // 
            // editEnemyZoneButton
            // 
            this.editEnemyZoneButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editEnemyZoneButton.Location = new System.Drawing.Point(7, 42);
            this.editEnemyZoneButton.Margin = new System.Windows.Forms.Padding(2);
            this.editEnemyZoneButton.Name = "editEnemyZoneButton";
            this.editEnemyZoneButton.Size = new System.Drawing.Size(60, 28);
            this.editEnemyZoneButton.TabIndex = 215;
            this.editEnemyZoneButton.Text = "Edit Enemy Zone";
            this.editEnemyZoneButton.UseVisualStyleBackColor = true;
            this.editEnemyZoneButton.Click += new System.EventHandler(this.button_Click);
            // 
            // MyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1284, 873);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonNewLevel);
            this.Controls.Add(this.superPizzaButton);
            this.Controls.Add(this.BGColorButton);
            this.Controls.Add(this.buttonImportLevel);
            this.Controls.Add(this.buttonSaveLevel);
            this.Controls.Add(this.buttonLoadLevel);
            this.Controls.Add(this.myEditorControl);
            this.Name = "MyEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyEditor";
            this.tabControl1.ResumeLayout(false);
            this.entities.ResumeLayout(false);
            this.entities.PerformLayout();
            this.staticPropertiesPanel.ResumeLayout(false);
            this.staticPropertiesPanel.PerformLayout();
            this.cameras.ResumeLayout(false);
            this.cameraNodePanel.ResumeLayout(false);
            this.cameraNodePanel.PerformLayout();
            this.colisions.ResumeLayout(false);
            this.enemyZones.ResumeLayout(false);
            this.enemyZones.PerformLayout();
            this.triggers.ResumeLayout(false);
            this.effects.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public MyEditorControl myEditorControl;
        private System.Windows.Forms.Button buttonImportLevel;
        private System.Windows.Forms.Button buttonSaveLevel;
        private System.Windows.Forms.Button buttonLoadLevel;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button BGColorButton;
        private System.Windows.Forms.Button superPizzaButton;
        private System.Windows.Forms.Button buttonNewLevel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage entities;
        public System.Windows.Forms.Panel staticPropertiesPanel;
        public System.Windows.Forms.CheckBox flipVerticalCheck;
        public System.Windows.Forms.CheckBox flipHorizontalCheck;
        private System.Windows.Forms.Button colorButton;
        public System.Windows.Forms.TextBox colorB;
        public System.Windows.Forms.TextBox colorG;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox colorA;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox colorR;
        private System.Windows.Forms.Button buttonResetPosition;
        private System.Windows.Forms.Button buttonResetScale;
        private System.Windows.Forms.Button buttonResetRotation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textScaleY;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textScaleX;
        public System.Windows.Forms.TextBox textRotZ;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textPosZ;
        public System.Windows.Forms.TextBox textPosY;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textPosX;
        public System.Windows.Forms.ComboBox texturesCombo;
        public System.Windows.Forms.CheckBox selectGroup;
        public System.Windows.Forms.CheckBox canSelectEnemy;
        public System.Windows.Forms.CheckBox canSelectAnimated;
        private System.Windows.Forms.Button buttonAddAnimated;
        public System.Windows.Forms.CheckBox canSelectStatic;
        private System.Windows.Forms.Button buttonAddStatic;
        private System.Windows.Forms.Button buttonCreateGroup;
        private System.Windows.Forms.Button buttonAddEnemy;
        private System.Windows.Forms.Button buttonScale;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.TabPage cameras;
        private System.Windows.Forms.Button buttonMoveCameraNode;
        private System.Windows.Forms.Button addCameraNodeButton;
        private System.Windows.Forms.Button addDefaultCamerasButton;
        public System.Windows.Forms.Panel cameraNodePanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSetupCamera;
        private System.Windows.Forms.Button linkNodeButton;
        public System.Windows.Forms.CheckBox isFirstCheck;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox cameraNodeSpeed;
        public System.Windows.Forms.TextBox cameraPosZ;
        public System.Windows.Forms.TextBox cameraPosY;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox cameraPosX;
        private System.Windows.Forms.TabPage colisions;
        private System.Windows.Forms.Button buttonEditColisions;
        private System.Windows.Forms.Button buttonAddColisions;
        private System.Windows.Forms.TabPage enemyZones;
        private System.Windows.Forms.TabPage triggers;
        private System.Windows.Forms.Button addEnemyZoneButton;
        public System.Windows.Forms.ComboBox enemiesCombo;
        public System.Windows.Forms.TextBox enemyCount;
        private System.Windows.Forms.TabPage effects;
        private System.Windows.Forms.Button addTriggerButton;
        public System.Windows.Forms.ComboBox availableConditions;
        private System.Windows.Forms.Button editTriggerButton;
        public System.Windows.Forms.ComboBox availableConsecuences;
        private System.Windows.Forms.Button addEffectButton;
        public System.Windows.Forms.ComboBox effectsCombo;
        private System.Windows.Forms.Button editEffectButton;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox effectColorB;
        public System.Windows.Forms.TextBox effectColorG;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox effectColorR;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox effectScale;
        public System.Windows.Forms.TextBox effectDirectionX;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.TextBox effectPositionZ;
        public System.Windows.Forms.TextBox effectPositionY;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.TextBox effectPositionX;
        public System.Windows.Forms.TextBox effectDirectionZ;
        public System.Windows.Forms.TextBox effectDirectionY;
        private System.Windows.Forms.Label label22;
        public System.Windows.Forms.TextBox effectLifetime;
        private System.Windows.Forms.Label NumParticles;
        public System.Windows.Forms.TextBox effectNumParticles;
        private System.Windows.Forms.Button addConsecuenceButton;
        private System.Windows.Forms.Button addConditionButton;
        public System.Windows.Forms.ComboBox triggerConsecuences;
        public System.Windows.Forms.ComboBox triggerConditions;
        private System.Windows.Forms.Button removeConsecuenceButton;
        private System.Windows.Forms.Button removeConditionButton;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button saveEffectsButton;
        private System.Windows.Forms.Button reloadEffectsButton;
        private System.Windows.Forms.Button editEffectsButton;
        public System.Windows.Forms.ComboBox editEffectsList;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.ComboBox ee_type;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox ee_texture;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.ComboBox ee_render;
        public System.Windows.Forms.TextBox ee_PosMaxZ;
        public System.Windows.Forms.TextBox ee_PosMaxY;
        private System.Windows.Forms.Label label27;
        public System.Windows.Forms.TextBox ee_PosMaxX;
        public System.Windows.Forms.TextBox ee_PosMinZ;
        public System.Windows.Forms.TextBox ee_PosMinY;
        private System.Windows.Forms.Label label26;
        public System.Windows.Forms.TextBox ee_PosMinX;
        public System.Windows.Forms.TextBox ee_PosZ;
        public System.Windows.Forms.TextBox ee_PosY;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.TextBox ee_posX;
        public System.Windows.Forms.TextBox ee_AccMaxZ;
        public System.Windows.Forms.TextBox ee_AccMaxY;
        private System.Windows.Forms.Label label31;
        public System.Windows.Forms.TextBox ee_AccMaxX;
        public System.Windows.Forms.TextBox ee_AccMinZ;
        public System.Windows.Forms.TextBox ee_AccMinY;
        private System.Windows.Forms.Label label32;
        public System.Windows.Forms.TextBox ee_AccMinX;
        public System.Windows.Forms.TextBox ee_AccZ;
        public System.Windows.Forms.TextBox ee_AccY;
        private System.Windows.Forms.Label label33;
        public System.Windows.Forms.TextBox ee_AccX;
        public System.Windows.Forms.TextBox ee_DirMaxZ;
        public System.Windows.Forms.TextBox ee_DirMaxY;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.TextBox ee_DirMaxX;
        public System.Windows.Forms.TextBox ee_DirMinZ;
        public System.Windows.Forms.TextBox ee_DirMinY;
        private System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox ee_DirMinX;
        public System.Windows.Forms.TextBox ee_DirZ;
        public System.Windows.Forms.TextBox ee_DirY;
        private System.Windows.Forms.Label label30;
        public System.Windows.Forms.TextBox ee_DirX;
        public System.Windows.Forms.TextBox ee_ColorMaxA;
        public System.Windows.Forms.TextBox ee_ColorMinA;
        public System.Windows.Forms.TextBox ee_ColorA;
        public System.Windows.Forms.TextBox ee_ColorMaxB;
        public System.Windows.Forms.TextBox ee_ColorMaxG;
        private System.Windows.Forms.Label label34;
        public System.Windows.Forms.TextBox ee_ColorMaxR;
        public System.Windows.Forms.TextBox ee_ColorMinB;
        public System.Windows.Forms.TextBox ee_ColorMinG;
        private System.Windows.Forms.Label label35;
        public System.Windows.Forms.TextBox ee_ColorMinR;
        public System.Windows.Forms.TextBox ee_ColorB;
        public System.Windows.Forms.TextBox ee_ColorG;
        private System.Windows.Forms.Label label36;
        public System.Windows.Forms.TextBox ee_ColorR;
        private System.Windows.Forms.Label label37;
        public System.Windows.Forms.TextBox ee_NumPart;
        private System.Windows.Forms.Label label47;
        public System.Windows.Forms.TextBox ee_FadeOut;
        private System.Windows.Forms.Label label48;
        public System.Windows.Forms.TextBox ee_FadeIn;
        private System.Windows.Forms.Label label45;
        public System.Windows.Forms.TextBox ee_Life;
        private System.Windows.Forms.Label label46;
        public System.Windows.Forms.TextBox ee_SizeEnd;
        private System.Windows.Forms.Label label43;
        public System.Windows.Forms.TextBox ee_SizeIni;
        private System.Windows.Forms.Label label44;
        public System.Windows.Forms.TextBox ee_size;
        private System.Windows.Forms.Label label41;
        public System.Windows.Forms.TextBox ee_RotSpeedVar;
        private System.Windows.Forms.Label label42;
        public System.Windows.Forms.TextBox ee_RotSpeed;
        private System.Windows.Forms.Label label39;
        public System.Windows.Forms.TextBox ee_RotVar;
        private System.Windows.Forms.Label label40;
        public System.Windows.Forms.TextBox ee_Rot;
        private System.Windows.Forms.Label label38;
        public System.Windows.Forms.TextBox ee_SystemLife;
        private System.Windows.Forms.Button effectDuplicateButton;
        private System.Windows.Forms.Button editEnemyZoneButton;
    }
}
#endif