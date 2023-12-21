namespace MusicDiscoveryApp;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}

	async void GoToSettings(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new SettingsPage());
    }
}