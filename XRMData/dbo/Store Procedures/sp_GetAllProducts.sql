CREATE PROCEDURE [dbo].[sp_GetAllProducts]

AS
begin
	select Id, ProductName, [Description], [RetailPrice]
	from [Product]
end
