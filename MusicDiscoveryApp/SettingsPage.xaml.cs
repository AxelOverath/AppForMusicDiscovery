using MongoDB.Driver;
using System.Diagnostics;

namespace MusicDiscoveryApp;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);

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
            //de code om de conectie te breken
            await Navigation.PushAsync(new Login());
        }
    }

    async void RemoveAccount(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Do you want to remove you account?", "No", "Yes");
        Debug.WriteLine(action);

      
        if (action == "Yes")
        {
            //Hier in dan zou de actie om de account te verwijderen moeten gebeuren. 

            string usernameToRemove = UserStorage.storedUsername; 

            // Call the method to remove the account from the database
            await RemoveUserAsync(usernameToRemove);

            await DisplayAlert("Success", "Account removed successfully.", "OK");

            await Navigation.PushAsync(new Login());

        }
    }


    private async Task RemoveUserAsync(string username)
    {
        try
        {
            // Assuming your User class has a property "Username"
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            await Database.UsersCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error removing account: {ex.Message}", "OK");
        }
    }
}