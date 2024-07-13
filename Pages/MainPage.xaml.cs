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
        // Переход к странице добавления товаров
    }

    private async void Button_Clicked_AddEvent(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SpendMoneyPage");
        // Переход к странице добавления траты денег
    }

    private async void Button_Clicked_SendToTelegram(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("TelegramPage");
        // Переход на страницу с общей информацией 
    }

    private async void Button_Clicked_Information(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("StatisticPage");
        // Переход на страницу с информацией 
    }
}