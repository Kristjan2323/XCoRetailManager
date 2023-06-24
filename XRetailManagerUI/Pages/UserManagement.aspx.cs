using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XRMWebUI.Library.Api;
using XRMWebUI.Library.Models;

namespace XRetailManagerUI.Pages
{
    public partial class UserManagement : System.Web.UI.Page
    {
        UserEndpoint userEndpoint = new UserEndpoint();
      static  string selectedUserId;
        protected async void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                 await PopulateUserGrid();
            }           
        }

        private async Task PopulateUserGrid()
        {
            try
            {
                var users = await userEndpoint.GetAllRolesForUsers();
                userManagementGrid.DataSource = users;
                userManagementGrid.DataBind();
            }
            catch (Exception ex)
            {

               if(ex.Message == "Unauthorized")
                {
                    string script = @"var modalValidator = new bootstrap.Modal(document.getElementById('modalValidator'));
                      modalValidator.show();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modalValidator", script, true);
                   
                }
            }
        }

        private async Task GetAllRoles(List<string> roleName)
        {
            var roles = await userEndpoint.GetAllRoles();
            List<string> newRole = new List<string>();
            foreach (string role in roles)
            {
                if (!roleName.Contains(role))
                {
                    newRole.Add(role); 
                }
            }
          
            ddlNewRole.DataSource = newRole;
            ddlNewRole.DataBind();
        }
        private  async Task<List<string>> PopulateListBoxWithUserRoles(string userId)
        {
            var users = await userEndpoint.GetAllRolesForUsers();
            var selectedUser = users.Where(x => x.Id == userId).FirstOrDefault();
            List<string> userRoles = new List<string>();
            if (selectedUser != null)
            {
                foreach (var userRole in selectedUser.Roles)
                {
                   userRoles.Add(userRole.Value);
                }
           
                    lstUserRoles.DataSource = userRoles;
                  //  lstUserRoles.DataTextField = "DisplayRoles";
                    lstUserRoles.DataBind();
            }

            return userRoles;
        }

        private void ClearComponents()
        {
            ddlNewRole.Items.Clear(); 
            ddlNewRole.DataBind();
            lstUserRoles.Items.Clear();
            lstUserRoles.DataBind();
        }
        protected async void addRoleButton_Click(object sender, EventArgs e)
        {
            try
            {
                UserRolePairModel userRoleModel = new UserRolePairModel();
                var selectedRole = ddlNewRole.SelectedValue;
                if (selectedRole != null)
                {
                    userRoleModel.UserId = selectedUserId;
                    userRoleModel.RoleId = selectedRole;
                    await userEndpoint.AddRole(userRoleModel);
                    await PopulateUserGrid();
                    ClearComponents();
                    lblUserManagementValidation.Text = $"New Role [{selectedRole}], added successfuly.";
                    lblUserManagementValidation.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblUserManagementValidation.Text = $"An error occur: {ex.Message}";
            }
        }

        protected async void removeRoleButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRole = lstUserRoles.SelectedValue;
                if (selectedRole != null)
                {
                    UserRolePairModel userRoleModel = new UserRolePairModel();
                    userRoleModel.UserId = selectedUserId;
                    userRoleModel.RoleId = selectedRole;
                    await userEndpoint.RemoveRole(userRoleModel);
                    await PopulateUserGrid();
                    ClearComponents();
                    lblUserManagementValidation.Text = $" Role [{selectedRole}], removed successfuly.";
                    lblUserManagementValidation.ForeColor = Color.Green;
                }
            }

            catch(Exception ex)
            {
                lblUserManagementValidation.Text = $"An error occur: {ex.Message}";
            }
        }

        protected async void btnManageRoles_Click(object sender, EventArgs e)
        {
            Button btnManageRoles = (Button)sender;
            lblUserManagementValidation.Text = string.Empty;
            selectedUserId = null;
            selectedUserId = btnManageRoles.CommandArgument;
            if(selectedUserId != null)
            {
             var getUserRoles =  await PopulateListBoxWithUserRoles(selectedUserId);
               await GetAllRoles(getUserRoles);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Sales");
        }
    }
}