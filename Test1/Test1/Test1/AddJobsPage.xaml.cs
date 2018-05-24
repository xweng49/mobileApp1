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
	public partial class AddJobsPage : ContentPage
	{
        Client client;
		public AddJobsPage (Client client)
		{
            this.client = client;
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
		}

        async void AddJob(object sender, EventArgs e)
        {
            Job newjob = new Job();
            int results;
            if (Address.Text != "" && Address.Text != null)
            {
                newjob.firstname = First_name.Text;
                newjob.lastname = Last_name.Text;
                newjob.address = Address.Text;
                newjob.description = Description.Text;
                newjob.complete = Completed.Text;
                newjob.time = Time.Text;
                newjob.assignedTime = Assigned_Time.Text;
                newjob.assignedTo = Assigned_To.Text;

                using (UserDialogs.Instance.Loading("Loading"))
                {
                    results = (int)await client.AddJob(newjob);

                }
                if (results == 0)
                {
                    await DisplayAlert("Alert", "Successfully Added Job", "OK");
                }
                else if (results == 3)
                {
                    await DisplayAlert("Alert", "Was not able to save Job", "OK");
                }
                else
                {
                    await DisplayAlert("Alert", "Network Error", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alert", "Please fill in the Address", "OK");
            }
        }

	}
}