namespace SummerWorkProject
{
    internal class Record
    {
        public Record(double price, DateTime dateTime, string name, string place)
        {
            this.price = Math.Abs(price);
            this.dateTime = dateTime;

            if (name == null || name.Length == 0)
            {
                this.name = "Без названия";
            }
            else
            {
                this.name = name;
            }

            if (place == null || place.Length == 0)
            {
                placeWhereBuy = "Без места";
            }
            else
            {
                placeWhereBuy = place;
            }
        }

        protected string name;
        protected double price;
        protected DateTime dateTime;
        protected string placeWhereBuy;
        private static string _filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Record.txt");
        private static Record[] _allRecords;


        /// <summary>
        /// Возвращает все записи о трате денег и покупке продуктов
        /// </summary>
        public static Record[] AllRecordsProducts
        {
            get
            {
                if (_allRecords == null)
                {
                    _allRecords = DownloadFile().ToArray();
                }
                Product[] _allProducts = Product.AllProducts;

                return _allRecords.Concat(_allProducts).ToArray();
            }

        }

        public virtual double Price
        {
            get { return price; }
            private set { }
        }

        public DateTime DateTimeOperation
        {
            get { return dateTime; }
            set
            {
                if (value > DateTime.Now)
                {
                    dateTime = DateTime.Now;
                }
                else
                {
                    dateTime = value;
                }
            }
        }

        public string Title
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public string Place
        {
            get
            {
                return placeWhereBuy;
            }
        }

        public static DateTime GetMinDateSpend
        {
            get
            {
                return _allRecords.Min(record => record.DateTimeOperation);
            }

        }

        public static DateTime GetLastDateSpend
        {
            get
            {
                return _allRecords.Max(record => record.DateTimeOperation);
            }
        }

        /// <summary>
        /// Возвращает общую потраченную сумму денежных средств за период
        /// </summary>
        /// <param name="records"></param>
        /// <param name="startTime"></param>
        /// <param name="finishTime"></param>
        /// <returns></returns>
        public static double GetTotalSumForPeriod(Record[] records, DateTime startTime, DateTime finishTime)
        {

            if (startTime > finishTime)
            {
                (startTime, finishTime) = (finishTime, startTime);
            }

            var productsInSearchDate = from record in records
                                       where record.DateTimeOperation < finishTime && record.DateTimeOperation >= startTime
                                       orderby record.DateTimeOperation
                                       select record;

            double tatalPrice = productsInSearchDate.Sum(product => product.Price);
            return tatalPrice;
        }


        /// <summary>
        /// Возвращает общую потраченную сумму для всех трат денег и продуктов
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="finishTime"></param>
        /// <returns></returns>
        public static double GetTotalSumForPeriod(DateTime startTime, DateTime finishTime)
        {
            if (startTime > finishTime)
            {
                (startTime, finishTime) = (finishTime, startTime);
            }

            var productsInSearchDate = from record in AllRecordsProducts
                                       where record.DateTimeOperation.Date <= finishTime.Date && record.DateTimeOperation.Date >= startTime.Date
                                       orderby record.DateTimeOperation
                                       select record;


            double totalPrice = 0;
            totalPrice = productsInSearchDate.Sum(product => product.Price);

            return totalPrice;
        }

        public override string ToString()
        {
            string str = $"Название@{name};" +
              $"Цена@{price};" +
              $"Дата покупки@{dateTime};" +
              $"Место@{placeWhereBuy}";
            return str;
        }

        /// <summary>
        /// Скачивает траты денег из файла
        /// </summary>
        /// <returns>Массив где храняться все потраченные средства</returns>
        public static List<Record> DownloadFile()
        {

            List<Record> records = new List<Record>();
            CreateFile.CreateFileMethod(_filePath);

            using (StreamReader reader = new StreamReader(_filePath))
            {
                while (true)
                {
                    string? line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;

                    string[] fieldsProduct = line.Split(";");
                    for (int j = 0; j < fieldsProduct.Length; j++)
                    {
                        fieldsProduct[j] = fieldsProduct[j].Split("@")[1];
                    }

                    records.Add(new Record(
                        double.Parse(fieldsProduct[1]),
                        DateTime.Parse(fieldsProduct[2]),
                        fieldsProduct[0],
                        fieldsProduct[3]));
                }
            }

            return records;
        }

        /// <summary>
        /// Добавляет экземпляр на котором был вызван метод в txt файл
        /// </summary>
        /// <returns></returns>
        public virtual async Task UploadDataInTxtFile()
        {
            CreateFile.CreateFileMethod(_filePath);
            await File.AppendAllTextAsync(_filePath, this.ToString() + "\n");
            AddCerordInArray();
        }

        /// <summary>
        /// Добавляет текущий экземпляр в массив где храняться все записи
        /// </summary>
        private void AddCerordInArray()
        {
            if (_allRecords == null)
            {
                _allRecords = DownloadFile().ToArray();
            }
            Array.Resize(ref _allRecords, _allRecords.Length + 1);
            _allRecords[_allRecords.Length - 1] = this;

        }

        /// <summary>
        /// Сортирует переданный массив записей по возрастанию потраченной суммы
        /// </summary>
        /// <param name="records"></param>
        public static void SortRecordOnPrice(Record[] records)
        {
            Array.Sort(records, (x, y) => x.Price.CompareTo(y.Price));
        }

        /// <summary>
        /// Метод соритрует переданный массив записей по возрастанию даты записи
        /// </summary>
        /// <param name="records"></param>
        public static void SortRecordOnDate(Record[] records)
        {
            Array.Sort(records, (x, y) => x.DateTimeOperation.CompareTo(y.DateTimeOperation));
        }
    }
}
