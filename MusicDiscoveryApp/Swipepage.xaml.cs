using CommunityToolkit.Maui.Views;

namespace MusicDiscoveryApp;

public partial class Swipepage : ContentPage
{
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




    private void OnDislikeButtonClicked(object sender, EventArgs e)
    {
        TrackImage.Source = "https://i.scdn.co/image/ab67616d0000b273a022feadbd7635c6cee11ef9";
        SongName.Text = "Erop Of Eronder";
        ArtistName.Text = "Pommelien Thijs";
        AlbumName.Text = "Per Ongeluk";
    }

    private async void OnSwipeChanging(object sender, SwipeChangingEventArgs e)
    {
        // Make the API call to get a random song
        ApiCalls.ApiResponse randomSongResponse = await ApiCalls.GetRandomSong();

        // Update the UI with the received information
        if (randomSongResponse != null && randomSongResponse.Tracks != null && randomSongResponse.Tracks.Count > 0)
        {
            TrackImage.Source = randomSongResponse.Tracks[0]?.Album?.Images?[0]?.Url;
            SongName.Text = randomSongResponse.Tracks[0]?.Name;
            ArtistName.Text = randomSongResponse.Tracks[0]?.Artists?[0]?.Name;
            AlbumName.Text = randomSongResponse.Tracks[0]?.Album?.Name;
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
}