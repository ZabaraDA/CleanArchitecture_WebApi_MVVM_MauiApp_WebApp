using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MyMauiApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

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
    }
}
