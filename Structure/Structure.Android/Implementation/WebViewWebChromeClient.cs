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
using Android.Webkit;
using Android.Provider;
using Android.Support.V4.App;
using Plugin.CurrentActivity;
using Android.Content.PM;
using Plugin.Permissions;
using System.Threading.Tasks;

namespace Structure.Droid.Implementation
{
  public  class WebViewWebChromeClient: WebChromeClient,ActivityCompat.IOnRequestPermissionsResultCallback
        
    {
        public static IValueCallback valueCallback;
        public static int PHOTO_REQUESTE_CODE =566; 
        
        public override void OnPermissionRequest(PermissionRequest request)
        {
          
            var res = request.GetResources();
            var orig = request.Origin;

           CrossCurrentActivity.Current.Activity.RunOnUiThread(() => request.Grant(res));
          
          
        }

        public void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            throw new NotImplementedException();
        }
        
        public override bool OnShowFileChooser(WebView webView, IValueCallback filePathCallback, FileChooserParams fileChooserParams)
        {
            valueCallback = filePathCallback;

           var intents= ImageHelper.CreateCaptureIntent(fileChooserParams);
            
            ActivityCompat.StartActivityForResult(
            CrossCurrentActivity.Current.Activity, Intent.CreateChooser(intents,"Select Image"), PHOTO_REQUESTE_CODE, null);
            return true;
        }
    }
}