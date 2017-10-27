using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Model
{
    public class Document
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual string Type { get; set; }
        public string FileName { get; set; }
        public string Data { get; set; }
        public string Extension { get; set; }
     
        public string IdMobile { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Recieved { get; set; }
        public bool Validated { get; set; }
        public bool Sent { get; set; }
        public string FilePath { get; set; }
        public string Comments { get; set; }

   
       
    }
}
