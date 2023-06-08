CREATE PROCEDURE [dbo].[sp_GetInventory]
	as   
	begin
	select [Id], [ProductId], [Quantity], [PurchasePrice], [PurchaseDate]
	from [Inventory]
	end