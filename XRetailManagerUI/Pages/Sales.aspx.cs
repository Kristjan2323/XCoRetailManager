using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XRMWebUI.Library.Api;
using XRMWebUI.Library.Models;

namespace XRetailManagerUI.Pages
{
    public partial class Sales : System.Web.UI.Page
    {
        ApiHelper api = ApiHelper.Instance;
       static List<CartItemModel> cartProducts = new List<CartItemModel>();
        protected async void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
            
                await LoadProducts();
            }
        }
       
        private async Task LoadProducts()
        {
            try
            {
               List<ProductModel> productList = await api.GetAllProducts();

                lstProducts.DataSource = productList;
                lstProducts.DataTextField = "ProductName";
                lstProducts.DataValueField = "Id";
                lstProducts.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnRemoveFromCart_Click(object sender, EventArgs e)
        {

        }
        private decimal CalculateTotal()
        {
            decimal total = CalculateSubTotal() + CalculateTask();
            return total;
        }
        private void Display_SubTotal_Tax_Total()
        {
            lblSubTotal.Text = $"SubTotal: {CalculateSubTotal().ToString("C")}";
            lblTax.Text = $"Tax: {CalculateTask().ToString("C")}";
            lblTotal.Text = $"Total {CalculateTotal().ToString("C")}"; 
        }
        private decimal  CalculateTask()
        {
            string taxAmount = ConfigurationManager.AppSettings["taxAmount"];
            var taxConvert = Convert.ToDecimal(taxAmount);
            decimal tax = 0;
            var itemsInCart = cartProducts;
            foreach (var item in itemsInCart)
            {
                if (item.Product.IsTaxable)
                {
                    tax += (item.Product.RetailPrice * item.QuantityInCard * taxConvert);
                }
            }

            return tax;
             
        }
     
        private decimal CalculateSubTotal()
        {
            decimal subtotal = 0;   
            var itemsInCart = cartProducts;
            foreach (var item in itemsInCart)
            {
                subtotal += item.Product.RetailPrice * item.QuantityInCard;
            }

         //   lblSubTotal.Text = subtotal.ToString("C");
         return subtotal;
        }
        
        private async Task<bool> IsItemInCart()
        {
            bool isItemInCart = false;
            var selectedItem = await GetSelectedProduct();
            var cartItems = cartProducts;
            foreach(var items in cartItems)
            {
                if(items.Product.Id == selectedItem.Id)
                {
                    isItemInCart = true;
                }
            }
            return  isItemInCart;    
        }

        private async Task UpdateExistingItemInCart(CartItemModel cartItem)
        {
            try
            {
                var getSameItemFromCart = cartProducts.Where(c => c.Product.Id == cartItem.Product.Id).First();

                if (getSameItemFromCart != null)
                {
                    var selectedItem = await GetSelectedProduct();
                    CartItemModel updateCartItem = new CartItemModel();
                    int updatedQuantity = getSameItemFromCart.QuantityInCard + Convert.ToInt32(txtQuantity.Text);
                    if (selectedItem.QuantityInStock >= updatedQuantity)
                    {
                        updateCartItem = cartItem;
                        updateCartItem.QuantityInCard = updatedQuantity;
                        cartProducts.Remove(getSameItemFromCart);
                        cartProducts.Add(updateCartItem);
                    }
                    else
                    {
                        throw new Exception("There is not as available items in stock as it is required.");
                    }
                }
            }
            catch (Exception ex)
            {
                lblQuantityValidation.Text = ex.Message;
            }          
        }

        private void LoadCart()
        {

            if (cartProducts.Count > 0)
            {
                lstCart.DataSource = cartProducts;
                lstCart.DataTextField = "DisplayText";
                lstCart.DataBind();
            }
        }
        public async  Task<string> ValidateQuantityEnter()
        {
            string output = "";
            lblQuantityValidation.Text = "";
            var selectedItem = await GetSelectedProduct();
            if(selectedItem == null)
            {
                output = "You should select a product from Items for Sale.";
                return output;
            }
            int quantityInStock = selectedItem.QuantityInStock;
            bool isQuantityValid = int.TryParse(txtQuantity.Text, out int quantity);

            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                output = "You should enter a value for quantity.";
                return output;

            }

            if (!isQuantityValid )
            {
                output = "You should enter a valid value.";
                return output;
            }

            if (!(quantity > 0))
            {
                output = "You quantity should be bigger than 0.";
                return output;
            }

            if (!(quantityInStock >= quantity))
            {
                output = "There is not as many quantity items in stock.";
                return output;
            }

        
            return output;

        }
        protected async void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (await ValidateQuantityEnter() == "")
            {
                CartItemModel cartItem = new CartItemModel();
                var selectedItem = await GetSelectedProduct();
                cartItem.Product = selectedItem;
                cartItem.QuantityInCard = Convert.ToInt32(txtQuantity.Text);
               
                if(await IsItemInCart())
                {   
                    
                 await UpdateExistingItemInCart(cartItem);  
                    
                }
                else
                {
                    cartProducts.Add(cartItem);
                }
                Display_SubTotal_Tax_Total();
                LoadCart(); 
            }
            else
            {
                lblQuantityValidation.Text = await ValidateQuantityEnter();
                lblQuantityValidation.ForeColor = Color.Red;
            }
        }

        private async Task<ProductModel> GetSelectedProduct( )
        {
            if (lstProducts.SelectedItem != null)
            {
                int selectedProductId = Convert.ToInt32(lstProducts.SelectedItem.Value);
                List<ProductModel> productlist = await api.GetAllProducts();
                ProductModel selectedProduct = productlist.Where(p => p.Id == selectedProductId).FirstOrDefault();
                return selectedProduct; 
            }
            else
            {
                return null;
            }
        }
        protected async  void lstProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  int selectedProductId = Convert.ToInt32(lstProducts.SelectedItem.Value);
             await  GetSelectedProduct();
            // Find the corresponding ProductModel object based on the selected value

        }
    }
}