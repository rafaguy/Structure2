using SQLite;
using Structure.Interface;
using Structure.Model;
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
            return _con.Table<Document>().SingleOrDefault(x => x.IdMobile == id);
        }

        public int CreateManyDocuments(IEnumerable<Document> documents)
        {
            //Delete All previous Data not temporary
            DeleteApiDocuments();

            //Set Category of temp to category where photo validated key: name

            //Get all temp image from DB
            var tempDoc = ReadTempDocument().ToList();

            //compare name and validity
            var validatedPhoto = documents.Where(x => x.Validated && tempDoc.Contains(x)).ToList();

            var tempValidated = tempDoc.Where(x => documents.Any(y => y.Name == x.Name && y.Validated)).ToList();

            foreach (var item in tempDoc)
            {
                var validated = documents.SingleOrDefault(x => x.Name == item.Name && x.Validated );
                //Assign Values
                if (validated != null)
                {
                    DeleteTempDocument(item);

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
            return _con.Table<Document>().Where(x => x.Type == "Temporary");
        }

        public int DeleteApiDocuments()
        {
            return _con.Execute("DELETE FROM Document WHERE Type !='Temporary'");
        }

        public int DeleteTempDocument(Document document)
        {
            return _con.Delete(document);
        }
        #endregion

        #region Configuration

        public int CreateConfiguration(ConfigurationModel configuration)
        {
            var dbConfig = _con.Table<ConfigurationModel>().ToList().SingleOrDefault(x => x.ClientKey == configuration.ClientKey);
            if (dbConfig != null)
            {
                dbConfig.Language = configuration.Language;
                dbConfig.Login = configuration.Login;
                dbConfig.Pwd = configuration.Pwd;
                dbConfig.Logo = configuration.Logo;
                dbConfig.Name = configuration.Name;
                dbConfig.Surname = configuration.Surname;

                return UpdateOneConfigurations(dbConfig);

            }
            else
            {
                return _con.Insert(configuration);

            }
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

        public string GetClientKey()
        {
            return _con.Table<ConfigurationModel>().SingleOrDefault(x => x != null).ClientKey;
        }

        #endregion
    }
}
