using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRMWebUI.Library.Models
{
    public class SaleModel
    {
        public int Id { get; set; }
       public List<SaleDetailsModel> SaleDetails { get; set; } = new List<SaleDetailsModel>();  
        public string CashierId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
