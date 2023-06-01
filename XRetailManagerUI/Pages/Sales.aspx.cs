using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XRMWebUI.Library.Api;

namespace XRetailManagerUI.Pages
{
    public partial class Sales : System.Web.UI.Page
    {
        ApiHelper api = ApiHelper.Instance;
        protected async void Page_Load(object sender, EventArgs e)
        {
           await LoadProducts();
        }
       
        private async Task LoadProducts()
        {
            try
            {
                var productList = await api.GetAllProducts();
                lstItems.DataSource = productList;
                lstItems.DataTextField = "ProductName";
                lstItems.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnRemoveFromCart_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {

        }
    }
}