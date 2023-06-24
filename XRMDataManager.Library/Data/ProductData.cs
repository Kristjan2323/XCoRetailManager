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
            try
            {
                _sqlData.StartTransaction();
                var result = _sqlData.LoadDataInTransaction<ProductModel, dynamic>("sp_GetAllProducts", new { });
                _sqlData.CommitTransaction();
                return result;
              
            }
            catch (Exception ex)
            {
                _sqlData.RollbackTransaction();
                throw;
            }
        }
        public ProductModel GetProductById(int productId)
        {
            try
            {
              //  _sqlData.StartTransaction();
                var result = _sqlData.LoadDataInTransaction<ProductModel, dynamic>("sp_GetProductById", new { productId });
               // _sqlData.CommitTransaction();
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _sqlData.RollbackTransaction();
                throw;
            }
        }
    }
}
