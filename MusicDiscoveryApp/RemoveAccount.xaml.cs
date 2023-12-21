using System;
using System.Threading.Tasks;
using System.Windows;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MusicDiscoveryApp;

public partial class RemoveAccount : ContentPage
{
	public RemoveAccount()
	{
		InitializeComponent();
	}

	private async void RemoveAccount_Clicked(object sender, EventArgs e)
	{
        // Get the username of the account you want to remove
        string usernameToRemove = "Keelen"; // Replace with the actual username

        // Call the method to remove the account from the database
        await RemoveUserAsync(usernameToRemove);

        await DisplayAlert("Success", "Account removed successfully.", "OK");
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

	
