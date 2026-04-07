using System;
using System.Collections.Generic;
using System.IO;

namespace EscapeLibrary
{
    public class CavePath // Объект для одной строки (дороги)
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public int Time { get; set; }
    }

    public class MapManager
    {
        public List<CavePath> ReadMap(string filePath)
        {
            List<CavePath> paths = new List<CavePath>();// Список, куда впишутся пути

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath); //Открывает файл и помещает каждую строку в массив
                foreach (string line in lines)
                {
                    string[] parts = line.Split(','); // Разделитель в этом случае запятая
                    if (parts.Length == 3) // точно ли 3 значения
                    {
                        paths.Add(new CavePath
                        {
                            FromId = int.Parse(parts[0]),
                            ToId = int.Parse(parts[1]),
                            Time = int.Parse(parts[2])
                        });
                    }
                }
            }
            return paths;
        }

        public List<CavePath> GetAvailablePaths(List<CavePath> allPaths, int currentCaveId)
        {
            // Находим все пути, где FromId совпадает с текущей пещерой
            return allPaths.FindAll(p => p.FromId == currentCaveId);
        }


    }
    
}