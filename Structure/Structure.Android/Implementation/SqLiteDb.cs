using System;
using SQLite;
using System.IO;
using Structure.Interface;
using Structure.Droid.Implementation;

[assembly: Xamarin.Forms.Dependency(typeof(SqLiteDb))]

namespace Structure.Droid.Implementation
{

    public class SqLiteDb : ISqLiteDb
    {
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var docpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docpath, "StructureDB.db3");
            return new SQLiteAsyncConnection(path);
        }
        public SQLiteConnection GetConnection()
        {
            var local = System.Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
            var docpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docpath, "StructureDB.db3");
            return new SQLiteConnection(path);
        }
    }
}