using System.Net.Http;
using System.Threading.Tasks;
using XRMWebUI.Library.Api.Models;

namespace XRMWebUI.Library.Api
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }   
        Task<AuthenticateUser> AuthenticateUser(string username, string password);
    }
}