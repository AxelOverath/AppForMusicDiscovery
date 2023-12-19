using MongoDB.Driver;

namespace MusicDiscoveryApp;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }

    public async void SignIn(object sender, EventArgs e)
    {
        string identifier = identifierInput.Text;
        string password = passwordInput.Text;

        if (identifier == null || password == null)
        {
            forgotPasswordLabel.Text = "Please enter both email/username and password!";
            return;
        }

        var existingUser = await CheckIfUserExists(identifier);

        if (existingUser != null && BCrypt.Net.BCrypt.Verify(password, existingUser.Password))
        {
            if (AnyUserInfoIsNull(existingUser))
            {
                forgotPasswordLabel.Text = "Some user information is missing. Redirecting to complete registration.";
                await Navigation.PushAsync(new RegisterInfo(existingUser.Email));
            }
            else
            {
                forgotPasswordLabel.Text = "Login Successful!";
                await Navigation.PushAsync(new Swipepage());
            }
        }
        else
        {
            forgotPasswordLabel.Text = "Incorrect email/username or password!";
        }
    }

    private async Task<User> CheckIfUserExists(string identifier)
    {
        var filter = Builders<User>.Filter.Where(u => u.Email == identifier || u.Username == identifier);
        var existingUser = await Database.UsersCollection.Find(filter).FirstOrDefaultAsync();
        return existingUser;
    }

    private bool AnyUserInfoIsNull(User user)
    {
        // Check for null values in user information
        return string.IsNullOrEmpty(user.FirstName)
            || string.IsNullOrEmpty(user.LastName)
            || string.IsNullOrEmpty(user.Username)
            || user.DateOfBirth == default(DateTime);
    }

    async void GoToForgetPassword(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ForgetPassword());
    }
    public async void GoToSignup_Clicke(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Signup());
    }
}