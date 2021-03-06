﻿using System;
using Structure.Interface;
using System.IO;
using Structure.Droid.Implementation;
using Xamarin.Forms;
using Plugin.Permissions;
using System.Threading.Tasks;

[assembly: Dependency(typeof(SaveFile))]
namespace Structure.Droid.Implementation
{
    public class SaveFile : ISaveFile
    {
        public SaveFile()
        {
             

        }
        public async Task<string> SaveText(string filename, byte[] text)
        {
            await CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { Plugin.Permissions.Abstractions.Permission.Storage });
            await CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { Plugin.Permissions.Abstractions.Permission.Location });

            var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
            //var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var documentsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;

            var filePath = Path.Combine(documentsPath, filename);

            File.WriteAllBytes(filePath, text);

            return filePath;
        }
    }
}