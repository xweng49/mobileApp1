using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class JobPage : ContentPage
	{
        ObservableCollection <Job> jobs;
        Client client;
        public JobPage(Client client)
        {
            //Task<List<Job>>
            InitializeComponent();

            jobs = new ObservableCollection <Job>();
            this.client = client;
            GetJob(client);

            NavigationPage.SetHasNavigationBar(this, false);
            listView.ItemsSource = jobs;
        }
        async void AddJobButton(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddJobsPage(client));
        }
        void GetJob(Client client)
        {
            jobs.Clear();
            var Results = Task.Run(() => client.GetJobs()).Result;

            foreach (var job in Results)
            {
                jobs.Add(job);
            }
        }


    }
}