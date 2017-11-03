using SQLite;
using Structure.Interface;
using Structure.Model;
using Structure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Structure.Data
{
    public class DatabaseAccess
    {
        private readonly SQLiteConnection _con;

        public DatabaseAccess()
        {
            _con = Xamarin.Forms.DependencyService.Get<ISqLiteDb>().GetConnection();
            _con.CreateTable<LanguageModel>();
            _con.CreateTable<ConfigurationModel>();
            _con.CreateTable<StructureExceptionModel>();
            _con.CreateTable<Document>();
        }

        #region Language

        public void SaveDefaultLanguage(LanguageModel language)
        {
            _con.Execute("DELETE FROM LanguageModel");
            language.IsDefault = true;
            _con.Insert(language);
        }

        public string GetCultureInfo()
        {
            var defaultLanguage = _con.Table<LanguageModel>().SingleOrDefault(x => x.IsDefault);
            if (defaultLanguage != null)
            {
                return defaultLanguage.Culture;
            }
            return "en-US";
        }
        #endregion

        #region Document

        public int CreateDocument(Document document)
        {
            return _con.Insert(document);
        }

        public Document ReadDocument(int id)
        {
            return _con.Table<Document>().SingleOrDefault(x => x.Id == id);
        }

        public int CreateManyDocuments(IEnumerable<Document> documents)
        {

            //Delete All previous Data not temporary
            DeleteApiDocuments();

            //Set Category of temp to category where photo validated key: name

            //Get all temp image from DB
            var tempDoc = ReadTempDocument().ToList();

            //compare name and validity
            foreach (var item in tempDoc)
            {
                var validated = documents.SingleOrDefault(x => x.Name == item.Name && x.Validated );
                //Assign Values
                if (validated != null)
                {
                    DeleteDocument(item);

                }
            }
            

            //Repopulate database
            return _con.InsertAll(documents);
        }

        public int UpdateManyDocuments(IEnumerable<Document> documents)
        {
            return _con.InsertOrReplace(documents);
        }

        public IEnumerable<Document> ReadAllDocuments()
        {
            return _con.Table<Document>();
        }

        public IEnumerable<Document> ReadTempDocument()
        {
            var cultureInfo = GetCultureInfo();
            var temp = Localize.GetString(Constants.Temporary, cultureInfo);
            return _con.Table<Document>().Where(x => x.Type == temp );
        }

        public int DeleteApiDocuments()
        {
            var cultureInfo = GetCultureInfo();
            var temp = Localize.GetString(Constants.Temporary, cultureInfo);
            return _con.Execute("DELETE FROM Document WHERE Type != '"+ temp +"'");
        }

        public int DeleteDocument(Document document)
        {
            return _con.Delete(document);
        }
        #endregion

        #region Configuration

        public int CreateConfiguration(ConfigurationModel configuration)
        {
            _con.Execute("Delete From ConfigurationModel");
         
            
                return _con.Insert(configuration);

            
        }

        public ConfigurationModel ReadOneConfiguration()
        {
            return _con.Table<ConfigurationModel>().FirstOrDefault();
        }

        public int UpdateOneConfigurations(ConfigurationModel configuration)
        {
            return _con.InsertOrReplace(configuration);
        }

        public int CreateManyConfigurations(IEnumerable<ConfigurationModel> configurations)
        {
            return _con.InsertAll(configurations);
        }

        public int UpdateManyConfigurations(IEnumerable<ConfigurationModel> configurations)
        {
            return _con.InsertOrReplace(configurations);
        }

        public IEnumerable<ConfigurationModel> ReadAllConfigurations()
        {
            return _con.Table<ConfigurationModel>().ToList();
        }

        /// <summary>
        /// Gets the client key.
        /// </summary>
        /// <returns></returns>
        public string GetClientKey()
        {
            try
            {
                _con.CreateTable<ConfigurationModel>();
                return _con.Table<ConfigurationModel>().SingleOrDefault(x => x != null).ClientKey;

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

        #endregion

        #region Exception
        public void CreateException(StructureExceptionModel exception)
        {
            _con.Insert(exception);
        }
        #endregion
    }
}
