using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// КЛАСС НУЖНО ДОРАБОАТЬ ЧТО БЫ ПОЛЬЗОВАТЕЛЬ ДОБАВЛЯЛ ТИПЫ ПРОДУКТОВ
namespace SummerWorkProject
{
    internal abstract class TypeProduct
    {
        private static string[] _typeProducts;



        private static string _filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "TypeProduct.txt");
        async public static Task<string[]> GetProducts()
        {

            if (_typeProducts != null)
            {
                return _typeProducts;
            }

            CreateFile.CreateFileMethod(_filePath);

            _typeProducts = await File.ReadAllLinesAsync(_filePath);
            if (_typeProducts == null || _typeProducts.Length == 0)
            {
                _typeProducts = ["Напитки", "Хлеб", "Крупы", "Колбасы", "Чипсы/Сухарики"];
            }
            return _typeProducts;
        }

        public static void AddType(string typeProduct)
        {
            CreateFile.CreateFileMethod(_filePath);
            File.AppendAllText(_filePath, typeProduct + Environment.NewLine);
        }

    }
}
