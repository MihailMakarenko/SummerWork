using Telegram.Bot;
using Telegram.Bot.Types;
namespace SummerWorkProject.Pages;
 /*номер чата 5037655460*/

public partial class TelegramPage : ContentPage
{
    public TelegramPage()
    {
        InitializeComponent();
        CreateFile.CreateFileMethod(_filePath);
        _currentRecords = Record.AllRecordsProducts;
        WorkWithTelegram workWithTelegram = new WorkWithTelegram();

        if (WorkWithTelegram.ChatId == 0)
        {
            DisplayAlert("Внимание", "Вначале вам необходимо добавить номер чата, для этого приложение должно оставаться запущенным, а данные необходимо ввести в поле внизу страницы", "Продолжить");
        }
        else
        {
            IdChat.Text = WorkWithTelegram.ChatId.ToString();
        }
    }

    private static string _filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ChatId.txt");

    private Record[] _currentRecords;



    private void DatePickerStart_DateSelected(object sender, DateChangedEventArgs e)
    {
        CheckDate();
        _currentRecords = Record.AllRecordsProducts.Where(item => item.DateTimeOperation.Date >= DateStart.Date && item.DateTimeOperation.Date <= DateFinish.Date).ToArray();
    }

    private void DatePickerFinish_DateSelected(object sender, DateChangedEventArgs e)
    {
        CheckDate();
        _currentRecords = Record.AllRecordsProducts.Where(item => item.DateTimeOperation.Date <= DateFinish.Date && item.DateTimeOperation.Date >= DateStart.Date).ToArray();
    }

    private void ButtonPeriodYear_Clicked(object sender, EventArgs e)
    {
        DateStart.Date = new DateTime(DateTime.Now.Year, 1, 1);
        DateFinish.Date = DateTime.Now;
    }

    private void ButtonPeriodMonth_Clicked(object sender, EventArgs e)
    {
        DateStart.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateFinish.Date = DateTime.Now;
    }

    private void ButtonAllTime_Clicked(object sender, EventArgs e)
    {
        DateStart.Date = Record.AllRecordsProducts.Min(item => item.DateTimeOperation);
        DateFinish.Date = Record.AllRecordsProducts.Max(item => item.DateTimeOperation);
    }

    private async void ButtonSendExel_Clicked(object sender, EventArgs e)
    {
        if (_currentRecords.Length == 0)
        {
            await DisplayAlert("Внимание", "За выбранный период нет трат", "Продолжить");
        }
        else
        {
            bool result = await WorkWithTelegram.TelegramSendFile(WorkWithExelPdf.SaveFileAsExel(_currentRecords));
            MessageAboutSendFile(result);
        }
    }

    private async void ButtonSendPdf_Clicked(object sender, EventArgs e)
    {
        if (_currentRecords.Length == 0)
        {
            await DisplayAlert("Внимание", "За выбранный период нет трат", "Продолжить");
        }
        else
        {
            bool result = await WorkWithTelegram.TelegramSendFile(WorkWithExelPdf.SaveFileAsPdf(_currentRecords));
            MessageAboutSendFile(result);
        }
    }

    private void CheckDate()
    {
        if (DateStart.Date > DateFinish.Date)
        {
            (DateStart.Date, DateFinish.Date) = (DateFinish.Date, DateStart.Date);
        }
    }

    private void SortPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_currentRecords == null)
        {
            _currentRecords = Record.AllRecordsProducts;
        }
        switch (SortPicker.SelectedIndex)
        {
            case 0:
                Record.SortRecordOnDate(_currentRecords);
                break;
            case 1:
                Record.SortRecordOnDate(_currentRecords);
                Array.Reverse(_currentRecords);
                break;
            case 2:
                Record.SortRecordOnPrice(_currentRecords);
                break;
            case 3:
                Record.SortRecordOnPrice(_currentRecords);
                Array.Reverse(_currentRecords);
                break;
        }
    }

    private async void AddIdChat_Clicked(object sender, EventArgs e)
    {
        WorkWithTelegram.ChatId = long.Parse(IdChat.Text);
        await DisplayAlert("Уведомление", "Номер чата успешно установлен!!!!", "Продолжить");
        await DisplayAlert("Уведомление", "Номер чата успешно установлен!!!!", "Продолжить");
    }

    private async void MessageAboutSendFile(bool result)
    {
        if (!result)
        {
            await DisplayAlert("Внимание", "По какой то причине не данные не были отправлены, возможно не верно указан ChatId", "Продолжить");
        }
        else
        {
            await DisplayAlert("Внимание", "Данные успешно отправлены", "Продолжить");
        }
    }
}