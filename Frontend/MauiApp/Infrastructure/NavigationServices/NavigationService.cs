using Frontend.MauiApp.Core.Application.Interfaces;

namespace Frontend.MauiApp.Infrastructure.NavigationServices
{
    public class NavigationService : INavigationService
    {
        private string _route = "//General/Menu";
        public string Route 
        {
            get 
            { 
                return _route;
            }
            set
            {
                _route = value;
                OnCurrentRouteChanged();
            }
        }

        public event Action CurrentRouteChanged;

        public void OnCurrentRouteChanged()
        {
            CurrentRouteChanged?.Invoke();
        }
    }
}
