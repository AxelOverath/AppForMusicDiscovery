
namespace MusicDiscoveryApp;

public partial class login : ContentPage
{
	public login()
	{
		InitializeComponent();

	}
    public async void GoToSignup_Clicke(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Signup());
    }
    
}