using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Structure.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPage : ContentPage
    {
        public WebPage()
        {
            InitializeComponent();
           // Browser.Source = "https://m.facebook.com";
         //  Application.Current.MainPage
            
        }
        private void BackClicked(object sender, EventArgs e)
        {
            // Check to see if there is anywhere to go back to
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
            else
            { // If not, leave the view
                Navigation.PopAsync();
            }
        }

        private void ForwardClicked(object sender, EventArgs e)
        {
            if (Browser.CanGoForward)
            {
                Browser.GoForward();
            }
        }

        public Image Image
        {
            get { return this.image; }
            set { this.image.Source = value.Source; }
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

        private void BtnHome_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        #endregion
    }
}