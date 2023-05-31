using System.Threading.Tasks;
using XRMWebUI.Library.Api.Models;

namespace XRMWebUI.Library.Api
{
    public interface IApiHelper
    {
        Task<AuthenticateUser> AuthenticateUser(string username, string password);
    }
}