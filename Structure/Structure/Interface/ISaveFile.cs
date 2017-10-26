using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Interface
{
    public interface ISaveFile
    {
       Task<string> SaveText(string filename, byte[] text);
    }
}
