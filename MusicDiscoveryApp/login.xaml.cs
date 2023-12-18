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

		//hier moet een controller gebeuren om te zien of user in de database bestaat en password klopt
		if (user != "Henrique3040" && password != "050205") {

            TestLabel.Text = "Forget password?";

        }


    }

	async void GoToForgetPassword(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(ForgetPassword));
	}
}