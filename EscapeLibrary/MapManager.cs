using System;
using System.Collections.Generic;
using System.IO;

namespace EscapeLibrary
{
    public enum CaveType { Normal, Victory, Death }
    public class CavePath // объект для одной строки (дороги)
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public int Time { get; set; }
    }

    public class MapManager
    {
        private const int VICTORY_CAVE_ID = 12;
        private const int DEATH_CAVE_ID = 19;

        public CaveType GetCaveType(int caveId)
        {
            if (caveId == VICTORY_CAVE_ID) 
                return CaveType.Victory;
            if (caveId == DEATH_CAVE_ID) 
                return CaveType.Death;
            return CaveType.Normal;
        }

        public List<CavePath> ReadMap(string filePath)
        {
            List<CavePath> paths = new List<CavePath>();// список, куда впишутся пути

            string[] lines = File.ReadAllLines(filePath);  // считываются все строки файла в массив строк

            if (File.Exists(filePath))
            {
                foreach (string line in lines)
                {
                    string[] parts = line.Trim().Split(',');

                    if (parts.Length != 3)
                    {
                        continue; 
                    }
                    try
                    {
                        paths.Add(new CavePath{FromId = int.Parse(parts[0]),ToId = int.Parse(parts[1]),Time = int.Parse(parts[2])}); // Создаёт объект пути
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                }
            }
            return paths; // возвращаем наполненный список путей
        }

        public List<CavePath> GetPaths(List<CavePath> allPaths, int currentCaveId)
        {
            if (allPaths == null) 
                throw new ArgumentNullException(nameof(allPaths));

            List<CavePath> result = new List<CavePath>();

            foreach (CavePath p in allPaths)
            {
                if (p.FromId == currentCaveId)
                {
                    result.Add(p);
                }
            }
            return result;
        }
    }    
}