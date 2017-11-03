using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Structure.Interface;
using Xamarin.Forms;
using Structure.iOS.Implementation;
using System.Threading.Tasks;

[assembly: Dependency(typeof(FileOpener))]

namespace Structure.iOS.Implementation
{
    public class FileOpener : IFileOpener
    {

        public Task OpenFile(string fullPath)
        {
            return null;
          
        }

        public class DocInteractionC : UIDocumentInteractionControllerDelegate
        {
            readonly UINavigationController _navigationController;

            public DocInteractionC(UINavigationController controller)
            {
                _navigationController = controller;
            }

            public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
            {

                return _navigationController;

            }

            public override UIView ViewForPreview(UIDocumentInteractionController controller)
            {

                return _navigationController.View;

            }
        }
    }
}