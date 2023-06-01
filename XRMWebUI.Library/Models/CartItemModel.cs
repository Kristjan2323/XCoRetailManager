using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRMWebUI.Library.Models
{
    public class CartItemModel
    {
        public ProductModel Product { get; set; } = new ProductModel(); 
        public int QuantityInCard { get; set; }

        public string DisplayText
        {
            get
            {
                return $" {Product.ProductName} ({QuantityInCard})";
            }
        }
    }
}
