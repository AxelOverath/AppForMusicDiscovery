namespace MusicDiscoveryApp.ViewModels;

public partial class LoginViewModel : ViewModel
{
    private readonly ISpotifyService spotifyService;
    public LoginViewModel(ISpotifyService spotifyService)
    {
        this.spotifyService = spotifyService;

    }



    [ObservableProperty]
    private bool showLogin;


    [RelayCommand]
    public void OpenLogin()
    {
        ShowLogin = true;
    }
    public async Task HandleAuthCode(string code)
    {
       await spotifyService.Initialize(code);

        
    }

}