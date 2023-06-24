using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XRMWebUI.Library.Api;
using XRMWebUI.Library.Models;

namespace XRetailManagerUI
{
    public partial class SiteMaster : MasterPage
    {
        UserModel loggedInUser = UserModel.Instance;
        UserEndpoint userEndpoint = new UserEndpoint();
        protected  void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageButtonVisibility();
                Task.Run(async () => await InsertUserPolicy());
            }
        }

        private void ManageButtonVisibility()
        {
            var result = UserModel.AuthUserId;
            if (result != null)
            {
                btnLogin.Visible = false;
                btnRegister.Visible = false;
                btnLogout.Visible = true; 
            }
            else
            {
                btnLogin.Visible = true;
                btnRegister.Visible = true;
                btnLogout.Visible = false;
            }
        }
        private  async Task InsertUserPolicy()
        {
            
            var user = await userEndpoint.GetUserInfo();
            List<string> roleName = new List<string>();
            if (user != null)
            {
                ManageButtonVisibility();
                foreach (var r in user.Roles)
                {
                    roleName.Add(r.Value);
                }
            }
         
            if(roleName.Count > 0)
            {
                if (!roleName.Contains("Admin"))
                {
                    tagUserManagement.Visible = false;
                }
                else
                {
                    tagUserManagement.Visible = true;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}