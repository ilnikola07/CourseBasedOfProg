namespace EscapeFromTheCave
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            pictureBoxMain = new PictureBox();
            labelStart = new Label();
            labelName = new Label();
            labelAbout = new Label();
            labelInc = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxMain
            // 
            pictureBoxMain.Dock = DockStyle.Fill;
            pictureBoxMain.Image = (Image)resources.GetObject("pictureBoxMain.Image");
            pictureBoxMain.Location = new Point(0, 0);
            pictureBoxMain.Name = "pictureBoxMain";
            pictureBoxMain.Size = new Size(497, 267);
            pictureBoxMain.TabIndex = 0;
            pictureBoxMain.TabStop = false;
            // 
            // labelStart
            // 
            labelStart.AutoSize = true;
            labelStart.BackColor = Color.Transparent;
            labelStart.Cursor = Cursors.Hand;
            labelStart.Font = new Font("Algerian", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelStart.ForeColor = SystemColors.ButtonHighlight;
            labelStart.Location = new Point(190, 130);
            labelStart.Name = "labelStart";
            labelStart.Size = new Size(104, 30);
            labelStart.TabIndex = 1;
            labelStart.Text = "START ";
            labelStart.TextAlign = ContentAlignment.MiddleCenter;
            labelStart.Click += labelStart_Click;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.BackColor = Color.Transparent;
            labelName.Font = new Font("Algerian", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelName.ForeColor = SystemColors.ButtonHighlight;
            labelName.Location = new Point(27, 9);
            labelName.Name = "labelName";
            labelName.Size = new Size(461, 41);
            labelName.TabIndex = 2;
            labelName.Text = "ESCAPE FROM THE CAVE\r\n";
            labelName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelAbout
            // 
            labelAbout.AutoSize = true;
            labelAbout.BackColor = Color.Transparent;
            labelAbout.Cursor = Cursors.Hand;
            labelAbout.Font = new Font("Algerian", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelAbout.ForeColor = SystemColors.ButtonHighlight;
            labelAbout.Location = new Point(190, 173);
            labelAbout.Name = "labelAbout";
            labelAbout.Size = new Size(99, 30);
            labelAbout.TabIndex = 3;
            labelAbout.Text = "ABOUT";
            labelAbout.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelInc
            // 
            labelInc.AutoSize = true;
            labelInc.BackColor = Color.Transparent;
            labelInc.Font = new Font("Algerian", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelInc.ForeColor = SystemColors.ButtonHighlight;
            labelInc.Location = new Point(0, 243);
            labelInc.Name = "labelInc";
            labelInc.Size = new Size(501, 15);
            labelInc.TabIndex = 4;
            labelInc.Text = "(C) 2026 Ulitka Soft Inc. All rights are unprotected, but stealing is bad\r\n";
            labelInc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 267);
            Controls.Add(labelInc);
            Controls.Add(labelAbout);
            Controls.Add(labelName);
            Controls.Add(labelStart);
            Controls.Add(pictureBoxMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Escape from the cave";
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxMain;
        private Label labelStart;
        private Label labelName;
        private Label labelAbout;
        private Label labelInc;
    }
}
