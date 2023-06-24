using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRMDataManager.Library.Models
{
    public class UserModel
    {
        private static readonly Lazy<UserModel> instance = new Lazy<UserModel>(() => new UserModel());

        public static UserModel Instance => instance.Value;
        public string AuthUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime();

        public void SetUserInfo(UserModel user)
        {

        }
    }
}
