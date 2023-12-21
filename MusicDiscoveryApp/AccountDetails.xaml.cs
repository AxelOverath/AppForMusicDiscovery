using MongoDB.Driver;
using System.Net.Mail;

namespace MusicDiscoveryApp
{
    public partial class AccountDetails : ContentPage
    {
        public AccountDetails()
        {
            InitializeComponent();
            usernameEntry.Text = UserStorage.storedUsername;
            emailEntry.Text = UserStorage.storedEmail;
            Shell.SetTabBarIsVisible(this, false);
        }

        private async void Confirm_Clicked(object sender, EventArgs e)
        {
            string newEmail = emailEntry.Text;
            string newUsername = usernameEntry.Text;

            if (!IsValidEmail(newEmail))
            {
                await DisplayAlert("Error", "Please enter a valid email address.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(newUsername) 
                || string.IsNullOrWhiteSpace(newEmail) 
                || string.IsNullOrWhiteSpace(passwordEntry.Text) 
                || string.IsNullOrWhiteSpace(passwordConfirmationEntry.Text))
            {
                await DisplayAlert("Error", "Not all fields are filled in.", "OK");
                return;
            }

            if (passwordEntry.Text != passwordConfirmationEntry.Text)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            if (!passwordConfirmationEntry.Text.Any(char.IsUpper) || !passwordConfirmationEntry.Text.Any(char.IsDigit))
            {
                await DisplayAlert("Error", "Password must contain at least 1 uppercase character and 1 number.", "OK");
                return;
            }

            // Access the MongoDB collection using the Database class
            IMongoCollection<User> usersCollection = Database.UsersCollection;

            var filter = Builders<User>.Filter.Eq(u => u.Email, UserStorage.storedEmail);

            var update = Builders<User>.Update
                .Set(u => u.Email, newEmail)
                .Set(u => u.Username, newUsername);

            if (!string.IsNullOrEmpty(passwordEntry.Text))
            {
                update = update.Set(u => u.Password, BCrypt.Net.BCrypt.HashPassword(passwordEntry.Text));
            }

            var updateResult = await usersCollection.UpdateOneAsync(filter, update);

            if (updateResult.ModifiedCount > 0)
            {
                // Update successful
                UserStorage.storedEmail = newEmail;
                UserStorage.storedUsername = newUsername;

                await DisplayAlert("Success", "Account details updated successfully!", "OK");
            }
            else
            {
                // Update failed
                await DisplayAlert("Error", "Failed to update account details.", "OK");
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Navigate back to the previous page (SettingsPage)
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
    }
}
