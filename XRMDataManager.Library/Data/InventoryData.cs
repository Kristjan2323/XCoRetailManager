using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRMDataManager.Library.Internal.DatatAccess;
using XRMDataManager.Library.Models;

namespace XRMDataManager.Library.Data
{
    public class InventoryData
    {
        private static ISqlDataAccess _sqlData;
        IProductData _productData;
        public InventoryData(ISqlDataAccess sqlData,IProductData productData)
        {
            _sqlData = sqlData;
            _productData = productData;
        }

        public List<InventoryModel> GetInventory()
        {
            var allInevories =  _sqlData.LoadData<InventoryModel, dynamic>("sp_GetInventory", new { });
            foreach(var item in allInevories)
            {
                item.Product = _productData.GetProductById(item.ProductId);
            }
            return allInevories;
        }

        public void InsertInventory(InventoryModel inventory)
        {
            _sqlData.SaveData("sp_InsertInventory", new { inventory.ProductId,inventory.Quantity, inventory.PurchaseDate, inventory.PurchasePrice });
        }
    }
}
