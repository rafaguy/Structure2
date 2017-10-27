using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Structure.Utils;
using Structure.View;
using Structure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Structure
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ConfigurationPage());
            Device.StartTimer(TimeSpan.FromMinutes(5), () =>  UpdateListNotification());
          //  Properties["old"] = 0;
        }

        private   bool UpdateListNotification()
        {
           Task t= Task.Run(async () => GlobalCommunicationDataSource.Messages = await MessagingPageViewModel.getListCommunication());
            Task.Run(async () => GlobalCommunicationDataSource.Notification = await NotificationCommunicationPage.getListNotification());
            

          /*  if(t.IsCompleted)
            {
                Properties["newCom"] = GlobalCommunicationDataSource.Messages.Where(x => x.Position.Equals("L")).ToList().Count;
                if ((int)Properties["newCom"] != (int)Properties["old"])
                {

                    Properties["count"] = (int)Properties["newCom"] - (int)Properties["old"];
                    Properties["old"] = Properties["newCom"];
                }
            }
            */


            return true;
        }

        public Page CurrentPage
        {
            get
            {
                var page = MainPage.Navigation.NavigationStack;
                var currentPage = page.LastOrDefault();
                return currentPage;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts  
            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }
        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Type currentPage = this.MainPage.GetType();
            if (e.IsConnected && currentPage != typeof(ConfigurationPage)) this.MainPage = new ConfigurationPage();
            else if (!e.IsConnected && currentPage != typeof(ConfigurationPage)) this.MainPage = new ConfigurationPage();
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}