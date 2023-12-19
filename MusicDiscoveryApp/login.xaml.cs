namespace MusicDiscoveryApp;

public partial class login : ContentPage
{
    public login()
    {
        InitializeComponent();
    }

    void SignIn(object sender, EventArgs e)
    {
        string user = userInput.Text;
        string password = passwordInput.Text;

        //hier moet een controller gebeuren om te zien of user in de database bestaat en password klopt
        if (user != "" && password != "")
        {
            TestLabel.Text = "Forget password?";
        }
    }

    async void GoToForgetPassword(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync(nameof(ForgetPassword));
    }
    public async void GoToSignup_Clicke(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Signup());
    }

}