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
        await Navigation.PopAsync();
    }

    private void FriendRequests_Clicked(object sender, EventArgs e)
    {
        // TODO
    }

    private void AddFriends_Clicked(object sender, EventArgs e)
    {
        // TODO
    }
}