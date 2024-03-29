﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRMWebUI.Library.Models
{
    public class ApplicationUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();

        public string DisplayRoles
        {
           
            get
            {
                string output = string.Empty;
                foreach (var role in Roles)
                {
                    if (Roles.Count > 1)
                    {
                        output += $" {role.Value}, "; 
                    }
                    else if(Roles.Count == 1) 
                    {
                        output += $" {role.Value}";
                    }
                 
                    else if(Roles.Count ==0) 
                    {
                        output = "User have no specific Roles";
                    }
                }
                return output;
            }
         
        }


    }
}
