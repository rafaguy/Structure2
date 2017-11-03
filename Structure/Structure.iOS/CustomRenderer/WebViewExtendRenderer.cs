using Foundation;
using Structure.Extensions;
using Structure.iOS.CustomRenderer;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WebViewExtended), typeof(WebViewExtendRenderer))]
namespace Structure.iOS.CustomRenderer
{
   public class WebViewExtendRenderer:ViewRenderer<WebViewExtended, UIWebView>
    {

        protected override void OnElementChanged(ElementChangedEventArgs<WebViewExtended> e)
        {
           // base.OnElementChanged(e);
            SetNativeControl(new UIWebView());
            if(e.NewElement !=null)
            {
                SettingWebView();
            }
        }
        private void SettingWebView()
        {
           

            var url = "https://appear.in/ags-paris01";
            Control.ScalesPageToFit = true;
            Control.LoadRequest(new NSUrlRequest(new NSUrl(url)));

        }
        /* protected override void OnElementChanged(VisualElementChangedEventArgs e)
         {
             base.OnElementChanged(e);
             ScalesPageToFit = true;
             LoadRequest(new Foundation.NSUrlRequest(new NSUrl("https://appear.in/ags-paris01")));

         }*/
        /// <summary>
        /// Settings the web view.
        /// </summary>

    }
}
