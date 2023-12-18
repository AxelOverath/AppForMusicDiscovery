using MongoDB.Driver;
using System.Net.Mail;

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
        string confirmPassword = PasswordConfirm.Text;

        if (!IsValidEmail(email))
        {
            EmailErrorLabel.Text = "This is not a valid email!";
            return;
        } else EmailErrorLabel.Text = string.Empty;

        if (!password.Any(char.IsUpper) || !password.Any(char.IsDigit))
        {
            PasswordErrorLabel.Text = "Password must contain at least 1 uppercase letter and 1 number!";
            return;
        } else PasswordErrorLabel.Text = string.Empty;

        if (confirmPassword != password)
        {
            PasswordErrorLabel2.Text = "Passwords do not match!";
            return;
        } else PasswordErrorLabel2.Text = string.Empty;

        // Check if the user already exists in the database
        var existingUser = await CheckIfUserExists(email);
        if (existingUser != null)
        {
            EmailErrorLabel.Text = "This email is already registered!";
            return;
        }

        // If the user doesn't exist, you can now proceed to insert the user into the database
        var newUser = new User { Email = email, Password = password };
        await Database.UsersCollection.InsertOneAsync(newUser);

        // Optionally, you can navigate to the next page or display a success message
        await Navigation.PushAsync(new RegisterInfo());
    }

    // Helper method to validate email format
    private bool IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private async Task<User> CheckIfUserExists(string email)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Email, email);
        var existingUser = await Database.UsersCollection.Find(filter).FirstOrDefaultAsync();
        return existingUser;
    }
}