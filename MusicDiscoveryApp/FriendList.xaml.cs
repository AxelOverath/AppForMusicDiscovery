using MongoDB.Driver;

namespace MusicDiscoveryApp;

public partial class FriendList : ContentPage
{
    public FriendList()
	{
		InitializeComponent();
        PopulateFriends();
    }

    private async void PopulateFriends()
    {
        var currentUserUsername = UserStorage.storedUsername;
        var currentUser = await GetUserByUsername(currentUserUsername);

        if (currentUser != null && currentUser.Friends != null && currentUser.Friends.Any())
        {
            foreach (var friend in currentUser.Friends)
            {
                Frame frame = CreateFriendFrame(friend);
                FriendsStackLayout.Children.Add(frame);
            }
        }
        else
        {
            FriendsStackLayout.Children.Add(new Label { Text = "No friends yet.", HorizontalOptions = LayoutOptions.Center });
        }
    }

    private Frame CreateFriendFrame(string friend)
    {
        Frame frame = new()
        {
            BackgroundColor = Colors.LightGray,
            Margin = new Thickness(5, 5, 5, 5),
            Padding = new Thickness(0, 10),
            CornerRadius = 50,
            HasShadow = true,
            HeightRequest = 80
        };

        Label nameLabel = new()
        {
            Text = friend,
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };

        Image shareImage = new()
        {
            Source = "share.svg",
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Margin = new Thickness(0, 0, 30, 0)
        };

        TapGestureRecognizer shareTap = new();
        shareTap.Tapped += async (sender, e) =>
        {
            await DisplayAlert("DEBUG", friend, "OK");
            //TODO: Handle share action
        };
        shareImage.GestureRecognizers.Add(shareTap);

        frame.Content = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Children =
        {
            nameLabel,
            shareImage
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


    public async void GoToFriendAdd_Clicked(object sender, EventArgs e)
	{
		var button = (Button)sender;
		button.IsEnabled = false;
        await Navigation.PushAsync(new FriendAdd());
		button.IsEnabled = true;
    }
}