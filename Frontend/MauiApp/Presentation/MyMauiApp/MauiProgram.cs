using Frontend.MauiApp.Core.Application.Interfaces;
using Frontend.MauiApp.Infrastructure.NavigationServices;
using Microsoft.Extensions.Logging;
using MyMauiApp.ViewModels;
using MyMauiApp.Views;

namespace MyMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));

            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<MenuPage>();
            builder.Services.AddTransient<MenuViewModel>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();




#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}