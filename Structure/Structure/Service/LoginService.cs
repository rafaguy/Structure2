using RestSharp.Portable;
using RestSharp.Portable.Authenticators;
using RestSharp.Portable.HttpClient;
using Structure.Interface;
using Structure.Model;
using Structure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Service
{
    public class LoginService : ILoginService
    {
        public Task<string> LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<string> PostConfig(string login, string password, string language)
        {
         
            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator(Constants.ApiUserName, Constants.ApiPassword);
                    var request = new RestRequest("MobilePostConfiguration", Method.POST);
                    request.AddJsonBody(new
                    {
                        Login = login,
                        Pwd = password,
                        Language = language,

                    });

                    var result = await client.Execute<ConfigurationModel>(request);

                    if (result != null)
                    {
                        if(result.Data.ClientKey != null)
                        {
                          return result.Data.ClientKey;

                        }

                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return "KO";
        }

        public async Task<string> PostPasswordLost(string clientKey)
        {
            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator(Constants.ApiUserName, Constants.ApiPassword);
                    var request = new RestRequest("MobilePostPwdLost", Method.POST);
                    request.AddQueryParameter("clientKey", clientKey);

                    var result = await client.Execute(request);

                    if (result.Content != null)
                    {
                        
                       return "OK";

                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return "KO";
        }
    }
}
