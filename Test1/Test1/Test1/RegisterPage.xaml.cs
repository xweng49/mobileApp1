using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.Data;
using Test1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Test1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        Client client;
        public RegisterPage(Client client)
        {
            this.client = client;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void CompletedRegister(object sender, EventArgs e)
        {
            User user = new User();
            int results;
            if (userId.Text != "" && fName.Text != "" && lName.Text != "" && password.Text != ""
                && userId.Text != null && fName.Text != null && lName.Text != null && password.Text != null)
            {
                user.username = userId.Text;
                user.password = password.Text;
                user.firstname = fName.Text;
                user.lastname = lName.Text;
                using (UserDialogs.Instance.Loading("Loading"))
                {
                    results = (int)await client.RegisterNewUser(user);
                }
                if (results == 0)
                {
                    await DisplayAlert("Alert", "Successfully Registered", "OK");
                }
                else if (results == 1)
                {
                    await DisplayAlert("Alert", "Username already taken", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Alert", "Network Error", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alert", "Please fill in all the field", "OK");
            }


        }

        async void CancelRegistration(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}