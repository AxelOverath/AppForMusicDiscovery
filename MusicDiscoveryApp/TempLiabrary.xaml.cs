using MongoDB.Driver;

namespace MusicDiscoveryApp
{
    public partial class TempLiabrary : ContentPage
    {
        public TempLiabrary()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SongsContainer.Children.Clear();
            PopulateLibrary();
        }

        private async void PopulateLibrary()
        {
            var currentUserUsername = UserStorage.storedUsername;
            var currentUser = await GetUserByUsername(currentUserUsername);
            var reversedLikedSongs = currentUser.LikedSongs.ToList();
            reversedLikedSongs.Reverse();
            foreach (var song in reversedLikedSongs)
            {
                var songDetails = await ApiCalls.GetSongDetails(song);
                if (songDetails != null)
                {
                    DisplaySongDetails(songDetails.CoverUrl, songDetails.Title, songDetails.Artist, songDetails.Album, song);
                }
            }
        }

        private async Task<User?> GetUserByUsername(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            var user = await Database.UsersCollection.Find(filter).FirstOrDefaultAsync();
            return user;
        }

        private void DisplaySongDetails(string Cover, string Title, string Artist, string Album, string ID)
        {
            var mainVerticalStackLayout = new StackLayout
            {
                Padding = new Thickness(10),
                WidthRequest = 210,
                HeightRequest = 320,
                BackgroundColor = Color.FromArgb("#2db36d")
            };

            var coverImage = new Image
            {
                Source = Cover,
                HeightRequest = 135,
                WidthRequest = 130
            };

            var songNameLabel = new Label { Text = Title, HorizontalOptions = LayoutOptions.Center };
            var artistLabel = new Label { Text = Artist, HorizontalOptions = LayoutOptions.Center };
            var albumLabel = new Label { Text = Album, HorizontalOptions = LayoutOptions.Center };

            var labelStackLayout = new StackLayout
            {
                Spacing = 10,
                Padding = new Thickness(15, 0, 0, 0)
            };

            labelStackLayout.Children.Add(songNameLabel);
            labelStackLayout.Children.Add(artistLabel);
            labelStackLayout.Children.Add(albumLabel);

            var buttonStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                Padding = new Thickness(15, 0, 0, 0)
            };

            var playButton = new Button { Text = "▶️", BackgroundColor = Color.FromArgb("#2db36d"), AutomationId = ID };
            var saveButton = new Button { Text = "💾", BackgroundColor = Color.FromArgb("#2db36d"), AutomationId = ID };
            var deleteButton = new Button { Text = "❌", BackgroundColor = Color.FromArgb("#2db36d"), AutomationId = ID };

            playButton.Clicked += (s, e) =>
            {
                DisplayAlert("PLAY", "Button to play a song", "OK");
            };

            saveButton.Clicked += (s, e) =>
            {
                DisplayAlert("SAVE", "Button to save song to your Spotify playlist", "OK");
            };

            deleteButton.Clicked += (s, e) =>
            {
                DisplayAlert("DELETE", "Button to delete this song from the library and from the database", "OK");
            };

            buttonStackLayout.Children.Add(playButton);
            buttonStackLayout.Children.Add(saveButton);
            buttonStackLayout.Children.Add(deleteButton);

            var innerVerticalStackLayout = new StackLayout();
            innerVerticalStackLayout.Children.Add(coverImage);
            innerVerticalStackLayout.Children.Add(labelStackLayout);

            mainVerticalStackLayout.Children.Add(innerVerticalStackLayout);
            mainVerticalStackLayout.Children.Add(buttonStackLayout);

            SongsContainer.Children.Add(mainVerticalStackLayout);
        }
    }
}
