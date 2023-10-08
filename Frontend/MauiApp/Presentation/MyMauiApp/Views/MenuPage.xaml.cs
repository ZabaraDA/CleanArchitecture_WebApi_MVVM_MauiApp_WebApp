using MyMauiApp.ViewModels;

namespace MyMauiApp.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage(MenuViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}