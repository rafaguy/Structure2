using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Structure.ViewModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Structure.Utils;
using System.Net.Http;
using Newtonsoft.Json;

namespace Structure.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationCommunicationPage : ContentPage
    {
        private ObservableCollection<NotificationCommunicationViewModel> _notification;
        private ObservableCollection<NotificationCommunicationViewModel> _expandedNotification;
        
        public  NotificationCommunicationPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;
            // base.OnAppearing();
            this._notification =await getListNotification();
            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
            UpdateList();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(sender as Button !=null)
            {
                int selectedIndex = _expandedNotification.IndexOf(
               ((NotificationCommunicationViewModel)((Button)sender).CommandParameter)
                );
                _notification[selectedIndex].Expanded = !_notification[selectedIndex].Expanded;
            }
            else
            {
                var label = sender as Label;
                var tapGestureRecognizer = label.GestureRecognizers[0] as TapGestureRecognizer;

                int selectedIndex = _expandedNotification.IndexOf(
               ((NotificationCommunicationViewModel) tapGestureRecognizer.CommandParameter)
                );
                _notification[selectedIndex].Expanded = !_notification[selectedIndex].Expanded;
            }
            
            UpdateList();
        }
        private void UpdateList()
        {
             _expandedNotification = new ObservableCollection<NotificationCommunicationViewModel>();
          

            foreach(var group in _notification)
            {
                var newNotif = new NotificationCommunicationViewModel(group.Title, group.ShortTitle);
                int index = group.Title.Equals("Notifications")?0:1;
                if (group.Expanded)
                {
                    newNotif.Expanded = !newNotif.Expanded;
                    
                   
                    foreach (var lbel in group)
                    {
                            lbel.index = index;
                            newNotif.Add(lbel);            
                    }
                   
                }
               
                _expandedNotification.Add(newNotif);
               
            }
            Notificationlist.ItemsSource = _expandedNotification;
           
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

        private void BtnNewCom_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewCommunicationPage());
        }

        private void BtnAddMessage_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnAddMessageCliked");
            Navigation.PushAsync(new MessagingPage());
            
        }

        private async Task<ObservableCollection<NotificationCommunicationViewModel>> getListNotification()
        {
             string requestUriListNotification = string.Concat(Constants.GetListNotificationrequestUri, "6FDFDD074B4BD30209085207575E5D0D");
             string requestUriListCommunication = string.Concat(Constants.GetCommunicationrequestUri, "6FDFDD074B4BD30209085207575E5D0D");
              System.Net.NetworkCredential credentials = new System.Net.NetworkCredential { UserName = Constants.ApiUserName, Password = Constants.ApiPassword };
            var notificationsGroup = new NotificationCommunicationViewModel("Notifications", "Notification");
            var communicationGroup = new NotificationCommunicationViewModel("Communications", "Communications");
            try
            {
                var handler = new HttpClientHandler { Credentials = credentials };
                var client = new HttpClient(handler);
                /////////Get lists of Notification from the API
                var json = await client.GetStringAsync(requestUriListNotification);
                var notificationLists = JsonConvert.DeserializeObject<List<Model.Notification>>(json);
                foreach(var notif in notificationLists)
                {
                    notificationsGroup.Add(new Notification { NotificationText = notif.Text });
                }
                //Get lists of Communication from the API
                json = await client.GetStringAsync(requestUriListCommunication);
                var communicationLists = JsonConvert.DeserializeObject<List<Model.Communication>>(json);
                foreach (var notif in communicationLists)
                {
                    communicationGroup.Add(new Notification { NotificationText = notif.Auteur,Date= notif.Date });
                }
                return new ObservableCollection<NotificationCommunicationViewModel> {
                 notificationsGroup,communicationGroup};
            }
            catch
            {
                var app = Application.Current as App;

                await  app.CurrentPage.DisplayAlert("Network Error", "No Connection Internet", "OK");
                return default(ObservableCollection<NotificationCommunicationViewModel>);
            }
           
        }

        private void BtnAddMessage_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MessagingPage());
            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var stacklayout = (sender as StackLayout);
            var gestureRec = stacklayout.GestureRecognizers[0] as TapGestureRecognizer;
            var notification = gestureRec.CommandParameter as Notification;
            if(notification.index ==1)
            {
                Navigation.PushAsync(new MessagingPage(notification));
            }

        }
    }
}