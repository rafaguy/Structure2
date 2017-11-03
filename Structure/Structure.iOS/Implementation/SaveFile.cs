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
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<string> SaveText(string filename, byte[] text)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var documents =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var file = Path.Combine(documents, filename);

            File.WriteAllBytes(file, text);

            var fullpath = documents + file;

            return fullpath;
        }
    }
}