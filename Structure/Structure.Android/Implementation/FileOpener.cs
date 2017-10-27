﻿using Structure.Interface;
using Structure.Droid.Implementation;
using Android.Content;
using System;
using Android.Widget;
using Xamarin.Forms;
using Android.Support.V4.App;
using Plugin.CurrentActivity;
using Plugin.Permissions;

[assembly: Dependency(typeof(FileOpener))]
namespace Structure.Droid.Implementation
{
    public class FileOpener : IFileOpener
    {
        public async void OpenFile(string filePath)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("file:///" + filePath);
            Intent fileIntent = new Intent(Intent.ActionView);
            fileIntent.SetDataAndType(uri, "application/*");
            fileIntent.SetFlags(ActivityFlags.NoHistory);
            Intent intent = Intent.CreateChooser(fileIntent, "Open File");
           await  CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { Plugin.Permissions.Abstractions.Permission.Storage });
            try
            {
               Forms.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Xamarin.Forms.Forms.Context, "No Application Available to View PDF", ToastLength.Short).Show();
            }
        }
    }
}