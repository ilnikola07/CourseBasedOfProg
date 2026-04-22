using System.Drawing;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace EscapeLibrary
{
    public class LoadImage
    {
        public Image LoadPhoto(int caveId)
        {
            string path = Path.Combine(Application.StartupPath, "Caves", $"cave{caveId}.jpg");//идём к файлу в папке рядом с ехе файлом
            if (File.Exists(path))
                return Image.FromFile(path); 
            return null;
        }

        public Image LoadEndPhoto(string imageName)
        {
            string imagePath = Path.Combine(Application.StartupPath, "Caves", imageName);
            if (File.Exists(imagePath))
                return Image.FromFile(imagePath);
            return null;
        }
    }
}