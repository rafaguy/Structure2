using Structure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.ViewModel
{
    public class HomeViewModel : BaseViewModel, IPageNotification
    {
        private int _count;

        public int NewComCount { get {
                return _count;
            } set
            {
                if(value!=_count)
                {
                    _count = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
