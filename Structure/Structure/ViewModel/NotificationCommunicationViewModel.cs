using Structure.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Structure.ViewModel
{
   public class NotificationCommunicationViewModel:ObservableCollection<Notification>,IPageNotification
    {
       
        private bool _expanded;
        private int _count=0;

        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public bool Expanded
        {
            get
            {
                return _expanded;
            }
            set
            {
                if(_expanded !=value)
                {
                    _expanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Expanded)));
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(StateIcon)));
                }
                
            }
        }
        public string ItemIcon
        {
            get
            {
                return Title.Equals("Notifications") ? "clock.png" : "messageclosedenvelope.png";
            }

        }
        public string StateIcon
        {
            get
            {
                
               return Expanded ? "expanded_blue.png" : "collapsed_blue.png";
            }
            
        }


        public Command TappedLabel { get; set; }


        public NotificationCommunicationViewModel(string title, string shortTitle)
        {
            this.Title = title;
            this.ShortTitle = shortTitle;
        }
        public NotificationCommunicationViewModel()
        {

        }
        
       public int NewComCount
        {
            get { return _count; }

            set
            {
             if(_count!=value)
                {
                    _count = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(NewComCount)));
                }
            }
        }
    }
}
