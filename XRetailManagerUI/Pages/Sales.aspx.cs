using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using XRMWebUI.Library.Api;
using XRMWebUI.Library.Models;

namespace XRetailManagerUI.Pages
{
    public partial class Sales : System.Web.UI.Page
    {
        ApiHelper api = ApiHelper.Instance;
        ProductEndpoint apiProduct = new ProductEndpoint();
       static List<CartItemModel> cartProducts = new List<CartItemModel>();
        static ProductModel selectedProductStatic = new ProductModel();
        static List<ProductModel> allAvailableProducts = new List<ProductModel>();


        protected async void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
            
                await LoadProducts();
            }
        }

        private void LoadCart()
        {
            if (cartProducts.Count > 0)
            {
                lstCart.DataSource = cartProducts.OrderBy(c => c.Product.ProductName);
                lstCart.DataTextField = "DisplayText";
                lstCart.DataValueField = "Id";
                lstCart.DataBind();

                btnRemoveAllItemsFromCart.Visible = true;
            }
            else
            {
                lstCart.DataSource = cartProducts;
                lstCart.DataBind();
                btnRemoveAllItemsFromCart.Visible = false;
            }
        
        }
        private async Task LoadProducts()
        {
            try
            {
               List<ProductModel> productList = await apiProduct.GetAllProducts();
                allAvailableProducts = productList;

                lstProducts.DataSource = allAvailableProducts.OrderBy(p => p.ProductName);
                //lstProducts.DataTextField = "ProductName";
                //lstProducts.DataValueField = "Id";
                lstProducts.DataBind();
            }
            catch (Exception ex)
            {

               if(ex.Message == "Unauthorized")
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        protected  void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (lstCart.SelectedItem == null)
            {
                lblQuantityValidation.Text = "Select an item from Cart listbox.";
                return;
            }
           
            else
            {
                int cartItemId = Convert.ToInt32(lstCart.SelectedItem.Value);
                CartItemModel selectedCartItem = cartProducts.Where(c => c.Id == cartItemId).FirstOrDefault();
               
                if (selectedCartItem != null)
                {
                    selectedCartItem.QuantityInCard -= 1;

                    var selectedAvailableProduct = allAvailableProducts.Where(p => p.Id ==  cartItemId).FirstOrDefault();    
                    if(selectedAvailableProduct != null)
                    {
                        selectedAvailableProduct.QuantityInStock += 1;
                        lstProducts.DataSource = allAvailableProducts;
                        lstProducts.DataBind();
                    }
                  
                }
                if (selectedCartItem.QuantityInCard == 0)
                {
                    cartProducts.Remove(selectedCartItem);
                }
                LoadCart();
                Display_SubTotal_Tax_Total();
            }
        }

        private void SyncronizeQuantityValue()
        {
            if (selectedProductStatic.QuantityInStock > 0)
            {
                int itemQuantity = Convert.ToInt32(txtQuantity.Text);
                selectedProductStatic.QuantityInStock -= itemQuantity;
                var selectedProductInList = allAvailableProducts.Where(x => x.Id == selectedProductStatic.Id).FirstOrDefault();
                selectedProductInList.QuantityInStock -= itemQuantity;
                lstProducts.DataSource = allAvailableProducts.OrderBy(p => p.ProductName);
                lstCart.DataValueField = "Id";
                lstProducts.DataBind();
            }

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
        
        private bool IsItemInCart()
        {
            bool isItemInCart = false;
         //   var selectedItem = await GetSelectedProduct();
            var cartItems = cartProducts;
            foreach(var items in cartItems)
            {
                if(items.Product.Id == selectedProductStatic.Id)
                {
                    isItemInCart = true;
                }
            }
            return  isItemInCart;    
        }

        private void UpdateExistingItemInCart(CartItemModel cartItem)
        {
            try
            {
                var getSameItemFromCart = cartProducts.Where(c => c.Product.Id == cartItem.Product.Id).First();

                if (getSameItemFromCart != null)
                {
                 //   var selectedItem = await GetSelectedProduct();
                    CartItemModel updateCartItem = new CartItemModel();
                    int selectedQuantity = Convert.ToInt32(txtQuantity.Text);
                    int updatedQuantity = getSameItemFromCart.QuantityInCard + selectedQuantity;
                    if (getSameItemFromCart.QuantityInCard > 0 && selectedQuantity <= cartItem.Product.QuantityInStock)
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

   
        public string ValidateQuantityEnter()
        {
            string output = "";
            lblQuantityValidation.Text = "";
          //  var selectedItem = await GetSelectedProduct();
            if(selectedProductStatic.Id == 0)
            {
                output = "You should select a product from Items for Sale.";
                return output;
            }
            int quantityInStock = selectedProductStatic.QuantityInStock;
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
        protected  void btnAddToCart_Click(object sender, EventArgs e)
        {
            string script = @"var modalValidator = new bootstrap.Modal(document.getElementById('modalValidator'));
                      modalValidator.show();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalValidator", script, true);
            if ( ValidateQuantityEnter() == "")
            {
                CartItemModel cartItem = new CartItemModel();
               // var selectedItem = await GetSelectedProduct();
                cartItem.Product = selectedProductStatic;
                cartItem.QuantityInCard = Convert.ToInt32(txtQuantity.Text);
               
                if( IsItemInCart())
                {                      
                  UpdateExistingItemInCart(cartItem);                     
                }
                else
                {
                    cartProducts.Add(cartItem);
                }
                SyncronizeQuantityValue();
                Display_SubTotal_Tax_Total();
                LoadCart(); 
            }
            else
            {
                lblQuantityValidation.Text =  ValidateQuantityEnter();
                lblQuantityValidation.ForeColor = Color.Red;
            }
        }

        private ProductModel CheckIfItemsIsSelectedBefore(int productId)
        {
          // bool selectedBefore = false;
            ProductModel product = new ProductModel();
            var checkInCart = cartProducts.Where(c => c.Product.Id == productId).ToList();
            if(checkInCart.Count > 0)
            {
                foreach(var p in checkInCart)
                {
                    product = p.Product;
                }
            }
            return product;
        }
        private async Task GetSelectedProduct( int productId)
        {
            try
            {
                if (productId != 0)
                {
                    if (CheckIfItemsIsSelectedBefore(productId).Id == 0)
                    {
                        List<ProductModel> productlist = await api.GetAllProducts();
                        ProductModel selectedProduct = productlist.Where(p => p.Id == productId).FirstOrDefault();
                        lblSelectedItem.Text = "";
                        selectedProductStatic = selectedProduct;
                        lblSelectedItem.Text = $"Selected item: {selectedProductStatic.ProductName}";
                        btnRemoveSelectedItem.Visible = true;
                    }
                    else
                    {
                        selectedProductStatic = CheckIfItemsIsSelectedBefore(productId);
                    }                
                }
                else
                {
                    throw new Exception("No item found");
                }
            }
            catch (Exception ex)
            {
               lblQuantityValidation.Text = ex.Message;
            }
        }
        protected async void lstProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "AddToCart")
            {
                lblQuantityValidation.Text = "";
                string selectedProductId = e.CommandArgument.ToString();
                int productId = Convert.ToInt32(selectedProductId);
                await GetSelectedProduct(productId);
            }
        }

        protected void btnRemoveSelectedItem_Click(object sender, EventArgs e)
        {
            selectedProductStatic = new ProductModel();
            lblSelectedItem.Text = "";
            lblQuantityValidation.Text = "";
            btnRemoveSelectedItem.Visible = false;
        }

        protected async void btnCheckOut_Click(object sender, EventArgs e)
        {
            // sale detail model complited
            SaleModel sale = new SaleModel();
            List<CartItemModel> itemsInCart = cartProducts;

           foreach(CartItemModel cartItem in itemsInCart)
            {
                sale.SaleDetails.Add( new SaleDetailsModel
                {
                    ProductId = cartItem.Product.Id,
                    Quantity = cartItem.QuantityInCard
                                  
                });
              
            }
          
           await api.InsertSale(sale);
        }

        protected async void btnRemoveAllItemsFromCart_Click(object sender, EventArgs e)
        {
            cartProducts = new List<CartItemModel>();

            lblSubTotal.Text = "SubTotal: 0£";
            lblTax.Text = "Tax: 0£";
            lblTotal.Text = "Total: 0£";
            allAvailableProducts = await apiProduct.GetAllProducts();
            LoadCart();
            await LoadProducts();


        }
    }
}