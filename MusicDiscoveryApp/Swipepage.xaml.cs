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

    private void OnLikeButtonClicked(object sender, EventArgs e)
    {
        TrackImage.Source = "https://i.scdn.co/image/ab67616d0000b2734718e2b124f79258be7bc452";
        SongName.Text = "Party Monster";
        ArtistName.Text = "The Weeknd";
        AlbumName.Text = "Starboy (Deluxe)";
    }

    private void OnDislikeButtonClicked(object sender, EventArgs e)
    {
        TrackImage.Source = "https://i.scdn.co/image/ab67616d0000b273a022feadbd7635c6cee11ef9";
        SongName.Text = "Erop Of Eronder";
        ArtistName.Text = "Pommelien Thijs";
        AlbumName.Text = "Per Ongeluk";
    }

    private void OnSwipeChanging(object sender, SwipeChangingEventArgs e)
    {
        if (e.Offset > 0)
        {
            TrackImage.Source = "https://i.scdn.co/image/ab67616d0000b2734718e2b124f79258be7bc452";
            SongName.Text = "Party Monster";
            ArtistName.Text = "The Weeknd";
            AlbumName.Text = "Starboy (Deluxe)";
        }
        else if (e.Offset < 0)
        {
            TrackImage.Source = "https://i.scdn.co/image/ab67616d0000b273a022feadbd7635c6cee11ef9";
            SongName.Text = "Erop Of Eronder";
            ArtistName.Text = "Pommelien Thijs";
            AlbumName.Text = "Per Ongeluk";
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