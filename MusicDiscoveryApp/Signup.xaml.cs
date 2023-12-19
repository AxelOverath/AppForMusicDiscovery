namespace MusicDiscoveryApp;

public partial class Signup : ContentPage
{
	public Signup()
	{
		InitializeComponent();
	}
    public async void GoToMainPage_Clicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = PasswordConfirm.Text;

        if (!IsValidEmail(email))
        {
            ErrorLabel.Text = "This is not a valid email!";
            return;
        } else ErrorLabel.Text = string.Empty;

        if (password != null && confirmPassword != null)
        {
            if (!password.Any(char.IsUpper) || !password.Any(char.IsDigit))
            {
                ErrorLabel.Text = "Password must contain at least 1 uppercase letter and 1 number!";
                return;
            }
            else ErrorLabel.Text = string.Empty;
        }
        else return;

        if (confirmPassword != password)
        {
            ErrorLabel.Text = "Passwords do not match!";
            return;
        } else ErrorLabel.Text = string.Empty;

        // Check if the user already exists in the database
        var existingUser = await CheckIfUserExists(email);
        if (existingUser != null)
        {
            ErrorLabel.Text = "This email is already registered!";
            return;
        }

        // If the user doesn't exist, you can now proceed to insert the user into the database
        var newUser = new User { Email = email, Password = password };
        await Database.UsersCollection.InsertOneAsync(newUser);

        // Optionally, you can navigate to the next page or display a success message
        await Navigation.PushAsync(new RegisterInfo());
    }
}