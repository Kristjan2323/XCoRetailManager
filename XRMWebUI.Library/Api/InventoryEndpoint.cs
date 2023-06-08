using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XRMWebUI.Library.Models;

namespace XRMWebUI.Library.Api
{
    public class InventoryEndpoint
    {
        IApiHelper apiHelper = ApiHelper.Instance;

        public async  Task<List<InventoyModel>> GetInventory()
        {
            using (HttpResponseMessage response = await apiHelper.ApiClient.GetAsync("api/Inventory"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<InventoyModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task InsertInventory(InventoyModel inventory)
        {
            using(HttpResponseMessage response = await apiHelper.ApiClient.PostAsJsonAsync("api/Inventory", inventory))
            {
                if(response.IsSuccessStatusCode )
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
