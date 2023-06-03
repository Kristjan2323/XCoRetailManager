using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRMDataManager.Library.Internal.DatatAccess;
using XRMDataManager.Library.Models;

namespace XRMDataManager.Library.Data
{
    public class ProductData : IProductData
    {
        private ISqlDataAccess _sqlData;

        public ProductData(ISqlDataAccess sqlData)
        {
            _sqlData = sqlData;
        }

        public List<ProductModel> GetAllProducts()
        {
            var result = _sqlData.LoadData<ProductModel, dynamic>("sp_GetAllProducts", new { });
            return result;
        }
        public ProductModel GetProductById(int productId)
        {
            var result = _sqlData.LoadData<ProductModel, dynamic>("sp_GetProductById", new { productId });
            return result.FirstOrDefault();
        }
    }
}
