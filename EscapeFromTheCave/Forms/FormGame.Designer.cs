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
            labelQuestion = new Label();
            lblPath1 = new Label();
            lblPath2 = new Label();
            lblPath3 = new Label();
            lblPath4 = new Label();
            SuspendLayout();
            // 
            // labelQuestion
            // 
            labelQuestion.AutoSize = true;
            labelQuestion.Location = new Point(567, 39);
            labelQuestion.Name = "labelQuestion";
            labelQuestion.Size = new Size(115, 15);
            labelQuestion.TabIndex = 0;
            labelQuestion.Text = "ТУТ БУДЕТ ВОПРОС";
            // 
            // lblPath1
            // 
            lblPath1.AutoSize = true;
            lblPath1.BackColor = Color.Transparent;
            lblPath1.Cursor = Cursors.Hand;
            lblPath1.Font = new Font("Algerian", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPath1.ForeColor = SystemColors.ButtonHighlight;
            lblPath1.Location = new Point(106, 320);
            lblPath1.Name = "lblPath1";
            lblPath1.Size = new Size(118, 30);
            lblPath1.TabIndex = 2;
            lblPath1.Text = "кнопка 1";
            lblPath1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPath2
            // 
            lblPath2.AutoSize = true;
            lblPath2.BackColor = Color.Transparent;
            lblPath2.Cursor = Cursors.Hand;
            lblPath2.Font = new Font("Algerian", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPath2.ForeColor = SystemColors.ButtonHighlight;
            lblPath2.Location = new Point(280, 320);
            lblPath2.Name = "lblPath2";
            lblPath2.Size = new Size(118, 30);
            lblPath2.TabIndex = 3;
            lblPath2.Text = "кнопка 2";
            lblPath2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPath3
            // 
            lblPath3.AutoSize = true;
            lblPath3.BackColor = Color.Transparent;
            lblPath3.Cursor = Cursors.Hand;
            lblPath3.Font = new Font("Algerian", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPath3.ForeColor = SystemColors.ButtonHighlight;
            lblPath3.Location = new Point(426, 320);
            lblPath3.Name = "lblPath3";
            lblPath3.Size = new Size(118, 30);
            lblPath3.TabIndex = 4;
            lblPath3.Text = "кнопка 3";
            lblPath3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPath4
            // 
            lblPath4.AutoSize = true;
            lblPath4.BackColor = Color.Transparent;
            lblPath4.Cursor = Cursors.Hand;
            lblPath4.Font = new Font("Algerian", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPath4.ForeColor = SystemColors.ButtonHighlight;
            lblPath4.Location = new Point(606, 320);
            lblPath4.Name = "lblPath4";
            lblPath4.Size = new Size(118, 30);
            lblPath4.TabIndex = 5;
            lblPath4.Text = "кнопка 4";
            lblPath4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblPath4);
            Controls.Add(lblPath3);
            Controls.Add(lblPath2);
            Controls.Add(lblPath1);
            Controls.Add(labelQuestion);
            Name = "FormGame";
            Text = "Попробуй выберись";
            Load += FormGame_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelQuestion;
        private Label lblPath1;
        private Label lblPath2;
        private Label lblPath3;
        private Label lblPath4;
    }
}