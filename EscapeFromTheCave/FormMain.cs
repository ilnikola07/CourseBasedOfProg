using System.Reflection.Emit;
using System.Windows.Forms;

namespace EscapeFromTheCave
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            labelStart.Parent = pictureBoxMain;// “.к. установить прозрачность на кнопках т€жело, используютс€ label,
            labelName.Parent = pictureBoxMain;// поэтому прив€зываем label к единому picturebox 
            labelAbout.Parent = pictureBoxMain;// дл€ прозрачности текста на в главной форме
            labelInc.Parent = pictureBoxMain;
        }


      
    }
}
