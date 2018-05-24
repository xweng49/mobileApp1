using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.Data;
using Xamarin.Forms;
using Test1.Model;
using Acr.UserDialogs;


namespace Test1
{
	public partial class MainPage : ContentPage
	{
        static int loginCode = 0;
        Client client;
		public MainPage(IService Aservice)
		{
            client = new Client(Aservice);
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
		}

        async void RegisterButton(object sender, EventArgs args)
        {
            login_password.Text = "";
            await Navigation.PushAsync(new RegisterPage(client));
        }

        async void LoginButton(object sender, EventArgs args)
        {
            User user = new User();
            bool loginSuccess = true;
            user.username = login_id.Text;
            user.password = login_password.Text;
            using (UserDialogs.Instance.Loading("Loading"))
            {
                loginSuccess = await client.Login(user);
            }
            if (loginSuccess)
            {
                user.password = "";
                login_password.Text = "";
                loginCode = 0;
            }
            else
            {
                loginCode = 1;
            }

            switch (loginCode)
            {
                case 0:
                    await DisplayAlert("Alert", "Login Successful", "OK");
                    await Navigation.PushAsync(new JobPage(client));
                    break;
                case 1:
                    await DisplayAlert("Alert", "Login Failed", "OK");
                    break;
                case 2:
                    await DisplayAlert("Alert", "Connection Failed", "OK");
                    break;
                default:
                    await DisplayAlert("Alert", "System Error", "OK");
                    break;
            }
        }
    }
}
