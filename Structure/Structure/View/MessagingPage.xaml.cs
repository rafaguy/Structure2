using Structure.Interface;
using Structure.Data;
using Structure.Model;
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
        ViewModel.Notification notification;
       private Model.Communication oldItemSelected=null;

        public DatabaseAccess DatabaseAccess{ get; set; }

        public MessagingPage(ViewModel.Notification notif=null)
        {
            InitializeComponent();
            BindingContext = vm = new MessagingPageViewModel();
            this.notification = notif;
            if (oldItemSelected != null)
            {
                oldItemSelected.Color = Color.Default;
            }
            
        }
        protected override async void OnAppearing()
        {
            
         if(GlobalCommunicationDataSource.Messages==null)
            {
                activityIndicatotr.IsVisible = true;
                activityIndicatotr.IsRunning = true;
                GlobalCommunicationDataSource.Messages= await (BindingContext as MessagingPageViewModel).loadCommunication();

                activityIndicatotr.IsVisible = false;
                activityIndicatotr.IsRunning = false;
            }
            GlobalCommunicationDataSource.CommunicationNumberViewed = GlobalCommunicationDataSource.Messages.Count(x => x.Position.Equals("L"));
            Application.Current.Properties["NumberViewed"] = GlobalCommunicationDataSource.CommunicationNumberViewed;
         
            if (notification !=null)
            {

                try 
                {
                   
                    var listviewItemSource = (BindingContext as MessagingPageViewModel).Messages;
                    var item = listviewItemSource.Single(m => m.Date.Ticks.Equals(notification.Date.Ticks) && m.Auteur.Equals(notification.NotificationText));
                    item.Color = Color.Blue;
                    oldItemSelected = item;
                    MessagesListView.ScrollTo(item, ScrollToPosition.Start, true);


                }
                catch(Exception e)
                {
                    DatabaseAccess data = new DatabaseAccess();
                    data.CreateException(new StructureExceptionModel
                    {
                        Message = e.Message,
                        MethodName = e.Source,
                        StackTrace = e.StackTrace,
                        TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture)

                    });
                }

                base.OnAppearing();
            }
        }
       
    }
}