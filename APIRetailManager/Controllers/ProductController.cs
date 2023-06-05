using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XRMDataManager.Library.Data;
using XRMDataManager.Library.Internal.DatatAccess;
using XRMDataManager.Library.Models;

namespace APIRetailManager.Controllers
{
   //[Authorize]
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        [HttpGet]
        public  List<ProductModel> GetAllProducts()
        {
            ISqlDataAccess sData = new SqlDataAccess();
            ProductData pData = new ProductData(sData);
            return  pData.GetAllProducts();
        }

    }
}
