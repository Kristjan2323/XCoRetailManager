using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XRMWebUI.Library.Models;

namespace XRMWebUI.Library.Api
{
    public class UserEndpoint
    {
        IApiHelper apiHelper = ApiHelper.Instance;

        public async Task<ApplicationUserModel> GetUserInfo()
        {
            using(HttpResponseMessage response = await apiHelper.ApiClient.GetAsync("api/Admin/GetUserInfo"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ApplicationUserModel>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
        }
        public async Task<List<ApplicationUserModel>> GetAllRolesForUsers()
        {
            using(HttpResponseMessage response = await apiHelper.ApiClient.GetAsync("api/Admin/GetAllRolesForUsers"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ApplicationUserModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }                   
        }

        public async Task<List<string>> GetAllRoles()
        {
            using (HttpResponseMessage response = await apiHelper.ApiClient.GetAsync("api/Admin/GetAllRoles"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<string>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }


        }
        public async Task AddRole(UserRolePairModel userRole)
        {
            using(HttpResponseMessage response = await apiHelper.ApiClient.PostAsJsonAsync("api/Admin/AddRole", userRole))
            {
                if ((response.IsSuccessStatusCode))
                {
                    //
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task RemoveRole(UserRolePairModel userRole)
        {
            using (HttpResponseMessage response = await apiHelper.ApiClient.PostAsJsonAsync("api/Admin/RemoveRole", userRole))
            {
                if (response.IsSuccessStatusCode)
                {
                    //
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
