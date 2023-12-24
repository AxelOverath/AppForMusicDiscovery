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
        }
    }
}
