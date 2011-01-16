namespace MyGame
{
    partial class NaughtyEditor
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
            this.myEditorControl1 = new MyGame.MyEditorControl();
            this.SuspendLayout();
            // 
            // myEditorControl1
            // 
            this.myEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.myEditorControl1.Name = "myEditorControl1";
            this.myEditorControl1.Size = new System.Drawing.Size(1280, 720);
            this.myEditorControl1.TabIndex = 0;
            this.myEditorControl1.Text = "myEditorControl1";
            // 
            // NaughtyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1272, 693);
            this.Controls.Add(this.myEditorControl1);
            this.Name = "NaughtyEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private MyEditorControl myEditorControl1;
    }
}