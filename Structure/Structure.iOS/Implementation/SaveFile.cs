using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Structure.Interface;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Structure.iOS.Implementation;

[assembly: Dependency(typeof(SaveFile))]

namespace Structure.iOS.Implementation
{
    public class SaveFile  : ISaveFile
    {
        public async Task<string> SaveText(string name, byte[] text)
        {
            var documents =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, name);

            File.WriteAllBytes(filename, text);

            var fullpath = documents + name;

            return fullpath;
        }
    }
}