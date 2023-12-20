using Microsoft.Maui.Platform;
using MusicDiscoveryApp.Handlers;
using SpotifyAPI.Web;

namespace MusicDiscoveryApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // borderless entry
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Borderless), (handler, view) =>
            {
                if (view is Borderless)
                {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif __IOS__
                    handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
                }
            });

            MainPage = new AppShell();

        /*    var authService = new SpotifyAuthService();
            var accessToken = await authService.Authenticate();

            if (!string.IsNullOrEmpty(accessToken))
            {
                // Now you can use the access token to interact with the Spotify API
                // For example, you can use it to make requests using HttpClient or other networking libraries
            }


            */
        }
    }
}
