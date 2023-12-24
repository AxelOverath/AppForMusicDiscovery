using MongoDB.Driver;

namespace MusicDiscoveryApp;

public partial class Swipepage : ContentPage
{
    private string CurrentSongID;
    public Swipepage()
    {
        InitializeComponent();
        GetNewSong();
    }

    private void OnButtonPressed(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            RandomButton.TextColor = Color.FromArgb("236738");
            ForYouButton.TextColor = Color.FromArgb("236738");
            button.TextColor = Color.FromArgb("#1f421e");
        }
    }

    private void OnForYouButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("For You Clicked", "You clicked the 'For You' button!", "OK");
    }

    private void OnRandomButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("Random Clicked", "You clicked the 'Random' button!", "OK");
    }

    private void OnLikeButtonClicked(object sender, EventArgs e)
    {
        SaveLikedSong();
        GetNewSong();
    }

    private void OnDislikeButtonClicked(object sender, EventArgs e)
    {
        GetNewSong();
        SaveDislikedSong();
    }

    private void OnSwipeChanging(object sender, SwipeChangingEventArgs e)
    {
        if (e.Offset > 0)
        {
            //Like

            // SaveLikedSong();
            GetNewSong();
        }
        else if (e.Offset < 0)
        {
            //Dislike

            GetNewSong();
        }
        
    }

    private void OnImageClicked(object sender, EventArgs e)
    {
        if (mediaElement.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Playing)
            mediaElement.Pause();
        else if (mediaElement.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Paused)
            mediaElement.Play();
    }

    private async void SaveLikedSong()
    {
        var currentUser = await GetUserByUsername(UserStorage.storedUsername);
        currentUser.LikedSongs.Add(CurrentSongID); // Add the song ID to LikedSongs

        // Update current user's information in the database
        var filterCurrentUser = Builders<User>.Filter.Eq(u => u.Username, currentUser.Username);
        var updateCurrentUser = Builders<User>.Update.Set(u => u.LikedSongs, currentUser.LikedSongs);

        await Database.UsersCollection.UpdateOneAsync(filterCurrentUser, updateCurrentUser);
    }

    private async void SaveDislikedSong()
    {
        var currentUser = await GetUserByUsername(UserStorage.storedUsername);

        currentUser.DislikedSongs.Add(CurrentSongID); // Add the song ID to LikedSongs

        // Update current user's information in the database
        var filterCurrentUser = Builders<User>.Filter.Eq(u => u.Username, currentUser.Username);
        var updateCurrentUser = Builders<User>.Update.Set(u => u.DislikedSongs, currentUser.DislikedSongs);

        await Database.UsersCollection.UpdateOneAsync(filterCurrentUser, updateCurrentUser);
    }

    private async Task<User?> GetUserByUsername(string username)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Username, username);
        var user = await Database.UsersCollection.Find(filter).FirstOrDefaultAsync();
        return user;
    }

    private async void GetNewSong()
    {
        int i = 0;
        // Make the API call to get a random song
        ApiCalls.ApiResponse randomSongResponse = await ApiCalls.GetRandomSong();

        // Find the first track with a preview URL
        var selectedTrack = randomSongResponse?.Tracks?.FirstOrDefault(track => !string.IsNullOrEmpty(track.PreviewUrl));

        while (selectedTrack == null && i != 5)
        {
            // No track with a preview URL found, make another API call
            randomSongResponse = await ApiCalls.GetRandomSong();
            selectedTrack = randomSongResponse?.Tracks?.FirstOrDefault(track => !string.IsNullOrEmpty(track.PreviewUrl));
            i++;
        }

        // Update the UI with the received information
        TrackImage.Source = selectedTrack?.Album?.Images?[0]?.Url;
        SongName.Text = "Title: " + selectedTrack?.Name;
        ArtistName.Text = "Artist(s): " + selectedTrack?.Artists?[0]?.Name;
        AlbumName.Text = "Album: " + selectedTrack?.Album?.Name;
        CurrentSongID = selectedTrack?.Id;

        // Set the MediaElement source only if the track has a preview
        if (!string.IsNullOrWhiteSpace(selectedTrack?.PreviewUrl))
        {
            mediaElement.Source = selectedTrack.PreviewUrl;
        }
        else
        {
            // Handle the case where there is no preview available
            mediaElement.IsVisible = false;
        }
    }
}