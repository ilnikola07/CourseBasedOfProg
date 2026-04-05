namespace EscapeFromTheCave.Forms
{
    partial class FormGame
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            labelStory = new Label();
            labelNext = new Label();
            timerTypewriter = new System.Windows.Forms.Timer(components);
            timerFadeIn = new System.Windows.Forms.Timer(components);
            labelHow = new Label();
            imageList1 = new ImageList(components);
            labelGetUp = new Label();
            SuspendLayout();
            // 
            // labelStory
            // 
            labelStory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelStory.AutoSize = true;
            labelStory.BackColor = Color.Transparent;
            labelStory.Font = new Font("Algerian", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelStory.ForeColor = SystemColors.ButtonHighlight;
            labelStory.Location = new Point(2, 206);
            labelStory.Name = "labelStory";
            labelStory.Size = new Size(191, 71);
            labelStory.TabIndex = 3;
            labelStory.Text = "Текст";
            labelStory.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelNext
            // 
            labelNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelNext.AutoSize = true;
            labelNext.BackColor = Color.Transparent;
            labelNext.Cursor = Cursors.Hand;
            labelNext.Font = new Font("Algerian", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelNext.ForeColor = SystemColors.ButtonHighlight;
            labelNext.Location = new Point(756, 406);
            labelNext.Name = "labelNext";
            labelNext.Size = new Size(209, 71);
            labelNext.TabIndex = 4;
            labelNext.Text = "Далее";
            labelNext.TextAlign = ContentAlignment.MiddleCenter;
            labelNext.Click += btnNext_Click;
            // 
            // timerTypewriter
            // 
            timerTypewriter.Interval = 50;
            timerTypewriter.Tick += timerTypewriter_Tick;
            // 
            // timerFadeIn
            // 
            timerFadeIn.Enabled = true;
            timerFadeIn.Interval = 3000;
            timerFadeIn.Tick += timerFadeIn_Tick;
            // 
            // labelHow
            // 
            labelHow.AutoSize = true;
            labelHow.BackColor = Color.Transparent;
            labelHow.Font = new Font("Algerian", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelHow.ForeColor = SystemColors.ButtonHighlight;
            labelHow.Location = new Point(2, 9);
            labelHow.Name = "labelHow";
            labelHow.Size = new Size(1274, 71);
            labelHow.TabIndex = 5;
            labelHow.Text = "Backstory. How did you end up here?";
            labelHow.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // labelGetUp
            // 
            labelGetUp.AutoSize = true;
            labelGetUp.BackColor = Color.Transparent;
            labelGetUp.Font = new Font("Algerian", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelGetUp.ForeColor = SystemColors.ButtonHighlight;
            labelGetUp.Location = new Point(372, 406);
            labelGetUp.Name = "labelGetUp";
            labelGetUp.Size = new Size(250, 71);
            labelGetUp.TabIndex = 6;
            labelGetUp.Text = "Get up\r\n";
            labelGetUp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(977, 486);
            Controls.Add(labelGetUp);
            Controls.Add(labelHow);
            Controls.Add(labelNext);
            Controls.Add(labelStory);
            Name = "FormGame";
            Opacity = 0D;
            Text = "Выберись из пещеры";
            Load += FormGame_Load;
            KeyDown += FormGame_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelStory;
        private Label labelNext;
        private System.Windows.Forms.Timer timerTypewriter;
        private System.Windows.Forms.Timer timerFadeIn;
        private Label labelHow;
        private ImageList imageList1;
        private Label labelGetUp;
    }
}