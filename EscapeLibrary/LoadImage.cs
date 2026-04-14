using System.Drawing;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace EscapeLibrary
{
    public class LoadImage
    {
        public Image LoadCaveImage(int caveId)
        {
            string path = Path.Combine(Application.StartupPath, "Caves", $"cave{caveId}.jpg");//идём к файлу в папке с ехе файлом
            if (File.Exists(path))
                return Image.FromFile(path); // Если существует то выдаёт изображение
            return null;
        }

        public Image LoadEndGameImage(string imageName)
        {
            string imagePath = Path.Combine(Application.StartupPath, "Caves", imageName);
            if (File.Exists(imagePath))
                return Image.FromFile(imagePath);
            return null;
        }
    }
}