using MongoDB.Driver;

namespace MusicDiscoveryApp;

public partial class FriendRequests : ContentPage
{
    public FriendRequests()
    {
        InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
        PopulateFriends();
    }

    private async void ArrowImage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FriendList());
    }

    private async void AddFriends_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FriendAdd());
    }

    private async void PopulateFriends()
    {
        var currentUserUsername = UserStorage.storedUsername;

        var currentUser = await GetUserByUsername(currentUserUsername);

        if (currentUser != null && currentUser.FriendRequests != null && currentUser.FriendRequests.Any())
        {
            foreach (var friendUsername in currentUser.FriendRequests)
            {
                Frame frame = CreateFriendFrame(friendUsername);
                FriendRequestsStackLayout.Children.Add(frame);
            }
        }
        else
        {
            FriendRequestsStackLayout.Children.Add(new Label { Text="No friend requests.", HorizontalOptions = LayoutOptions.Center});
        }
    }

    private Frame CreateFriendFrame(string friendUsername)
    {
        Frame frame = new Frame
        {
            BackgroundColor = Colors.LightGray,
            Margin = new Thickness(5, 5, 5, 5),
            Padding = new Thickness(10),
            CornerRadius = 50,
            HasShadow = true,
            HeightRequest = 80
        };

        Label nameLabel = new Label
        {
            Text = friendUsername,
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };

        // "Accept" image button
        var acceptImage = new Image
        {
            Source = "accept.svg",
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            HeightRequest = 30,
            WidthRequest = 30
        };

        var acceptTapGesture = new TapGestureRecognizer();
        acceptTapGesture.Tapped += async (sender, e) =>
        {
            var currentUserUsername = UserStorage.storedUsername;
            var currentUser = await GetUserByUsername(currentUserUsername);

            if (currentUser != null)
            {
                // Remove friend request from current user's list
                currentUser.FriendRequests?.Remove(friendUsername);

                // Add the friend to the current user's friend list
                if (currentUser.Friends == null)
                {
                    currentUser.Friends = [];
                }
                currentUser.Friends.Add(friendUsername);

                // Update current user's information in the database
                var filterCurrentUser = Builders<User>.Filter.Eq(u => u.Username, currentUser.Username);
                var updateCurrentUser = Builders<User>.Update
                    .Set(u => u.FriendRequests, currentUser.FriendRequests)
                    .Set(u => u.Friends, currentUser.Friends);

                await Database.UsersCollection.UpdateOneAsync(filterCurrentUser, updateCurrentUser);

                // Now, update the sender's (friendUsername) friend list
                var senderUser = await GetUserByUsername(friendUsername);

                if (senderUser != null)
                {
                    if (senderUser.Friends == null)
                    {
                        senderUser.Friends = [];
                    }
                    senderUser.Friends.Add(currentUserUsername);

                    // Update sender's information in the database
                    var filterSenderUser = Builders<User>.Filter.Eq(u => u.Username, senderUser.Username);
                    var updateSenderUser = Builders<User>.Update.Set(u => u.Friends, senderUser.Friends);

                    await Database.UsersCollection.UpdateOneAsync(filterSenderUser, updateSenderUser);
                }

                await DisplayAlert("Accept", $"Accepted friend request from {friendUsername}.", "OK");
            }
        };

        acceptImage.GestureRecognizers.Add(acceptTapGesture);

        // "Deny" image button
        var denyImage = new Image
        {
            Source = "deny.svg",
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            HeightRequest = 30,
            WidthRequest = 30
        };

        var denyTapGesture = new TapGestureRecognizer();
        denyTapGesture.Tapped += async (sender, e) =>
        {
            var currentUserUsername = UserStorage.storedUsername;
            var currentUser = await GetUserByUsername(currentUserUsername);

            if (currentUser != null)
            {
                currentUser.FriendRequests?.Remove(friendUsername);

                var filter = Builders<User>.Filter.Eq(u => u.Username, currentUser.Username);
                var update = Builders<User>.Update.Set(u => u.FriendRequests, currentUser.FriendRequests);

                await Database.UsersCollection.UpdateOneAsync(filter, update);
                await DisplayAlert("Deny", $"Denied friend request from {friendUsername}.", "OK");
            }
        };
        denyImage.GestureRecognizers.Add(denyTapGesture);

        frame.Content = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Children =
            {
                nameLabel,
                acceptImage,
                denyImage
            }
        };

        return frame;
    }

    private async Task<User?> GetUserByUsername(string username)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Username, username);
        var user = await Database.UsersCollection.Find(filter).FirstOrDefaultAsync();
        return user;
    }
}
