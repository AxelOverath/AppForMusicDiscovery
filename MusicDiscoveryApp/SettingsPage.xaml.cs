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

    void GoToAccountDetais(object sender, EventArgs e)
    {

    }

    async void Logout(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Do you want to logout?", "No", "Yes");

        if (action == "Yes")
        {
            //de code om de conectie te breken
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