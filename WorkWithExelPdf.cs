using Aspose.Cells;

namespace SummerWorkProject
{
    internal abstract class WorkWithExelPdf
    {
        private static string _filePathXls = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "AllStatistic.xlsx");
        private static string _filePathPdf = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "AllStatistic.pdf");

        /// <summary>
        /// Метод сохраняет записи в формат EXEL и предоставляет путь к файлу
        /// </summary>
        /// <param name="products"></param>
        /// <returns>Путь к файлу</returns>
        public static string SaveFileAsExel(params Record[] products)
        {
            bool fileExists = File.Exists(_filePathXls);

            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
            File.Delete(_filePathXls);

            Worksheet worksheet = workbook.Worksheets.Add("MySheet");
            worksheet.AutoFitRows(); 
            worksheet.Cells.Style.Font.Size = 14;
            worksheet.Cells.Style.Font.IsBold = true;
          

            AddTitle(worksheet, workbook);

        
            int row = 1;

            AddProductsInWorksheetAsync(row, worksheet, products);
          
            worksheet.AutoFitColumns();

            worksheet.PageSetup.Orientation = PageOrientationType.Landscape;
            worksheet.PageSetup.BottomMargin = 1;
            worksheet.PageSetup.LeftMargin = 1;
            worksheet.PageSetup.RightMargin = 1;
            worksheet.PageSetup.TopMargin = 1;
            workbook.Save(_filePathXls, SaveFormat.Xlsx);

            return _filePathXls;
        }

        /// <summary>
        /// Метод сохраняет записи в формат PDF и предоставляет путь к файлу
        /// </summary>
        /// <param name="records"></param>
        /// <returns>Путь к файлу</returns>
        public static string SaveFileAsPdf(params Record[] records)
        {

            SaveFileAsExel(records);

            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(_filePathXls);

            var worksheet = workbook.Worksheets[0];


            worksheet.PageSetup.Orientation = Aspose.Cells.PageOrientationType.Landscape;
            worksheet.PageSetup.BottomMargin = 1;
            worksheet.PageSetup.LeftMargin = 1;
            worksheet.PageSetup.RightMargin = 1;
            worksheet.PageSetup.TopMargin = 1;
            worksheet.PageSetup.Orientation = PageOrientationType.Portrait;
            workbook.Save(_filePathPdf, SaveFormat.Pdf);

            return _filePathPdf;

        }

        /// <summary>
        /// Метод добавляет на лист EXEL название столбцов
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="workbook"></param>
        private static void AddTitle(Worksheet worksheet, Workbook workbook)
        {
            worksheet.Cells[0, 0].Value = "ДАТА";
            worksheet.Cells[0, 1].Value = "ЦЕНА";
            worksheet.Cells[0, 2].Value = "НАЗВАНИЕ";
            worksheet.Cells[0, 3].Value = "КОЛЛ";
            worksheet.Cells[0, 4].Value = "ТИП";
            worksheet.Cells[0, 5].Value = "МЕСТО";
            worksheet.Cells[0, 6].Value = "ОПИСАНИЕ";
        }

        /// <summary>
        /// Добавляет данные на лист Exel 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="worksheet"></param>
        /// <param name="products"></param>
        private static void AddProductsInWorksheetAsync(int row, Worksheet worksheet, params Record[] products)
        {
            // Запись данных в пустые ячейки
            for (int i = 0; i < products.Length; i++)
            {
                worksheet.Cells[row, 2].Value = products[i].Title;
                worksheet.Cells[row, 1].Value = Math.Round(products[i].Price, 1).ToString();
                worksheet.Cells[row, 0].Value = products[i].DateTimeOperation.ToString("d");
                if (products[i] is Product product)
                {
                    worksheet.Cells[row, 3].Value = product.Count.ToString();
                    worksheet.Cells[row, 4].Value = product.Type;
                    worksheet.Cells[row, 5].Value = product.Place;
                    worksheet.Cells[row, 6].Value = product.Description;
                }
                row++;
            }
            row++;

            worksheet.Cells[row, 0].Value = "Сумма";
            worksheet.Cells[row, 1].Value = products.Sum(item => item.Price).ToString();
        }
    }
}
