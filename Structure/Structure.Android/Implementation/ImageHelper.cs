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
using Plugin.Permissions;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Android.Support.V4.Content;
using Java.Lang;
using static Android.Webkit.WebChromeClient;
using Android.Net;
using Android.Provider;

namespace Structure.Droid.Implementation
{
   public static class ImageHelper
    {
        private static string FILE_PROVIDER_AUTHORITY = "Structure.Android.fileprovider";
        private  static string IMAGE_MIME_TYPE = "image/*";
        public static Android.Net. Uri CreateTempFileContentUri(string suffix)
        {
            try
            {
                var mediaPath = Forms.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures);
             
                Java.IO.File mediaFile = Java.IO.File.CreateTempFile(DateTime.Now.Ticks.ToString(), suffix, mediaPath);
                return FileProvider.GetUriForFile(Forms.Context, FILE_PROVIDER_AUTHORITY, mediaFile);
            }catch(RuntimeException e)
            {
                throw new RuntimeException();
            }
        }
        private static Android.Net.Uri mCapturedMedia;

        public static Intent CreateCaptureIntent(FileChooserParams mparam)
        {
            string mimeType = "*/*";
            string[] acceptTypes = mparam.GetAcceptTypes();
            if (acceptTypes != null && acceptTypes.Count() > 0)
            {
                mimeType = acceptTypes[0];
            }
            Intent intent=null;
            if(mimeType.Equals(IMAGE_MIME_TYPE))
            {
               // intents = new Intent[0];
                var uri = CreateTempFileContentUri(".png");
                 intent= CreateCameraIntent(uri);
            }

            return intent;
        }

        private static Intent CreateCameraIntent(Android.Net.Uri uri)
        {
            mCapturedMedia = uri;
            var TakePictureIntent = new Intent(MediaStore.ActionImageCapture);
            TakePictureIntent.SetFlags( ActivityFlags.GrantReadUriPermission |
                   ActivityFlags.GrantWriteUriPermission);

            TakePictureIntent.PutExtra(MediaStore.ExtraOutput, mCapturedMedia);
            TakePictureIntent.ClipData=ClipData.NewUri(Forms.Context.ContentResolver,
                    FILE_PROVIDER_AUTHORITY, mCapturedMedia);

            var SelectionContentIntent = new Intent(Intent.ActionGetContent);
            SelectionContentIntent.AddCategory(Intent.CategoryOpenable);
            SelectionContentIntent.PutExtra(Intent.ExtraAllowMultiple, true);
            SelectionContentIntent.SetType("image/*");

            Intent[] intentArray = new Intent[] { TakePictureIntent };

            Intent chooserIntent = new Intent(Intent.ActionChooser);
            chooserIntent.PutExtra(Intent.ExtraIntent, SelectionContentIntent);
            chooserIntent.PutExtra(Intent.ExtraTitle, "Image Chooser");
            chooserIntent.PutExtra(Intent.ExtraInitialIntents, intentArray);
            return chooserIntent;
        }
        public static Android.Net. Uri[] ParseResult(Result resultCode, Intent intent)
        {
            if (resultCode == Result.Canceled)
            {
                return null;
            }
            Android.Net.Uri result = intent == null || resultCode != Result.Ok ? null
                    : intent.Data;
            
            if (result == null && intent == null && resultCode == Result.Ok
                    && mCapturedMedia != null)
            {
                result = mCapturedMedia;
            }
           Android.Net. Uri[] uris = null;
            if (result != null)
            {
                uris = new Android.Net.Uri[1];
                uris[0] = result;
            }
            return uris;
        }
        public  static async Task<Android.Net.Uri> SaveImage(string filename,byte[] data)
        {
            await CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { Plugin.Permissions.Abstractions.Permission.Storage, Plugin.Permissions.Abstractions.Permission.Location });
          
          //  var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
            //var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var documentsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).AbsolutePath;

            var filePath = Path.Combine(documentsPath, filename+".png");

            File.WriteAllBytes( filePath , data);
            return Android.Net.Uri.Parse(filePath);
        }
    }
}