namespace MusicDiscoveryApp;

public partial class Signup : ContentPage
{
	public Signup()
	{
		InitializeComponent();
	}
    public async void GoToMainPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterInfo());
    }
}