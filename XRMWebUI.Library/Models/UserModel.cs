using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XRMWebUI.Library.Models;

namespace XRMWebUI.Library.Models
{
    public class UserModel
    {
        private static readonly Lazy<UserModel> instance = new Lazy<UserModel>(() => new UserModel());

        public  static UserModel Instance => instance.Value;
        public static string AuthUserId { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string EmailAddress { get; set; }
        public static string Token { get; set; }
        public static DateTime CreatedDate { get; set; } = new DateTime();

        public void SetUserInfo(LoggedInUser result, string token)
        {
            CreatedDate = result.CreatedDate;
            EmailAddress = result.EmailAddress;
            AuthUserId = result.AuthUserId;
            FirstName = result.FirstName;
            LastName = result.LastName;
            Token = token;

        }
    }
}