using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using XRMWebUI.Library.Api.Models;
using XRMWebUI.Library.Models;

namespace XRMWebUI.Library.Api
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }

        Task<AuthenticateUser> AuthenticateUser(string username, string password);
        Task<List<ProductModel>> GetAllProducts();
        Task GetUserDetail(string token);
        Task InsertSale(SaleModel sale);
    }
}