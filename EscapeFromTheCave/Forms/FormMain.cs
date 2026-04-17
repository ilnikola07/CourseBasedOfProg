using EscapeFromTheCave.Forms;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace EscapeFromTheCave
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            labelStart.Parent = pictureBoxMain;// Т.к. установить прозрачность на кнопках тяжело, используются label,
            labelName.Parent = pictureBoxMain;// поэтому привязываем label к единому picturebox 
            labelAbout.Parent = pictureBoxMain;// для прозрачности текста в главной форме
            labelInc.Parent = pictureBoxMain;
            labelExit.Parent = pictureBoxMain;
        }

        private void labelStart_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you ready to start the game?", "Rhetorical question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                FormHist game = new FormHist();
                game.FormBorderStyle = FormBorderStyle.None; // Убираем заголовок и границы окна                 
                game.WindowState = FormWindowState.Maximized;// Окно на весь экран                
                game.TopMost = true;// Окно поверх всех остальных
                game.Show(); // Показываем форму предыстории
                this.Hide();
            }
        }

        private void labelAbout_Click(object sender, EventArgs e)
        {
            string gameInfo = @"Welcome to the game 'Escape from the Cave'!

You find yourself in a labyrinth of caves and must find a way out before time runs out.

Controls:
Use buttons to choose a path
The 'Back' button returns you to the previous cave
The 'Scream' button spends 5 seconds of your time
Your goal is to find the exit within the allotted time

Click OK to continue.";

            MessageBox.Show(gameInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
