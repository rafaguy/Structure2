using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Structure.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
           
			InitializeComponent ();
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