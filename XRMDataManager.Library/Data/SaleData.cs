using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRMDataManager.Library.Internal.DatatAccess;
using XRMDataManager.Library.Models;

namespace XRMDataManager.Library.Data
{
    public class SaleData
    {
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly IProductData _productData;
        public SaleData(ISqlDataAccess sqlDataAccess, IProductData productData)
        {
            _sqlDataAccess = sqlDataAccess;
            _productData = productData; 
        }

        public void InsertSale(SaleModel sale, string userId)
        {
            //populate sale model 
            int taxAmount = 5;
            SaleDbModel saleDbModel = new SaleDbModel();
            saleDbModel.SaleDate = DateTime.UtcNow;    

            //get product by id
            foreach(SaleDetailsModel sd in sale.SaleDetails)
            {
                ProductModel productModel = _productData.GetProductById(sd.ProductId);
                saleDbModel.SubTotal += productModel.RetailPrice * sd.Quantity;

                if(productModel.IsTaxable)
                {
                    saleDbModel.Tax += (productModel.RetailPrice * sd.Quantity * taxAmount);
                }               
            }

            saleDbModel.Total = saleDbModel.SubTotal + saleDbModel.Tax;

            try
            {
              
                var p = new DynamicParameters();
                p.Add("@CashierId", userId);
                p.Add("@SaleDate", saleDbModel.SaleDate);
                p.Add("@SubTotal", saleDbModel.SubTotal);
                p.Add("@Tax", saleDbModel.Tax);
                p.Add("@Total", saleDbModel.Total);
                p.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                _sqlDataAccess.SaveDataInTransiction("sp_InsertSale", p);

                //get sale id model
                int saleId = p.Get<int>("Id");

                //populate sale detail model 
                foreach (var saleItem in sale.SaleDetails)
                {
                    var saleProduct = _productData.GetProductById(saleItem.ProductId);
                    SaleDetailDbModel saleDetailDbModel = new SaleDetailDbModel
                    {
                        SaleId = saleId,
                        ProductId = saleItem.ProductId,
                        Quantity = saleItem.Quantity,
                        PurchasePrice = saleProduct.RetailPrice,
                        Tax = saleProduct.RetailPrice * saleItem.Quantity * taxAmount
                    };
                    _sqlDataAccess.SaveDataInTransiction("sp_InsertSaleDetails", new
                    {
                        saleDetailDbModel.SaleId,
                        saleDetailDbModel.ProductId,
                        saleDetailDbModel.Quantity,
                        saleDetailDbModel.PurchasePrice,
                        saleDetailDbModel.Tax
                    });
                }

                _sqlDataAccess.CommitTransaction();
               // _sqlDataAccess.Dispose();
            }
            catch (Exception ex)
            {
                _sqlDataAccess.RollbackTransaction();
                throw;
            }

        
        }

    }
}
