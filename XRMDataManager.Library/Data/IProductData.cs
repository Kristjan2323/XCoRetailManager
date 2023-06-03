using System.Collections.Generic;
using XRMDataManager.Library.Models;

namespace XRMDataManager.Library.Data
{
    public interface IProductData
    {
        List<ProductModel> GetAllProducts();
        ProductModel GetProductById(int productId);
    }
}