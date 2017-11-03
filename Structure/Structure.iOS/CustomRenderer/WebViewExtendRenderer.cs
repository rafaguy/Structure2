using Foundation;
using Structure.Extensions;
using Structure.iOS.CustomRenderer;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendRenderer))]
namespace Structure.iOS.CustomRenderer
{
   public class WebViewExtendRenderer: Xamarin.Forms.Platform.iOS.WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            ScalesPageToFit = true;
            LoadRequest(new Foundation.NSUrlRequest(new NSUrl("https://appear.in/ags-paris01")));
          
        }
        /// <summary>
        /// Settings the web view.
        /// </summary>
      
    }
}
