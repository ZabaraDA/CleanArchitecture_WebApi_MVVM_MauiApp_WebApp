using MyMauiApp.ViewModels;
using MyMauiApp.Views;

namespace MyMauiApp
{
    public partial class App : Application
    {
        public App(AppShell appShell)
        {
            InitializeComponent();

            MainPage = appShell;
        }
    }
}