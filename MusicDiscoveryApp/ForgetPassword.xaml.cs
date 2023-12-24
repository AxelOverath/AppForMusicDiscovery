namespace MusicDiscoveryApp;

public partial class ForgetPassword : ContentPage
{
	private string userEmail = "";
	public ForgetPassword()
	{
		InitializeComponent();
	}

	void SendEmail(object sender, EventArgs e)
	{
		userEmail = email.Text;

		//een controle maken om te zien of de email bestaat in de data base

		if (userEmail != "@gmail")
		{
			info.Text = "email bestaat niet in onze app";
		}
		else
		{
			info.Text = "Email gestuurd";
		}

    }
}