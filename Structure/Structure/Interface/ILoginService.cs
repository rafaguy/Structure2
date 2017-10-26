using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Interface
{
    public interface ILoginService
    {
        Task<string> LoginAsync(string username,string password);

    }
}
