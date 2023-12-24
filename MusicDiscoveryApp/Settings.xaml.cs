namespace MusicDiscoveryApp;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}


    public async void GoToSettingsPage(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new SettingsPage());
    }
}