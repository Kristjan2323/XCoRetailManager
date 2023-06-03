using APIRetailManager.Models;
using Microsoft.AspNet.Identity;
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
    [RoutePrefix("api/Sale")]
    public class SaleController : ApiController
    {
        [HttpPost]
        public void PostSale(SaleModel sale)
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            ISqlDataAccess sqlDataAccess = new SqlDataAccess();
            IProductData productData = new ProductData(sqlDataAccess);
            SaleData saleData = new SaleData(sqlDataAccess, productData);

            saleData.InsertSale(sale, userId);
        }
    }
}
