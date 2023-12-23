using System.Diagnostics;

namespace MusicDiscoveryApp;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
    }


	void GoToSwipeFilter(object sender, EventArgs e)
	{

	}


    void GoToBlockArtist(object sender, EventArgs e)
    {

    }

    async void GoToAccountDetais(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AccountDetails());
    }

    async void Logout(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Do you want to logout?", "No", "Yes");

        if (action == "Yes")
        {
            UserStorage.Clear();
            await Navigation.PushAsync(new Login());
        }
        else
        {
            return;
        }
    }


    async void RemoveAccount(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Do you want to remove you account?", "No", "Yes");
        Debug.WriteLine(action);

      
        if (action == "Yes")
        {
            //Hier in dan zou de actie om de account te verwijderen moeten gebeuren.
        }
    }
}