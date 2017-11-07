using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.Design;
using Android.Support.V4.Content;
using Android.Webkit;
using Plugin.Permissions;
using Structure.Droid.Implementation;
using System;
using System.IO;
using Xamarin.Forms;

namespace Structure.Droid
{
    [Activity(Label = "Packing", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
   
        }
        public  override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
          
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
           
        }
        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
           
            if(requestCode== WebViewWebChromeClient.PHOTO_REQUESTE_CODE)
            {
                      
                    WebViewWebChromeClient.valueCallback.OnReceiveValue(ImageHelper.ParseResult(resultCode, data));                
               
            }
        }


    }
}

