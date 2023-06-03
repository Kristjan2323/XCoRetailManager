using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRMDataManager.Library.Models
{
    public class SaleDbModel
    {
        public int Id { get; set; } 
        public string CashierId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set;}
    }
}
