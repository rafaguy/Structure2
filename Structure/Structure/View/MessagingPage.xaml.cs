using Structure.ViewModel;
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
    public partial class MessagingPage : ContentPage
    {
        MessagingPageViewModel vm;
        public MessagingPage(string username)
        {
            InitializeComponent();
            BindingContext = vm = new MessagingPageViewModel(activityIndicatotr);
        }
        protected override async void OnAppearing()
        {
            // base.OnAppearing();
          await  (BindingContext as MessagingPageViewModel).loadCommunication();
        }
       
    }
}