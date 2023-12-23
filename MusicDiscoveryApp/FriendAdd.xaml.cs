namespace MusicDiscoveryApp;

public partial class FriendAdd : ContentPage
{
	public FriendAdd()
	{
		InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
    }

    private async void ArrowImage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FriendList());
    }

    private async void FriendRequests_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FriendRequests());
    }
}