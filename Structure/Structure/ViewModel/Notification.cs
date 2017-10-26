using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Structure.ViewModel
{
    public class Notification:BaseViewModel
    {
       
        private int _index;

        public int index { get { return _index; }
            set {
                _index = value;
               
            } }

        public string NotificationText { get; set; }
        
        
        
        public LayoutOptions HorizontalOpt
        {
            get;set;
        }
        
       public FormattedString FormatattedText { get; set; }

        public string ItemIcon
        {
            get
            {
                return index==0 ? "clock.png" : "messageclosedenvelope.png";
            }

        }
        public DateTime Date { get; set; }

    }
}