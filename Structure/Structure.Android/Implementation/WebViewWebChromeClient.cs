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

namespace Structure.Droid.Implementation
{
  public  class WebViewWebChromeClient: WebChromeClient,ActivityCompat.IOnRequestPermissionsResultCallback
        
    {
        public static IValueCallback valueCallback;
        public override void OnPermissionRequest(PermissionRequest request)
        {
            base.OnPermissionRequest(request);
            request.Grant(request.GetResources());
        }

        public void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            throw new NotImplementedException();
        }
        
        public override bool OnShowFileChooser(WebView webView, IValueCallback filePathCallback, FileChooserParams fileChooserParams)
        {
            //return base.OnShowFileChooser(webView, filePathCallback, fileChooserParams);
            var TakePictureIntent = new Intent(MediaStore.ActionImageCapture);

          if(  TakePictureIntent.ResolveActivity(CrossCurrentActivity.Current.Activity.PackageManager) !=null)
            {

            }
            valueCallback = filePathCallback;
            var SelectionContentIntent = new Intent(Intent.ActionGetContent);
            SelectionContentIntent.AddCategory(Intent.CategoryOpenable);
            SelectionContentIntent.PutExtra(Intent.ExtraAllowMultiple, true);
            SelectionContentIntent.SetType("image/*");

           Intent[]  intentArray = new Intent[] { TakePictureIntent };

            Intent chooserIntent = new Intent(Intent.ActionChooser);
            chooserIntent.PutExtra(Intent.ExtraIntent, SelectionContentIntent);
            chooserIntent.PutExtra(Intent.ExtraTitle,"Image Chooser");
            chooserIntent.PutExtra(Intent.ExtraInitialIntents, intentArray);
            ActivityCompat.StartActivityForResult(
            CrossCurrentActivity.Current.Activity, Intent.CreateChooser(chooserIntent,"Select Image"),1,null);
            return true;
        }
    }
}