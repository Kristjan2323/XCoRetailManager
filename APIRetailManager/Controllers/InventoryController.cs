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
    [Authorize]
    [RoutePrefix("api/Inventory") ]
    public class InventoryController : ApiController
    {
        [HttpGet]
        public List<InventoryModel> GetInventory()
        {
            ISqlDataAccess sData = new SqlDataAccess();
            InventoryData iData = new InventoryData(sData);
            var result = iData.GetInventory();
            return result;  
        }

        [HttpPost]
        public void PostInventory(InventoryModel inventory)
        {
            ISqlDataAccess sData = new SqlDataAccess();
            InventoryData iData = new InventoryData(sData);
            iData.InsertInventory(inventory);
        }

    }
}
