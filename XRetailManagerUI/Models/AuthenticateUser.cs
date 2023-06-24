using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XRMWebUI.Library.Api;

namespace XRetailManagerUI.Models
{
    public class AuthenticateUser 
    {
       
        public string Access_Token { get; set; }
        public string Username { get; set; }
        public string Error { get; internal set; }

     
    }
}