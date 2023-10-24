using Frontend.MauiApp.Core.Application.Interfaces;
using Frontend.MauiApp.Core.Domain.Models;
using Frontend.MauiApp.Infrastructure.DataManager;
using MyMauiApp.Models;
using MyMauiApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Windows.Input;

namespace MyMauiApp.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IDataManager _dataManager;

        private ObservableCollection<Product> _productList;
        public ObservableCollection<Product> ProductList
        {
            get
            {
                return _productList;
            }
        }

        #region Constructor
        public MenuViewModel(INavigationService navigationService, IDataManager dataManager)
        {
            _navigationService = navigationService;
            _dataManager = dataManager;
        }
        #endregion

        #region Private Methods
        private async Task GetProduct()
        {
            if (_productList == null)
            {
                _productList = new ObservableCollection<Product>();
            }
            _productList.Clear();
            var response = await _dataManager.GetItems<ApiResponse<List<Product>>>("Product");
            foreach (var product in response.Result)
            {
                _productList.Add(product);
            }
            OnPropertyChanged(nameof(ProductList));
        }
        #endregion


        #region Route Commands
        public ICommand LoginRouteCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.Route = "//Access/Login";
                });
            }
        }


        #endregion
        #region Function Commands
        public ICommand ProductGetCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Title += "Text ";
                    await GetProduct();
                });
            }
        }

        #endregion

        public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
        {
            public NotifyTaskCompletion(Task<TResult> task)
            {
                Task = task;
                if (!task.IsCompleted)
                {
                    var _ = WatchTaskAsync(task);
                }
            }
            private async Task WatchTaskAsync(Task task)
            {
                try
                {
                    await task;
                }
                catch
                {
                }
                var propertyChanged = PropertyChanged;
                if (propertyChanged == null)
                    return;
                propertyChanged(this, new PropertyChangedEventArgs("Status"));
                propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
                propertyChanged(this, new PropertyChangedEventArgs("IsNotCompleted"));
                if (task.IsCanceled)
                {
                    propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
                }
                else if (task.IsFaulted)
                {
                    propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
                    propertyChanged(this, new PropertyChangedEventArgs("Exception"));
                    propertyChanged(this,
                      new PropertyChangedEventArgs("InnerException"));
                    propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
                }
                else
                {
                    propertyChanged(this,
                      new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
                    propertyChanged(this, new PropertyChangedEventArgs("Result"));
                }
            }
            public Task<TResult> Task { get; private set; }
            public TResult Result
            {
                get
                {
                    return (Task.Status == TaskStatus.RanToCompletion) ?
              Task.Result : default(TResult);
                }
            }
            public TaskStatus Status { get { return Task.Status; } }
            public bool IsCompleted { get { return Task.IsCompleted; } }
            public bool IsNotCompleted { get { return !Task.IsCompleted; } }
            public bool IsSuccessfullyCompleted
            {
                get
                {
                    return Task.Status ==
              TaskStatus.RanToCompletion;
                }
            }
            public bool IsCanceled { get { return Task.IsCanceled; } }
            public bool IsFaulted { get { return Task.IsFaulted; } }
            public AggregateException Exception { get { return Task.Exception; } }
            public Exception InnerException
            {
                get
                {
                    return (Exception == null) ?
              null : Exception.InnerException;
                }
            }
            public string ErrorMessage
            {
                get
                {
                    return (InnerException == null) ?
              null : InnerException.Message;
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}
