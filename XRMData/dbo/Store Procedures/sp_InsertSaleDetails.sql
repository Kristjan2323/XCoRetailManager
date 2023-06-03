CREATE PROCEDURE [dbo].[sp_InsertSaleDetails]
	@SaleId int,
	@ProductId int,
	@Quantity int,
	@PurchasePrice money,
	@Tax money
AS
	begin
	insert into [SaleDetail] (SaleId, ProductId, Quantity, PurchasePrice, Tax)
	values  (@SaleId, @ProductId, @Quantity, @PurchasePrice, @Tax)
	end
