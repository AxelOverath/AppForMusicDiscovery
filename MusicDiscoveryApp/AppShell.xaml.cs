namespace MusicDiscoveryApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            GoToLogin();
        }

        private async void GoToLogin()
        {
            await Navigation.PushAsync(new Login());
        }
    }
}
