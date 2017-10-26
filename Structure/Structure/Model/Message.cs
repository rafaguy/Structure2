using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Model
{
   public class Message
    {
        public string Text { get; set; }
        public DateTime MessageDateTime { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public bool IsIncoming => UserId != "8926104211700735729F";
    }
}
