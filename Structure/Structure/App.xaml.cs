using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Structure.View;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Structure
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AddDocumentPage());
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