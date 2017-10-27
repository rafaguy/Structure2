using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.Permissions;
using Structure.Droid.Implementation;
using System;

namespace Structure.Droid
{
    [Activity(Label = "Packing", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
   
        }
        public async override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
           // await CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { Plugin.Permissions.Abstractions.Permission.Storage });
            //await CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { Plugin.Permissions.Abstractions.Permission.Location });
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
           
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
           // base.OnActivityResult(requestCode, resultCode, data);
           if(resultCode== Result.Ok)
            {
                Android.Net.Uri[] results = null;
                var dataString = data.Data;
                var dataExtra = data.Extras;
                var bitmap = dataExtra.Get("data");
                // (Structure.App)(Xamarin.Forms.Application.Current).CurrentPage
                var app = Xamarin.Forms.Application.Current as Structure.App;
                var webpage = app.CurrentPage as Structure.View.WebPage;
                webpage.Image = new Xamarin.Forms.Image();
                //webpage.Image = bitmap.();
             //   results = new Android.Net.Uri[] {dataString };
                WebViewWebChromeClient.valueCallback.OnReceiveValue(results);
                WebViewWebChromeClient.valueCallback = null;
            }
        }


    }
}

