namespace MusicDiscoveryApp;

public partial class Signup : ContentPage
{
	public Signup()
	{
		InitializeComponent();
	}
    public async void GoToLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new login());
    }
    public async void GoToRegisterInfo_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterInfo());
    }
}