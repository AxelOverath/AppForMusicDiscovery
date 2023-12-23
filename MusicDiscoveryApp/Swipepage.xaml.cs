using CommunityToolkit.Maui.Views;

namespace MusicDiscoveryApp;

public partial class Swipepage : ContentPage
{
    private string CurrentSongID;
    public Swipepage()
    {
        InitializeComponent();
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

    private async void OnLikeButtonClicked(object sender, EventArgs e)
    {
        SaveLikedSong();
        GetNewSong();
    }

    private async void OnDislikeButtonClicked(object sender, EventArgs e)
    {
        GetNewSong();
    }

    private async void OnSwipeChanging(object sender, SwipeChangingEventArgs e)
    {
        if (e.Offset > 0)
        {
            //Like
            SaveLikedSong();
            GetNewSong();
        }
        else if (e.Offset < 0)
        {
            //Dislike
            GetNewSong();
        }
        swipeView.Close();
    }

    private void OnImageClicked(object sender, EventArgs e)
    {
        if (mediaElement.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Playing)
            mediaElement.Pause();
        else if (mediaElement.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Paused)
            mediaElement.Play();
    }

    private void SaveLikedSong()
    {
        //Sent id from song and username of client
        //The id is in CurrentSongID
        CurrentSongID = CurrentSongID;
    }

    private async void GetNewSong()
    {
        // Make the API call to get a random song
        ApiCalls.ApiResponse randomSongResponse = await ApiCalls.GetRandomSong();

        // Find the first track with a preview URL
        var selectedTrack = randomSongResponse?.Tracks?.FirstOrDefault(track => !string.IsNullOrEmpty(track.PreviewUrl));

        while (selectedTrack == null)
        {
            // No track with a preview URL found, make another API call
            randomSongResponse = await ApiCalls.GetRandomSong();
            selectedTrack = randomSongResponse?.Tracks?.FirstOrDefault(track => !string.IsNullOrEmpty(track.PreviewUrl));
        }

        // Update the UI with the received information
        TrackImage.Source = selectedTrack?.Album?.Images?[0]?.Url;
        SongName.Text = selectedTrack?.Name;
        ArtistName.Text = selectedTrack?.Artists?[0]?.Name;
        AlbumName.Text = selectedTrack?.Album?.Name;
        CurrentSongID = selectedTrack?.Id;

        // Set the MediaElement source only if the track has a preview
        if (!string.IsNullOrEmpty(selectedTrack.PreviewUrl))
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