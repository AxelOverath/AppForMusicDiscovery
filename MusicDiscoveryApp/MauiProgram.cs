global using Microsoft.Extensions.Logging;
global using MusicDiscoveryApp.ViewModels;
global using MusicDiscoveryApp.Views;



global using TinyMvvm;

global using CommunityToolkit;
global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;

global using System.Text.Json.Serialization;
global using System;
global using System.Net;
global using System.Text;
global using System.Text.Json;
global using System.Windows.Input;
global using System.Collections.ObjectModel;
global using MusicDiscoveryApp.Services;



namespace MusicDiscoveryApp
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginView>();

            builder.Services.AddSingleton<ISpotifyService, SpotifyService>();
            builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
            return builder.Build();
        }
    }
}
