using SummerWorkProject;
using SummerWorkProject.Pages;
namespace SummerWorkProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("TelegramPage", typeof(TelegramPage));
            Routing.RegisterRoute("SpendMoneyPage", typeof(SpendMoney));
            Routing.RegisterRoute("BuyProductPage", typeof(BuyProduct));
            Routing.RegisterRoute("StatisticPage", typeof(StatisticPage));
            MainPage = new AppShell();
        }
    }
}
