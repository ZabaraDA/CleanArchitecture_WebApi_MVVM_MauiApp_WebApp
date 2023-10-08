
namespace Frontend.MauiApp.Core.Application.Interfaces
{
    
    public interface INavigationService
    {
        string Route { get; set; }

        event Action CurrentRouteChanged;
        void OnCurrentRouteChanged();
    }
}
