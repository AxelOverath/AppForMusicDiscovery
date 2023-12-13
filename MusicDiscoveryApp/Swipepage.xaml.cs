using Microsoft.Maui.Controls;

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
            button.TextColor = Colors.Red;
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
}