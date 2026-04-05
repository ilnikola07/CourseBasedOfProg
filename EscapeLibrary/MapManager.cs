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
        public List<CavePath> ReadMap(string filePath) // Чтение
        {
            List<CavePath> paths = new List<CavePath>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
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
    }
}