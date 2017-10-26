using System;
using System.Collections.Generic;

using Structure.Interface;
using Structure.Model;
using RestSharp.Portable.Authenticators;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable;
using Structure.Utils;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Structure.Service
{
    public class DocumentService : IDocumentService
    {
        /// <summary>
        /// Gets the documents.
        /// </summary>
        /// <param name="ClientKey">The client key.</param>
        /// <returns></returns>
        public async Task<ObservableCollection<Document>> GetDocuments(string ClientKey)
        {
            ObservableCollection<Document> DataDocuments;
            try
            {

                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator(Constants.ApiUserName, Constants.ApiPassword);
                    var request = new RestRequest("MobileGetListDocs", Method.GET);
                    request.AddQueryParameter("clientkey", ClientKey);

                    var result = await client.Execute<List<Document>>(request).ConfigureAwait(false);

                    DataDocuments = new ObservableCollection<Document>(result.Data);

                    return DataDocuments;
                }
            }

            catch (Exception e)
            {
                throw e;
            }


         
        }

        public async Task<bool> PostDocument(Document document, string clientKey)
        {

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator(Constants.ApiUserName, Constants.ApiPassword);
                    var request = new RestRequest("MobilePostDocument", Method.POST);
                    request.AddJsonBody(new
                    {
                        ClientKey = clientKey,
                        data = document.Data,
                        comments = document.Comments,

                    });
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var result = await client.Execute<Document>(request);

                    if (result.StatusDescription == "OK")
                    {
                            return true;
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return false;
        }
    }
}