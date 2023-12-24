namespace MusicDiscoveryApp;

using Microsoft.Maui.Controls;

public partial class TempLiabrary : ContentPage
{
    //AANmaken van list waar de songs in komen
    //Voorbeeld SONGS
    public TempLiabrary()
    {
        InitializeComponent();

        //SONG.clear

        //Get all songsID's with username of client

        //SONGS = Get all songs with ID

        //foreach(song in songs)
        //{
        //    MakeNewSongDisplay(song.Cover, song.Title, song.Artist, song.Album, song.ID)
        //}

        //MakeNewSongDisplay(Cover, Title, Artist, Album)
    }

    private void MakeNewSongDisplay(string Cover,string Title, string Artist, string Album, string ID)
    {
        // Hoofdverticaal stacklayout met padding, breedte, hoogte en achtergrondkleur
        VerticalStackLayout mainVerticalStackLayout = new()
        {
            Padding = new Thickness(10),
            WidthRequest = 210,
            HeightRequest = 320,
            BackgroundColor = Color.FromArgb("#2db36d")
        };

        // Binnenste verticale stacklayout met een afbeelding
        VerticalStackLayout innerVerticalStackLayout1 = [];
        Image image1 = new() { Source = "https://i.imgur.com/fEGwJQl.png", WidthRequest = 125 };
        innerVerticalStackLayout1.Children.Add(image1);

        // Binnenste verticale stacklayout met een andere afbeelding en labels
        VerticalStackLayout innerVerticalStackLayout2 = new()
        {
            VerticalOptions = LayoutOptions.Center
        };

        Image coverImage = new() { Source = Cover, HeightRequest = 135, WidthRequest = 130 };


        VerticalStackLayout labelStackLayout = new()
        {
            Spacing = 10,
            Padding = new Thickness(15, 0, 0, 0)
        };

        Label songNameLabel = new() { Text = Title, HorizontalOptions = LayoutOptions.Center };
        Label artistLabel = new() { Text = Artist, HorizontalOptions = LayoutOptions.Center };
        Label albumLabel = new() { Text = Album, HorizontalOptions = LayoutOptions.Center };

        labelStackLayout.Children.Add(songNameLabel);
        labelStackLayout.Children.Add(artistLabel);
        labelStackLayout.Children.Add(albumLabel);

        innerVerticalStackLayout2.Children.Add(coverImage);
        innerVerticalStackLayout2.Children.Add(labelStackLayout);

        // Voeg de binnenste stacklayouts toe aan de hoofdstacklayout
        mainVerticalStackLayout.Children.Add(innerVerticalStackLayout1);
        mainVerticalStackLayout.Children.Add(innerVerticalStackLayout2);

        // Voeg drie knoppen toe onder de labels naast elkaar
        HorizontalStackLayout buttonStackLayout = new HorizontalStackLayout
        {
            Spacing = 10,
            Padding = new Thickness(15, 0, 0, 0)
        };

        Button button1 = new() { Text = "▶️", BackgroundColor = Color.FromArgb("#2db36d"), AutomationId = ID };
        Button button2 = new() { Text = "💾", BackgroundColor = Color.FromArgb("#2db36d"), AutomationId = ID };
        Button button3 = new() { Text = "❌", BackgroundColor = Color.FromArgb("#2db36d"), AutomationId = ID };

        button1.Clicked += (s, e) =>
        {
            if (mediaElement.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Playing)
            {
                mediaElement.Pause();
            }
                
            else if (mediaElement.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Paused)
            {
                //foreach(song in songs)
                //{
                //    if(song.ID == button1.AutomationId)
                //    {
                //        mediaElement.Source = song.urlsong
                          mediaElement.Play();
                //    }
                //}
            }
        };

        button2.Clicked += (s, e) =>
        {
            //sent to spotify playlist
        };

        button3.Clicked += (s, e) =>
        {
            //remove song uit db en reload this page
        };

        buttonStackLayout.Children.Add(button1);
        buttonStackLayout.Children.Add(button2);
        buttonStackLayout.Children.Add(button3);

        mainVerticalStackLayout.Children.Add(buttonStackLayout);

        // Voeg de hoofdstacklayout toe aan de Content van de ContentPage
        Content = mainVerticalStackLayout;
    }
}