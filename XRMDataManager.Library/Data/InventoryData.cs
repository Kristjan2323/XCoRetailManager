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
        private readonly ISqlDataAccess _sqlData;

        public InventoryData(ISqlDataAccess sqlData)
        {
            _sqlData = sqlData;
        }

        public List<InventoryModel> GetInventory()
        {
            var result =  _sqlData.LoadData<InventoryModel, dynamic>("sp_GetInventory", new { });
            return result;
        }

        public void InsertInventory(InventoryModel inventory)
        {
            _sqlData.SaveData("sp_InsertInventory", new { inventory.ProductId,inventory.Quantity, inventory.PurchaseDate, inventory.PurchasePrice });
        }
    }
}
