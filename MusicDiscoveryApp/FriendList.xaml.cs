namespace MusicDiscoveryApp;

public partial class FriendList : ContentPage
{
    public FriendList()
	{
		InitializeComponent();
        PopulateFriends();
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

            Image shareImage = new Image
            {
                Source = "share.svg",
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(0, 0, 30, 0)
            };

            TapGestureRecognizer shareTap = new TapGestureRecognizer();
            shareTap.Tapped += async (sender, e) =>
            {
                await DisplayAlert("DEBUG", friend, "OK");
                //TODO
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

            FriendsStackLayout.Children.Add(frame);
        }
    }

    public async void GoToFriendAdd_Clicked(object sender, EventArgs e)
	{
		var button = (Button)sender;
		button.IsEnabled = false;
        await Navigation.PushAsync(new FriendAdd());
		button.IsEnabled = true;
    }
}