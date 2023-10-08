using Frontend.MauiApp.Core.Application.Interfaces;
using System;
using System.Windows.Input;

namespace MyMauiApp.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        #region AppShell Private Properties
        public INavigationService _navigationService;
        #endregion

        #region Constructor
        public AppShellViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.CurrentRouteChanged += OnCurrentRouteChanged;
        }
        #endregion

        private async void OnCurrentRouteChanged()
        {
            await Shell.Current.GoToAsync(Route);
            OnPropertyChanged(nameof(Route));
        }

        #region AppShell Properties
        public string Route
        {
            get
            {
                return _navigationService.Route;
            }
        }
        #endregion

        #region AppShell Route Commands
        public ICommand AddCommand { get; set; }


        #endregion
    }
}
