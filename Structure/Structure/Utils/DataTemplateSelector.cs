using Structure.CustomCells;
using Structure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Structure.View
{
    public class DataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
        public DataTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = (Communication)item;
            if(message == null)
            {
                return null;
            }
            return message.Position.Equals("L") ? this.incomingDataTemplate : this.outgoingDataTemplate;
        }
    }
}
