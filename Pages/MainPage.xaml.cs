namespace SummerWorkProject.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked_AddProduct(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("BuyProductPage");
        // ������� � �������� ���������� �������
    }

    private async void Button_Clicked_AddEvent(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SpendMoneyPage");
        // ������� � �������� ���������� ����� �����
    }

    private async void Button_Clicked_SendToTelegram(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("TelegramPage");
        // ������� �� �������� � ����� ����������� 
    }

    private async void Button_Clicked_Information(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("StatisticPage");
        // ������� �� �������� � ����������� 
    }
}