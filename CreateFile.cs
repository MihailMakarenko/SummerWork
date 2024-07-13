using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerWorkProject
{
    internal abstract class CreateFile
    {
        /// <summary>
        /// Метод создает файл по указаному пути если его ранее не существовало
        /// </summary>
        /// <param name="filePath"></param>
        public static void CreateFileMethod(string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }
        }
    }
}
