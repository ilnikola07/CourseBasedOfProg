using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace EscapeLibrary
{
    public class QuestManager
    {
        private Dictionary<int, string> _caveTexts = new Dictionary<int, string>();
        private string _fullCurrentText = "";
        private int _charIndex = 0;


        public void SetCurrentQuest(int caveId)
        {
            if (_caveTexts.TryGetValue(caveId, out string text))
                _fullCurrentText = text;
            else
                _fullCurrentText = "Описание этой пещеры потеряно во тьме...";

            _charIndex = 0;
        }

        public char? GetNextLetter()
        {
            if (_charIndex < _fullCurrentText.Length)
                return _fullCurrentText[_charIndex++];
            return null;
        }

        public void LoadQuestsFromFile(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (!File.Exists(filePath)) return;

            string[] lines = File.ReadAllLines(filePath);
            int currentId = -1;
            string currentText = "";

            foreach (var line in lines)
            {
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                    continue;

                string firstWord = trimmedLine.Split(' ')[0];

                if (int.TryParse(firstWord, out int id))
                {
                    
                    if (currentId != -1) // если уже читали описание для предыдущей пещеры — сохраняем его
                          _caveTexts[currentId] = currentText.Trim();
                    currentId = id;                   
                    currentText = trimmedLine.Substring(firstWord.Length).Trim() + Environment.NewLine; // убираем само число из начала строки, оставляя остаток текста
                }
                else
                {                    
                    currentText += trimmedLine + Environment.NewLine;// Если это не число, значит это продолжение текста текущей пещеры
                }
            }            
            if (currentId != -1)
            {
                _caveTexts[currentId] = currentText.Trim(); // сохранить последнюю прочитанную пещеру
            }
        }
    }
}
