using System.Reflection.Emit;
using System.Windows.Forms;
using EscapeFromTheCave.Forms;

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
        }

        private void labelStart_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно готовы начать игру?","Риторический вопрос",MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result==DialogResult.OK)
            {
                FormGame game = new FormGame();                               
                game.FormBorderStyle = FormBorderStyle.None; // Убираем заголовок и границы окна                 
                game.WindowState = FormWindowState.Maximized;// Окно на весь экран                
                game.TopMost = true;// Окно поверх всех остальных
                game.Show();
                this.Hide();
            }
        }
    }
}
