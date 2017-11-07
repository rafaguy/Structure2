using Structure.Data;
using Structure.Utils;
using Structure.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Structure.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public DatabaseAccess DatabaseAccess { get; set; }
        public HomePage()
        {
            DatabaseAccess = new DatabaseAccess();
            InitializeComponent();
            SetHomeURI();
            BindingContext = new HomeViewModel
            {
                NewComCount = GlobalCommunicationDataSource.CurrentNewComNumber
            };


        }

        public void SetHomeURI()
        {
           /* string clientKey = DatabaseAccess.GetClientKey();
            string cultureInfo = DatabaseAccess.GetCultureInfo();
            string URI = Constants.HomeURI + clientKey + "&langueinit=" + cultureInfo;
            HomeWebView.Source = URI;*/
        }

        private void ArrowBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        #region Footer Navigations
        private void BtnDoc_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new DocumentsPage());

        }

        private void BtnComm_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NotificationCommunicationPage());

        }

        private void BtnHelp_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new WebPage());
        }

        private void BtnConfig_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ConfigurationPage());

        }

        private void BtnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
        #endregion
    }
}