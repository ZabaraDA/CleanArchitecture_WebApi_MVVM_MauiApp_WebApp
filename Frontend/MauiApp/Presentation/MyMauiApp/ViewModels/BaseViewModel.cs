using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyMauiApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
            set 
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        #region OnPropertyChanged
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            //if (PropertyChanged != null)
            //    PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
