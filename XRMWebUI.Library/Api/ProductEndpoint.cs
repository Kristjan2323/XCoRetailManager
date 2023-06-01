using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XRMWebUI.Library.Models;

namespace XRMWebUI.Library.Api
{
    public class ProductEndpoint
    {

        private IApiHelper _apiHelper;
        public ProductEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        public async Task<List<ProductModel>> GetAllProducts()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Product"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ProductModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
