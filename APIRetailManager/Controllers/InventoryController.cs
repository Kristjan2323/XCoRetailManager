using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XRMDataManager.Library.Data;
using XRMDataManager.Library.Internal.DatatAccess;
using XRMDataManager.Library.Models;

namespace APIRetailManager.Controllers
{
    [Authorize (Roles ="Admin,Cashier")]
    [RoutePrefix("api/Inventory") ]
    public class InventoryController : ApiController
    {
        [HttpGet]
       
        public List<InventoryModel> GetInventory()
        {
            ISqlDataAccess sData = new SqlDataAccess();
            IProductData productData = new ProductData(sData);
            InventoryData iData = new InventoryData(sData, productData);
            var result = iData.GetInventory();
            return result;  
        }

        [HttpPost]
        public void PostInventory(InventoryModel inventory)
        {
            ISqlDataAccess sData = new SqlDataAccess();
            IProductData productData = new ProductData(sData);
            InventoryData iData = new InventoryData(sData, productData);
            iData.InsertInventory(inventory);
        }

    }
}
