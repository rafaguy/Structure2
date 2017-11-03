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
using Structure.Data;

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
            ObservableCollection<Document> DataDocuments = new ObservableCollection<Document>();
            try
            {

                using (var client = new RestClient(new Uri(Constants.TrayBaseUri)))
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
                DatabaseAccess data = new DatabaseAccess();
                data.CreateException(new StructureExceptionModel
                {
                    Message = e.Message,
                    MethodName = e.Source,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture)

                });
                return DataDocuments;

            }


        }

        public async Task<bool> PostDocument(Document document, string clientKey, LocationModel location)
        {

            try
            {
                using (var client = new RestClient(new Uri(Constants.TrayBaseUri)))
                {
                    client.Authenticator = new HttpBasicAuthenticator(Constants.ApiUserName, Constants.ApiPassword);
                    var request = new RestRequest(Constants.PostDocument, Method.POST);
                    request.AddJsonBody(new
                    {
                        ClientKey = clientKey,
                        data = document.Data,
                        comments = document.Comments,
                        IdMobile = document.FileName
                    });
                    
                    var query =  await client.Execute<Document>(request);
              
                    await PostLocation(location);

                    if (query.IsSuccess)
                    {
                            return true;
                    }

                }
            }
            catch (Exception e)
            {
                DatabaseAccess data = new DatabaseAccess();
                data.CreateException(new StructureExceptionModel {
                    Message = e.Message,
                    MethodName = e.Source,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture)

                });

            }

            return false;
        }

        public async Task PostLocation(LocationModel location)
        {
            try
            {
                using (var client = new RestClient(new Uri(Constants.TrayBaseUri)))
                {
                    client.Authenticator = new HttpBasicAuthenticator(Constants.ApiUserName, Constants.ApiPassword);
                    var request = new RestRequest(Constants.PostLocation, Method.POST);
                    request.AddJsonBody(new
                    {
                        ClientKey = location.ClientKey,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
                    });
                  
                   await client.Execute<LocationModel>(request);

                  

                }
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
            }

          
        }
    }
}