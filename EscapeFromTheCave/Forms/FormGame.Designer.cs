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
            labelQuestion = new Label();
            buttonPath1 = new Button();
            buttonPath2 = new Button();
            buttonPath3 = new Button();
            buttonPath4 = new Button();
            panelButtons = new Panel();
            labelEnd = new Label();
            labelTime = new Label();
            timerTypewriterQuest = new System.Windows.Forms.Timer(components);
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // labelQuestion
            // 
            labelQuestion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelQuestion.AutoSize = true;
            labelQuestion.BackColor = Color.Transparent;
            labelQuestion.Font = new Font("Algerian", 30F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelQuestion.ForeColor = Color.White;
            labelQuestion.Location = new Point(225, 125);
            labelQuestion.Name = "labelQuestion";
            labelQuestion.Size = new Size(370, 45);
            labelQuestion.TabIndex = 0;
            labelQuestion.Text = "ТУТ БУДЕТ ВОПРОС";
            labelQuestion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonPath1
            // 
            buttonPath1.Anchor = AnchorStyles.Bottom;
            buttonPath1.BackColor = Color.Black;
            buttonPath1.Font = new Font("Algerian", 21.75F);
            buttonPath1.ForeColor = Color.White;
            buttonPath1.Location = new Point(42, 3);
            buttonPath1.Name = "buttonPath1";
            buttonPath1.Size = new Size(343, 35);
            buttonPath1.TabIndex = 5;
            buttonPath1.Text = "1 кн";
            buttonPath1.UseVisualStyleBackColor = false;
            // 
            // buttonPath2
            // 
            buttonPath2.Anchor = AnchorStyles.Bottom;
            buttonPath2.BackColor = Color.Black;
            buttonPath2.Font = new Font("Algerian", 21.75F);
            buttonPath2.ForeColor = Color.White;
            buttonPath2.Location = new Point(416, 3);
            buttonPath2.Name = "buttonPath2";
            buttonPath2.Size = new Size(343, 35);
            buttonPath2.TabIndex = 6;
            buttonPath2.Text = "2 кн";
            buttonPath2.UseVisualStyleBackColor = false;
            // 
            // buttonPath3
            // 
            buttonPath3.Anchor = AnchorStyles.Bottom;
            buttonPath3.BackColor = Color.Black;
            buttonPath3.Font = new Font("Algerian", 21.75F);
            buttonPath3.ForeColor = Color.White;
            buttonPath3.Location = new Point(42, 42);
            buttonPath3.Name = "buttonPath3";
            buttonPath3.Size = new Size(343, 35);
            buttonPath3.TabIndex = 7;
            buttonPath3.Text = "3 кн";
            buttonPath3.UseVisualStyleBackColor = false;
            // 
            // buttonPath4
            // 
            buttonPath4.Anchor = AnchorStyles.Bottom;
            buttonPath4.BackColor = Color.Black;
            buttonPath4.Font = new Font("Algerian", 21.75F);
            buttonPath4.ForeColor = Color.White;
            buttonPath4.Location = new Point(416, 42);
            buttonPath4.Name = "buttonPath4";
            buttonPath4.Size = new Size(343, 35);
            buttonPath4.TabIndex = 8;
            buttonPath4.Text = "4 кн";
            buttonPath4.UseVisualStyleBackColor = false;
            // 
            // panelButtons
            // 
            panelButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelButtons.BackColor = Color.Black;
            panelButtons.Controls.Add(buttonPath1);
            panelButtons.Controls.Add(buttonPath4);
            panelButtons.Controls.Add(buttonPath2);
            panelButtons.Controls.Add(buttonPath3);
            panelButtons.Location = new Point(0, 370);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(800, 80);
            panelButtons.TabIndex = 9;
            // 
            // labelEnd
            // 
            labelEnd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelEnd.AutoSize = true;
            labelEnd.BackColor = Color.Transparent;
            labelEnd.Font = new Font("Algerian", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelEnd.ForeColor = Color.White;
            labelEnd.Location = new Point(276, 187);
            labelEnd.Name = "labelEnd";
            labelEnd.Size = new Size(925, 71);
            labelEnd.TabIndex = 10;
            labelEnd.Text = "ВЫВОД ПОБЕДА ИЛИ СМЕРТЬ";
            labelEnd.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelTime
            // 
            labelTime.AutoSize = true;
            labelTime.BackColor = Color.Transparent;
            labelTime.Font = new Font("Algerian", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTime.ForeColor = Color.White;
            labelTime.Location = new Point(0, 9);
            labelTime.Name = "labelTime";
            labelTime.Size = new Size(276, 54);
            labelTime.TabIndex = 11;
            labelTime.Text = "Your time:";
            // 
            // timerTypewriterQuest
            // 
            timerTypewriterQuest.Interval = 50;
            // 
            // FormGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(labelTime);
            Controls.Add(labelEnd);
            Controls.Add(panelButtons);
            Controls.Add(labelQuestion);
            KeyPreview = true;
            Name = "FormGame";
            Text = "Попробуй выберись";
            //Load += FormGame_Load;
            KeyDown += MainForm_KeyDown;
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Label labelQuestion;
        private Button buttonPath1;
        private Button buttonPath2;
        private Button buttonPath3;
        private Button buttonPath4;
        private Panel panelButtons;
        private Label labelEnd;
        private Label labelTime;
        private System.Windows.Forms.Timer timerTypewriterQuest;
    }
}