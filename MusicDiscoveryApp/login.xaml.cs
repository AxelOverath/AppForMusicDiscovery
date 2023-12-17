namespace MusicDiscoveryApp;

public partial class login : ContentPage
{

	private string user;
	private string password;
	public login()
	{
		InitializeComponent();

	}

	void SignIn(object sender, EventArgs e)
	{
		user = userInput.Text;
		password = passwordInput.Text;

		TestLabel.Text = user;

    }
}