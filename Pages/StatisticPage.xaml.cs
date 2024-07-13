using System.Text;

namespace SummerWorkProject.Pages;

public partial class StatisticPage : ContentPage
{
    public StatisticPage()
    {
        InitializeComponent();
        _currentDate = DateTime.Now;
        Dictionary<DateTime, double> keyValuePairs = GetDictionaryDayMoneyInMonth(Record.AllRecordsProducts, _currentDate);
        TableStatistic.Text = DictionaryToSting(keyValuePairs);
    }

    private DateTime _currentDate;


    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        _currentDate = DatePickerName.Date;
        Dictionary<DateTime, double> keyValuePairs = GetDictionaryDayMoneyInMonth(Record.AllRecordsProducts, _currentDate);
        TableStatistic.Text = DictionaryToSting(keyValuePairs);
    }

    private Dictionary<DateTime, double> GetDictionaryDayMoneyInMonth(Record[] records, DateTime dateTime)
    {
        Dictionary<DateTime, double> keyValuePairs = new Dictionary<DateTime, double>();

        int countDaysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        dateTime = new DateTime(dateTime.Year, dateTime.Month, 1);
        for (int i = 1; i <= countDaysInMonth; i++)
        {
            if (keyValuePairs.TryGetValue(dateTime.Date, out double value))
            {
                keyValuePairs[dateTime.Date] += value;
            }
            else
            {
                keyValuePairs.Add(dateTime.Date, Record.GetTotalSumForPeriod(dateTime, dateTime));
            }
            dateTime = dateTime.AddDays(1);
        }

        return keyValuePairs;
    }

    private string DictionaryToSting(Dictionary<DateTime, double> keyValuePairs)
    {
        StringBuilder stringBuilder = new StringBuilder("\tÄÀÒÀ\t\t\t\tÖÅÍÀ\n");

        foreach (var item in keyValuePairs)
        {
            if (item.Key.Day.ToString().Length == 1)
            {
                stringBuilder.Append($"{item.Key.Date.ToString("d")}\t\t\t  {item.Value}\n");
            }
            else
            {
                stringBuilder.Append($"{item.Key.Date.ToString("d")}\t\t\t{item.Value}\n");
            }
        }

        double totalCostAmount = keyValuePairs.Sum(kv => kv.Value);
        stringBuilder.Append($"\nÎáùàÿ ñóììà:\t\t\t {totalCostAmount}\n");

        return stringBuilder.ToString();
    }


}