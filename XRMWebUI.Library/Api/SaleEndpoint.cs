using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XRMWebUI.Library.Models;

namespace XRMWebUI.Library.Api
{
    public class SaleEndpoint
    {
        IApiHelper apiHelper = ApiHelper.Instance;

        public async Task InsertSale(SaleModel sale)
        {
            using (HttpResponseMessage response = await apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
            {
                if(response.IsSuccessStatusCode)
                {
                    //
                }
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
