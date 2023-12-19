namespace MusicDiscoveryApp;

public partial class RegisterInfo : ContentPage
{
	public RegisterInfo()
	{
		InitializeComponent();
	}
    public async void GoToSwipePage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Swipepage());
    }
}