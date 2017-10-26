using Structure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Structure.Model
{
   public class Communication:BaseViewModel
    {
        private Color _color;
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Auteur { get; set; }
        public string Text { get; set; }
        public bool Received { get; set; }
        public bool Send { get; set; }
        public string UserId { get; set; }
        public string Position { get; set; }
        public Color Color {
            get { return _color; }
            set {
                if(_color!= value)
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
