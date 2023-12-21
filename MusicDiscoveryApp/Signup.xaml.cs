using MongoDB.Driver;
using System.Net.Mail;

namespace MusicDiscoveryApp;

public partial class Signup : ContentPage
{
    public Signup()
    {
        InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);

    }
    public async void GoToLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }
    public async void GoToRegisterInfo_Clicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        string confirmPassword = PasswordConfirm.Text;

        if (!IsValidEmail(email))
        {
            ErrorLabel.Text = "This is not a valid email!";
            return;
        }
        else ErrorLabel.Text = string.Empty;

        if (password != null && confirmPassword != null)
        {
            if (!password.Any(char.IsUpper) || !password.Any(char.IsDigit))
            {
                ErrorLabel.Text = "Password must contain at least 1 uppercase letter and 1 number!";
                return;
            }
            else ErrorLabel.Text = string.Empty;
        }

        else ErrorLabel.Text = string.Empty;

        if (confirmPassword != password)
        {
            ErrorLabel.Text = "Passwords do not match!";
            return;
        }
        else ErrorLabel.Text = string.Empty;


        // Check if the user already exists in the database
        var existingUser = await CheckIfUserExists(email);
        if (existingUser != null)
        {
            ErrorLabel.Text = "This email is already registered!";
            return;
        }

        UserStorage.storedEmail = email;

        // If the user doesn't exist, you can now proceed to insert the user into the database
        await Database.UsersCollection.InsertOneAsync(new User { Email = email, Password = hashedPassword});

        await Navigation.PushAsync(new RegisterInfo());
    }

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