using System.Threading.Tasks;
using XRetailManagerUI.Models;

namespace XRetailManagerUI.Helpers
{
    public interface IApiHelper
    {
        Task<AuthenticateUser> AuthenticateUser(string username, string password);
    }
}