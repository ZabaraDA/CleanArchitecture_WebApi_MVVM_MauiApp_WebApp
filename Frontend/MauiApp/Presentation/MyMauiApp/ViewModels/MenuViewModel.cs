using Frontend.MauiApp.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyMauiApp.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        #region Constructor
        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddCommand = new Command(() =>
            {
                _navigationService.Route = "///LoginPage";
            });
        }
        #endregion

        #region Login Route Commands
        public ICommand AddCommand { get; set; }


        #endregion
    }
}
