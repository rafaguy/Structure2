using Structure.Model;
using Structure.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Utils
{
 public static   class GlobalCommunicationDataSource
    {
        public static ObservableCollection<Communication> Messages
        {
            get;
            set;           
        }
        public static ObservableCollection<NotificationCommunicationViewModel> Notification { get; set; }
        public static int CommunicationNumberViewed { get; set; }
        public static int CommunicationNumberNotViewed { get; set; }
        public static int CurrentNewComNumber
        {
            get
            {
                return CommunicationNumberNotViewed - CommunicationNumberViewed;
            }
        }
    }
}
