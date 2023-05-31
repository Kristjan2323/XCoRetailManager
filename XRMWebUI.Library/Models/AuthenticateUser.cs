using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XRMWebUI.Library.Api.Models
{
    public class AuthenticateUser
    {
        public string Access_Token { get; set; }
        public string Username { get; set; }
        public string Error { get; internal set; }
    }
}