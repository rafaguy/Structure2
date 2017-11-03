using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Structure.Extensions;
using Structure.Droid.CustomRenderer;
using Xamarin.Forms.Platform.Android;
using Structure.Droid.Implementation;
using static Android.Webkit.WebSettings;

/// <summary>
/// 
/// </summary>
[assembly: ExportRenderer(typeof(WebViewExtended),typeof(WebViewExtendRenderer))]
namespace Structure.Droid.CustomRenderer
{
   public class WebViewExtendRenderer: WebViewRenderer
    {

        /// <summary>
        /// Raises the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ElementChangedEventArgs{WebViewExtended}"/> instance containing the event data.</param>
        /// 
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                SettingWebView();
            }
        }
     
        /// <summary>
        /// Settings the web view.
        /// </summary>
        private void SettingWebView()
        {
           
            Control.Settings.JavaScriptEnabled = true;
            Control.Settings.AllowFileAccessFromFileURLs = true;
            Control.Settings.AllowUniversalAccessFromFileURLs = true;
            Control.Settings.SetPluginState(PluginState.On);
            Control.SetWebViewClient(new Android.Webkit.WebViewClient());
            Control.SetWebChromeClient(new WebViewWebChromeClient());
            Control.Settings.JavaScriptCanOpenWindowsAutomatically = true;
            Control.Settings.DomStorageEnabled = true;
            Control.Settings.SupportZoom();

             var url = "https://appear.in/ags-paris01";
           
            Control.LoadUrl(url);
         
        }
    }
}