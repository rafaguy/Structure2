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
            BindingContext = vm = new MessagingPageViewModel(activityIndicatotr);
            this.notification = notif;
        }
        protected override async void OnAppearing()
        {
            // base.OnAppearing();
          await  (BindingContext as MessagingPageViewModel).loadCommunication();
            if(notification !=null)
            {
                var listviewItemSource = (BindingContext as MessagingPageViewModel).Messages;
                var item = listviewItemSource.Single(m => m.Date.Equals(notification.Date) && m.Auteur.Equals(notification.NotificationText));
                item.Color = Color.Blue;
                MessagesListView.ScrollTo(item, ScrollToPosition.Start, true);
               
                //MessagesListView.SelectedItem = item;
            }


        }
       
    }
}