namespace MusicDiscoveryApp;

public partial class ChangerPassword : ContentPage
{

	private string _password = "";
	private string _password2 = "";
	public ChangerPassword()
	{
		InitializeComponent();
	}



	void Confirm (object sender, EventArgs e)
    {

		_password = passwordInput.Text;
		_password2 = passwordInput2.Text;

		
		if (_password == _password2) {
            //al de pasworden klopen, dan na de datbase sturen

            changInfo.Text = "Password changed!";

        }
		else
		{
			changInfo.Text = "Passwords are not equals";
		}
	
	}

}