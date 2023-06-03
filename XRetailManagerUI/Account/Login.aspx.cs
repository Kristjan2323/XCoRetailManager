using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XRMWebUI.Library.Api;

namespace XRetailManagerUI.Account
{
    public partial class Login : System.Web.UI.Page
    {
         ApiHelper api = ApiHelper.Instance;       
        protected void Page_Load(object sender, EventArgs e)
        {

        }    

        protected async void LogIn_Click(object sender, EventArgs e)
        {
            lblLoginValidation.Text = "";
            var result = await api.AuthenticateUser(Email.Text, Password.Text);

            if ((result.Access_Token == null && result.Username == null) && !string.IsNullOrEmpty(result.Error))
            {
                lblLoginValidation.Text = $"Ann error occured: {result.Error}.";
                return;
            }

            await api.GetUserDetail(result.Access_Token);
          
        }
    }
}