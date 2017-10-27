using Structure.Utils;
using Structure.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Notification notification;
        public MessagingPage(Notification notif=null)
        {
            InitializeComponent();
            BindingContext = vm = new MessagingPageViewModel();
            this.notification = notif;
        }
        protected override async void OnAppearing()
        {
            // base.OnAppearing();
         if(GlobalCommunicationDataSource.Messages==null)
            {
                activityIndicatotr.IsVisible = true;
                activityIndicatotr.IsRunning = true;
               await (BindingContext as MessagingPageViewModel).loadCommunication();
                activityIndicatotr.IsVisible = false;
                activityIndicatotr.IsRunning = false;
            }
            if(notification !=null)
            {

                try 
                {
                    var listviewItemSource = (BindingContext as MessagingPageViewModel).Messages;
                    var item = listviewItemSource.Single(m => m.Date.Ticks.Equals(notification.Date.Ticks) && m.Auteur.Equals(notification.NotificationText));
                    item.Color = Color.Blue;
                    MessagesListView.ScrollTo(item, ScrollToPosition.Start, true);


                }
                catch(Exception e)
                {
                    
                }
              
                           
            }
        }
       
    }
}