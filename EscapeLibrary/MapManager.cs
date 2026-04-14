using System;
using System.Collections.Generic;
using System.IO;

namespace EscapeLibrary
{
    public enum CaveType { Normal, Victory, Death }
    public class CavePath // Объект для одной строки (дороги)
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
            if (caveId == VICTORY_CAVE_ID) return CaveType.Victory;
            if (caveId == DEATH_CAVE_ID) return CaveType.Death;
            return CaveType.Normal;
        }



        public List<CavePath> ReadMap(string filePath)
        {
            List<CavePath> paths = new List<CavePath>();// Список, куда впишутся пути

            string[] lines = File.ReadAllLines(filePath);  // Считываем все строки файла в массив строк

            if (File.Exists(filePath))
            {
                foreach (string line in lines)
                {
                    string[] parts = line.Trim().Split(',');

                    if (parts.Length != 3)
                    {
                        continue; // Пропускаем некорректные строки
                    }
                    try
                    {
                        paths.Add(new CavePath // Создаем объект пути и конвертируем текст в числа
                        {
                            FromId = int.Parse(parts[0]),
                            ToId = int.Parse(parts[1]),
                            Time = int.Parse(parts[2])
                        });
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                }
            }
            return paths; // Возвращаем наполненный список путей
        }

        public List<CavePath> GetAvailablePaths(List<CavePath> allPaths, int currentCaveId)
        {
            if (allPaths == null)
                throw new ArgumentNullException(nameof(allPaths)); 
            
            return allPaths.FindAll(p => p.FromId == currentCaveId);// находим все пути, где FromId совпадает с текущей пещерой
        }
    }    
}