using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;
using MusicDiscoveryApp.ViewModels;

namespace MusicDiscoveryApp.Views;

public partial class LoginView
{
    private readonly LoginViewModel loginViewModel;

    private readonly string state;

    public LoginView(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        this.loginViewModel = loginViewModel;

        BindingContext = loginViewModel;

        state = Guid.NewGuid().ToString();

        Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping("UserAgent", (handler, webview) =>
        {

            var userAgent = "Chrome";
#if ANDROID
            handler.PlatformView.Settings.UserAgentString = userAgent;
#elif IOS
            handler.PlatformView.CustomUserAgent = userAgent;
#endif
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        loginViewModel.PropertyChanged += LoginViewModel_PropertyChanged;
        LoginWeb.Navigating += LoginWeb_Navigating;
    }


    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        loginViewModel.PropertyChanged -= LoginViewModel_PropertyChanged;
        LoginWeb.Navigating -= LoginWeb_Navigating;
    }

    private void LoginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(loginViewModel.ShowLogin))
        {
            var scopes = "playlist-read-private playlist-modify-private";

            var querystring = $"response_type=code&client_id={Constants.SpotifyClientId}&scopes={WebUtility.UrlEncode(scopes)}&redirect_uri={Constants.RedirectUrl}&state={state}";

            LoginWeb.Source = $"https://accounts.spotify.com/authorize?{querystring}";
            Login.TranslationY = this.Height;
            Login.IsVisible = true;

            Login.TranslateTo(Login.X, 100, easing: Easing.Linear);
        }
    }

    private async void LoginWeb_Navigating(object? sender, WebNavigatingEventArgs e)
    {
        if (!e.Url.Contains("redirect_uri") && e.Url.Contains(Constants.RedirectUrl))
        {
            var queryString = e.Url.Split("?").Last();
            var parts = queryString.Split("&");

            var parameters = parts.Select(x => x.Split("=")).ToDictionary(x => x.First(), x => x.Last());

            var code = parameters["code"];
            var returnState = parameters["state"];

            if (returnState == state && !string.IsNullOrWhiteSpace(code))
            {
                _ = Task.Run(async () => await loginViewModel.HandleAuthCode(code));


                await Login.TranslateTo(Login.X, this.Height, easing: Easing.Linear);
                Login.IsVisible = false;
            }
        }
    }
    public async void GoToSwipe_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Swipepage());
    }
}