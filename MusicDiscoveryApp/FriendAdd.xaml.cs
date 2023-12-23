using MongoDB.Driver;

namespace MusicDiscoveryApp
{
    public partial class FriendAdd : ContentPage
    {
        public FriendAdd()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            SearchBar.TextChanged += SearchBarOnTextChanged;
        }

        private async void ArrowImage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendList());
        }

        private async void FriendRequests_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendRequests());
        }

        private async void SearchBarOnTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;

            // Perform a search in the database for usernames that match the search text
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filter = Builders<User>.Filter.Regex(u => u.Username, new MongoDB.Bson.BsonRegularExpression(searchText, "i"));
                var users = await Database.UsersCollection.Find(filter).ToListAsync();

                // Clear previous entries in the UI
                AddFriendsStackLayout.Children.Clear();

                // Display usernames that match the search text in the UI
                foreach (var user in users)
                {
                    if (user.Username != UserStorage.storedUsername)
                    {
                        var frame = new Frame
                        {
                            BackgroundColor = Colors.LightGray,
                            Margin = new Thickness(5, 5, 5, 5),
                            Padding = new Thickness(0, 10),
                            CornerRadius = 50,
                            HasShadow = true,
                            HeightRequest = 80
                        };

                        var nameLabel = new Label
                        {
                            Text = user.Username,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            FontSize = 16,
                            TextColor = Colors.Black,
                            Margin = new Thickness(0, 5)
                        };

                        var addImage = new Image
                        {
                            Source = "add.svg",
                            HorizontalOptions = LayoutOptions.End,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Margin = new Thickness(0, 0, 30, 0)
                        };

                        TapGestureRecognizer addTap = new TapGestureRecognizer();
                        addTap.Tapped += async (sender, args) =>
                        {
                            var currentUserUsername = UserStorage.storedUsername;

                            // Get the tapped user's information
                            var tappedUser = user;

                            var filter = Builders<User>.Filter.Eq(u => u.Username, tappedUser.Username);
                            var update = Builders<User>.Update.AddToSet(u => u.FriendRequests, currentUserUsername);

                            var updateResult = await Database.UsersCollection.UpdateOneAsync(filter, update);

                            if (updateResult.IsModifiedCountAvailable && updateResult.ModifiedCount > 0)
                            {
                                await DisplayAlert("Success", $"Requested {tappedUser.Username} to be your friend.", "OK");
                            }
                            else
                            {
                                await DisplayAlert("Error", "Failed to send friend request.", "OK");
                            }
                        };

                        addImage.GestureRecognizers.Add(addTap);

                        frame.Content = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                nameLabel,
                                addImage
                            }
                        };

                        AddFriendsStackLayout.Children.Add(frame);
                    }
                }
            }
            else
            {
                AddFriendsStackLayout.Children.Clear();
            }
        }

    }
}
