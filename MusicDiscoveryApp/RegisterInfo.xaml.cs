using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace MusicDiscoveryApp
{
    public partial class RegisterInfo : ContentPage
    {
        private readonly string userEmail;

        public RegisterInfo(string email)
        {
            InitializeComponent();
            DOBEntry.Date = new DateTime(2000, 1, 1);
            DOBEntry.MinimumDate = new DateTime(1900, 1, 1);
            DOBEntry.MaximumDate = DateTime.Now.AddYears(-3);
            userEmail = email;
        }

        public async void GoToSwipePage_Clicked(object sender, EventArgs e)
        {
            string firstName = firstNameEntry.Text;
            string lastName = lastNameEntry.Text;
            string username = usernameEntry.Text;
            DateTime dateOfBirth = DOBEntry.Date;

            if (firstName == null || lastName == null || username == null)
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            var existingUsername = await CheckIfUsernameExists(username);
            if (existingUsername != null)
            {
                await DisplayAlert("Error", "Username already exists. Please choose a different username.", "OK");
                return;
            }

            var filter = Builders<User>.Filter.Eq(u => u.Email, userEmail);
            var update = Builders<User>.Update
                .Set(u => u.FirstName, firstName)
                .Set(u => u.LastName, lastName)
                .Set(u => u.Username, username)
                .Set(u => u.DateOfBirth, dateOfBirth);

            await Database.UsersCollection.UpdateOneAsync(filter, update);

            await Navigation.PushAsync(new Swipepage());
        }

        private async Task<User> CheckIfUsernameExists(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            var existingUser = await Database.UsersCollection.Find(filter).FirstOrDefaultAsync();
            return existingUser;
        }
    }
}