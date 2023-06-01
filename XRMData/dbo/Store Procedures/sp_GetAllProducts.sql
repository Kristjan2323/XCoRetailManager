CREATE PROCEDURE [dbo].[sp_GetAllProducts]

AS
begin
	select Id, ProductName, [Description], [RetailPrice], QuantityInStock, IsTaxable
	from [Product]
end
