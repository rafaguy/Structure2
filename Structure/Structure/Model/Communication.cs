using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Model
{
   public class Communication
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Auteur { get; set; }
        public string Text { get; set; }
        public bool Received { get; set; }
        public bool Send { get; set; }
        public string UserId { get; set; }
        public string Position { get; set; }
    }
}
