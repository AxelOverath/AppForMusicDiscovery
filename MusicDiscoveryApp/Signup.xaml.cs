using MongoDB.Driver;

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
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        // Check if the user already exists
        if (await Database.IsUserExistsAsync(email))
            {
                await DisplayAlert("Error", "User already exists. Please choose a different username.", "OK");
                return;
            }

            // If the user doesn't exist, you can now proceed to insert the user into the database
            var newUser = new User { Name = email, Password = password };
            await Database.UsersCollection.InsertOneAsync(newUser);

        // Optionally, you can navigate to the next page or display a success message
        await Navigation.PushAsync(new RegisterInfo());
        
    }
}