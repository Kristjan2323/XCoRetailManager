using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using XRetailManagerUI.Models;

namespace XRetailManagerUI.Helpers
{
    public class ApiHelper : IApiHelper
    {

        private static readonly Lazy<ApiHelper> instance = new Lazy<ApiHelper>(() => new ApiHelper());

        public static ApiHelper Instance => instance.Value;

        private HttpClient apiClient;

        private ApiHelper()
        {
            InitializeClient();
        }
        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];
            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticateUser> AuthenticateUser(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type", "password"),
                new KeyValuePair<string,string>("username", username),
                new KeyValuePair<string,string>("password",password)
            });

            try
            {
                using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<AuthenticateUser>();
                        return result;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {

                var errorResult = new AuthenticateUser { Error = ex.Message };
                return errorResult;

            }
        }
    }
}