using System;
using System.Globalization;
using Structure.Data;
using Structure.Interface;
using Structure.Droid.Implementation;
using Structure.Model;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Dependency(typeof(FileMgr))]
namespace Structure.Droid.Implementation
{
   public  class FileMgr : IFileMgr
    {

        public SaveFile FileSaver { get; set; }

        public FileMgr()
        {
            FileSaver = new SaveFile();
        }



        /// <summary>
        /// Gets the base64 image string.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public string GetBase64ImageString(string filename)
        {
            string res = string.Empty;
            try
            {
                byte[] b = System.IO.File.ReadAllBytes(filename);
                res = Convert.ToBase64String(b);

            }
            catch (Exception )
            {
                return null;                
            }
            return res;
        }

        public async Task<string> Base64ToFile(string base64String , string fileName,string extension)
        {
           
         
            try
            {
                byte[] b = Convert.FromBase64String(base64String);
                return await FileSaver.SaveText(fileName+extension, b);
            }
            catch (Exception e)
            {
                DatabaseAccess data = new DatabaseAccess();
                data.CreateException(new StructureExceptionModel
                {
                    Message = e.Message,
                    MethodName = e.Source,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture)

                });
                return null;
                
            }
      

        }
    }
}