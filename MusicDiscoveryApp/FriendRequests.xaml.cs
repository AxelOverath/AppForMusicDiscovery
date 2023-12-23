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

    private void PopulateFriends()
    {

        string[] friends = {
        "Emma", "Liam", "Olivia", "Noah", "Ava",
        "William", "Sophia", "James", "Isabella", "Oliver",
        "Mia", "Benjamin", "Charlotte", "Elijah", "Amelia",
        "Lucas", "Harper" };
        foreach (string friend in friends)
        {
            Frame frame = new Frame
            {
                BackgroundColor = Colors.LightGray,
                Margin = new Thickness(5, 5, 5, 5),
                Padding = new Thickness(0, 10),
                CornerRadius = 50,
                HasShadow = true,
                HeightRequest = 80
            };

            Label nameLabel = new Label
            {
                Text = friend,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Image acceptImage = new Image
            {
                Source = "accept.svg",
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(0, 0, 10, 0)
            };

            Image denyImage = new Image
            {
                Source = "deny.svg",
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(0, 0, 30, 0)
            };

            TapGestureRecognizer acceptTap = new TapGestureRecognizer();
            acceptTap.Tapped += async (sender, e) =>
            {
                await DisplayAlert("Accept", friend, "OK");
                // TODO
            };
            acceptImage.GestureRecognizers.Add(acceptTap);

            TapGestureRecognizer denyTap = new TapGestureRecognizer();
            denyTap.Tapped += async (sender, e) =>
            {
                await DisplayAlert("Deny", friend, "OK");
                // TODO
            };
            denyImage.GestureRecognizers.Add(denyTap);

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

            FriendRequestsStackLayout.Children.Add(frame);
        }
    }
}