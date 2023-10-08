using MyMauiApp.ViewModels;

namespace MyMauiApp.Views
{
    public partial class AppShell : Shell
    {
        public  AppShell(AppShellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}