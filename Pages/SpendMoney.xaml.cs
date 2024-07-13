namespace SummerWorkProject.Pages;

public partial class SpendMoney : ContentPage
{
    public SpendMoney()
    {
        InitializeComponent();
        _currentDate = DateTime.Now.Date;
    }
    private DateTime _currentDate;

    private void DateBuy_DateSelected(object sender, DateChangedEventArgs e)
    {

    }

    private async void AddRecordBtn_Clicked(object sender, EventArgs e)
    {
        if (!CheckFields())
        {
            await DisplayAlert("Уведомление", "Не все поля заполенны", "Продолжить");
        }
        else
        {
            Record record = new Record(double.Parse(PriceEntry.Text), DateBuy.Date, Title.Text, Place.Text);
            await record.UploadDataInTxtFile();
            var choice = await DisplayAlert("Уведомление", "Данные добавлены", "Продолжить", "Закончить");
            if (choice)
            {
                ClearnField();
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }

        }
    }

    private bool CheckFields()
    {
        if (Title.Text == null || Title.Text.Trim().Length == 0 || PriceEntry.Text == null || PriceEntry.Text.Trim().Length == 0 || Place.Text == null || Place.Text.Trim().Length == 0)
        {
            return false;
        }
        return true;
    }

    private void ClearnField()
    {
        Title.Text = null;
        PriceEntry.Text = null;

    }
}