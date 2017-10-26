
using System.IO;


using SQLite;
using Structure.Interface;
using Structure.iOS.Implementation;

[assembly: Xamarin.Forms.Dependency(typeof(SqLiteDb))]
namespace Structure.iOS.Implementation
{
    /// <summary>
    /// The class implements the conenction to Isql db 
    /// </summary>
    /// <seealso cref="Structure.Interface.ISqLiteDb" />
    public class SqLiteDb : ISqLiteDb
    {
        /// <summary>
        /// Gets the asynchronous connection.
        /// </summary>
        /// <returns>
        /// Instance of SQLiteAsycConnection
        /// </returns>
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var docpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docpath, "StructureAppDB.db3");
            return new SQLiteAsyncConnection(path);
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>
        /// Instance of SQLiteConnection
        /// </returns>
        public SQLiteConnection GetConnection()
        {
            var docpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docpath, "StructureAppDB.db3");
            return new SQLiteConnection(path);
        }
    }
}