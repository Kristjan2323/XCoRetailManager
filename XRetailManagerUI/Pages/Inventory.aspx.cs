using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XRMWebUI.Library.Api;
using XRMWebUI.Library.Models;

namespace XRetailManagerUI.Pages
{
    public partial class Inventory : System.Web.UI.Page
    {
        InventoryEndpoint inventoryEndpoint = new InventoryEndpoint();
        protected async void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               await PopulateInventoryGrid();
            }
        }

        private async Task<List<InventoyModel>> GetInventory()
        {
            try
            {
                var result = await inventoryEndpoint.GetInventory();
                return result;
            }
            catch (Exception ex)
            {

                lblInventory.Text = ex.Message;
                return null;
            }
        }

        private async Task PopulateInventoryGrid()
        {
            inventoryGrid.DataSource = await GetInventory();
            inventoryGrid.DataBind();
        }
    }
}