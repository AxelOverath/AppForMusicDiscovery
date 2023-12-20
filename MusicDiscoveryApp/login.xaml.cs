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
            await DisplayAlert("Error", "Please enter both email and password.", "OK"); return;
        }

        var existingUser = await CheckIfUserExists(identifier);

        if (existingUser != null && BCrypt.Net.BCrypt.Verify(password, existingUser.Password))
        {
            if (AnyUserInfoIsNull(existingUser))
            {
                await Navigation.PushAsync(new RegisterInfo());
            }
            else
            {
                UserStorage.storedUsername = existingUser.Username;
                UserStorage.storedEmail = existingUser.Email;
                await Navigation.PushAsync(new Swipepage());
            }
        }
        else
        {
            await DisplayAlert("Error", "Email or password is wrong", "OK");
            return;
        }
    }

    private async Task<User> CheckIfUserExists(string identifier)
    {
        var filter = Builders<User>.Filter.Where(u => u.Email == identifier);
        var existingUser = await Database.UsersCollection.Find(filter).FirstOrDefaultAsync();
        return existingUser;
    }

    private bool AnyUserInfoIsNull(User user)
    {
        // Check for null values in user information
        return string.IsNullOrEmpty(user.FirstName)
            || string.IsNullOrEmpty(user.LastName)
            || string.IsNullOrEmpty(user.Username)
            || user.DateOfBirth == default;
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