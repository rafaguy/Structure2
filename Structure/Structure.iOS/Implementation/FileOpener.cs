using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Structure.Interface;
using Xamarin.Forms;
using Structure.iOS.Implementation;

[assembly: Dependency(typeof(FileOpener))]

namespace Structure.iOS.Implementation
{
    public class FileOpener : IFileOpener
    {

        public void OpenFile(string fullPath)
        {
            try
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {

                    //
                    // This is a trick. Here we are trying to get the navigation renderer to get the navigationcontroller from it.
                    //

                    var firstController = UIApplication.SharedApplication.KeyWindow.RootViewController.ChildViewControllers.First().ChildViewControllers.Last().ChildViewControllers.First();

                    var navcontroller = firstController as UINavigationController;

                    var uidic = UIDocumentInteractionController.FromUrl(new NSUrl(fullPath, true));

                    uidic.Delegate = new DocInteractionC(navcontroller);

                    uidic.PresentPreview(true);

                });

            }
            catch (Exception e)
            {

                throw e;

            }
            finally
            {

                //
                // Close the original stream.
                //

                // fileStream.Close();

            }

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