using Frontend.MauiApp.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyMauiApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        #region Constructor
        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddCommand = new Command(() =>
            {
                _navigationService.Route = "MenuPage";
            });
        }
        #endregion

        #region Login Properties        
        private string _login = "erre";
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        private SecureString _password;
        public SecureString Password
        {
            private get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        #endregion

        #region Login Route Commands
        public ICommand AddCommand { get; set; }


        #endregion
    }
}
