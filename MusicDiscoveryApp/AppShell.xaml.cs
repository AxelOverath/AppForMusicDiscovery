namespace MusicDiscoveryApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ForgetPassword), typeof(ForgetPassword));
        }
    }
}
