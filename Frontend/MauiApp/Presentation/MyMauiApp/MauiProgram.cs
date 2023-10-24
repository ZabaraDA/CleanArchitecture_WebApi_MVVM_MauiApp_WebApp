using CommunityToolkit.Maui;
using Frontend.MauiApp.Core.Application.Interfaces;
using Frontend.MauiApp.Infrastructure.DataManager;
using Frontend.MauiApp.Infrastructure.NavigationServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyMauiApp.ViewModels;
using MyMauiApp.Views;
using System.Diagnostics;
using System.Reflection;

namespace MyMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("MyMauiApp.appsettings.json"))
            {
                var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
                builder.Configuration.AddConfiguration(config);
            }


            //Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            //Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
            //Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MenuPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<RegistrationPage>();

            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<MenuViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<RegistrationViewModel>();

            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddScoped<HttpClient>();

            string apiWebAddress = builder.Configuration.GetConnectionString("ApiWebAddress");
            builder.Services.AddScoped<HttpClient>();
            builder.Services.AddScoped(token => new Token()
            {
                BaseUrl = apiWebAddress
            });
            builder.Services.AddScoped<IDataManager, DataManager>();

#if DEBUG
        builder.Logging.AddDebug();
            //проверка файлов
            var aa = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (var item in aa)
            {
                Debug.WriteLine($"\n{item}\n");
            }
#endif

            return builder.Build();
        }
    }
}