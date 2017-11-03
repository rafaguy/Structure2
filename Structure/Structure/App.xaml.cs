using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Structure.Interface;
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
            UpdateListNotification();
            Device.StartTimer(TimeSpan.FromMinutes(1), () =>  UpdateListNotification());
         
        }
        // This Method is called every 5 min, wich the API for the new Notification and Communication
        //GlobalCommunicationDataSource.Messages is the listview item source for le NotificationCommunicationPage

        private bool UpdateListNotification()
        {
           Task.Run(async () => { GlobalCommunicationDataSource.Messages = await MessagingPageViewModel.getListCommunication();
               GlobalCommunicationDataSource.CommunicationNumberNotViewed = GlobalCommunicationDataSource.Messages.Where(x => x.Position.Equals("L")).Count();
               Application.Current.Properties["NumberNotViewed"] = GlobalCommunicationDataSource.CommunicationNumberNotViewed;
               Application.Current.Properties["NumberViewed"] = GlobalCommunicationDataSource.CommunicationNumberViewed;
               if (GlobalCommunicationDataSource.CommunicationNumberNotViewed != GlobalCommunicationDataSource.CommunicationNumberViewed)
               {
                   (CurrentPage.BindingContext as IPageNotification).NewComCount = GlobalCommunicationDataSource.CommunicationNumberNotViewed - GlobalCommunicationDataSource.CommunicationNumberViewed;
               }
           });
            Task.Run(async () => GlobalCommunicationDataSource.Notification = await NotificationCommunicationPage.getListNotification());
            
          
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
            if (Application.Current.Properties.ContainsKey("NumberNotViewed")  && Application.Current.Properties.ContainsKey("NumberViewed"))
            {
                GlobalCommunicationDataSource.CommunicationNumberNotViewed = (int)Application.Current.Properties["NumberNotViewed"];
                GlobalCommunicationDataSource.CommunicationNumberViewed = (int)Application.Current.Properties["NumberViewed"];
            }

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
            if (Application.Current.Properties.ContainsKey("NumberNotViewed") && Application.Current.Properties.ContainsKey("NumberViewed"))
            {
                GlobalCommunicationDataSource.CommunicationNumberNotViewed = (int)Application.Current.Properties["NumberNotViewed"];
                GlobalCommunicationDataSource.CommunicationNumberViewed = (int)Application.Current.Properties["NumberViewed"];
            }
        }
    }
}