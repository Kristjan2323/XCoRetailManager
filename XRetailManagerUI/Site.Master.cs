using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XRMWebUI.Library.Api;

namespace XRetailManagerUI
{
    public partial class SiteMaster : MasterPage
    {
        UserEndpoint userEndpoint = new UserEndpoint();
        protected  void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Task.Run(async () => await InsertUserPolicy());
            }
        }

        private  async Task InsertUserPolicy()
        {
            var user = await userEndpoint.GetUserInfo();
            List<string> roleName = new List<string>();
            if (user != null)
            {
                foreach(var r in user.Roles)
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
    }
}