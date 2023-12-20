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

    private void OnButtonHovered(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            
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
        DisplayAlert("For You Clicked", "You clicked the 'Like' button!", "OK");
    }

    private void OnDislikeButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("For You Clicked", "You clicked the 'Dislike' button!", "OK");
    }

    private void OnSwipeChanging(object sender, SwipeChangingEventArgs e)
    {
        if (e.Offset > 0)
        {
            // Swipe naar rechts (like)
            DisplayAlert("Like", "Je hebt naar rechts geswiped (like)!", "OK");
        }
        else if (e.Offset < 0)
        {
            // Swipe naar links (dislike)
            DisplayAlert("Dislike", "Je hebt naar links geswiped (dislike)!", "OK");
        }
        swipeView.Close();
    }
}