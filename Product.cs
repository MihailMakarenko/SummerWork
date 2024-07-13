

namespace SummerWorkProject
{
    internal class Product : Record
    {
        public Product(double price,
           DateTime dateTime,
           string type,
           byte count,
           string placeWhereBuy = "Без места",
           string title = "Без названия",
           string description = "Без описания")
           : base(price, dateTime, title, placeWhereBuy)
        {

            _type = type.ToUpper();
            _count = count;

            if (description == null || description.Length == 0)
            {
                _description = "Без описания";
            }
            else
            {
                _description = description;
            }
         
         
        }

        private string _description;
        private string _type;
        private byte _count;
        private static string _filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Product.txt");
        private static Product[] _allProducts;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// Свойство позволяет получить общую стоимость потраченную на продукт
        /// </summary>
        public override double Price
        {
            get
            {
                return _count * price;
            }
        }

     
        /// <summary>
        /// Свойство возвращает все записи о покупки продуктов
        /// </summary>
        public static Product[] AllProducts
        {
            get
            {
                if (_allProducts == null)
                {
                    _allProducts = DownloadFile().ToArray();
                }
                return _allProducts;
            }
        }

        public override string ToString()
        {
            string str = base.ToString() +
                $";колличество@{_count};" +
                $"тип@{_type};" +
                $"описание@{_description}\n";
            return str;
        }

       
        /// <summary>
        /// Поиск продуктов которые были куплены в определенную дату
        /// </summary>
        /// <param name="products"></param>
        /// <param name="inputDate"></param>
        /// <returns>Массив продуктов</returns>
        public static Product[]? SearchProductByDate(Product[] products, DateTime inputDate)
        {
            var productsSearch = from product in products
                                 where product.DateTimeOperation == inputDate
                                 orderby product.Price
                                 select product;
            if (productsSearch.ToArray().Length == 0)
            {
                return null;
            }
            return productsSearch.ToArray();
        }

        /// <summary>
        /// Выполняет поиск продуктов по их типу
        /// </summary>
        /// <param name="products"></param>
        /// <param name="typeProduct"></param>
        /// <returns>Возвращает массив продуктов удовлетворяющих условию</returns>
        public static Product[]? SearchProductByType(Product[] products, string typeProduct)
        {
            var productsSearch = from product in products
                                 where product.Type == typeProduct
                                 orderby product.DateTimeOperation, product.Price
                                 select product;

            if (productsSearch.ToArray().Length == 0)
            {
                return null;
            }

            return productsSearch.ToArray();
        }

        /// <summary>
        /// Загружает текущий экземпляр у которого был вызван метод, в txt файл
        /// </summary>
        /// <returns></returns>
        async public override Task UploadDataInTxtFile()
        {
            CreateFile.CreateFileMethod(_filePath);
            await File.AppendAllTextAsync(_filePath, this.ToString());
            AddProductInArray();    
        }

        /// <summary>
        /// Скачивает продукты из txt файла
        /// </summary>
        /// <returns>Возвращает списпок продуктов</returns>
        private static new List<Product> DownloadFile()
        {

            List<string> productsStr = new List<string>();
            CreateFile.CreateFileMethod(_filePath);
            List<Product> records = new List<Product>();
            using (StreamReader reader = new StreamReader(_filePath))
            {
                while (true)
                {
                    string? line = reader.ReadLine();
                    if (line == null)
                    {
                        break; // Если достигнут конец файла, выходим из цикла
                    }

                    string[] fieldsProduct = line.Split(";");
                    for (int j = 0; j < fieldsProduct.Length; j++)
                    {
                        fieldsProduct[j] = fieldsProduct[j].Split("@")[1];
                    }

                    // тут нужно доделать
                    records.Add(new Product(double.Parse(fieldsProduct[1]), DateTime.Parse(fieldsProduct[2]), fieldsProduct[5], byte.Parse(fieldsProduct[4]), fieldsProduct[3], fieldsProduct[0], fieldsProduct[6]));

                }
            }
            return records;


        }


        /// <summary>
        /// Добавляет экземпляр продукта на котором был вызван метод в общий массив 
        /// где храняться все продуты которые были куплены за все время
        /// </summary>
        private void AddProductInArray()
        {
            if (_allProducts == null)
            {
                _allProducts = DownloadFile().ToArray();
            }
            Array.Resize(ref _allProducts, _allProducts.Length + 1);
            _allProducts[_allProducts.Length - 1] = this;
        }
      
    }
}
