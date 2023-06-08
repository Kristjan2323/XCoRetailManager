CREATE PROCEDURE [dbo].[sp_InsertInventory]

--[Id], [ProductId], [Quantity], [PurchasePrice], [PurchaseDate]
@ProductId int,
@Quantity int,
@PurchasePrice money,
@PurchaseDate datetime2

as
begin
insert into [Inventory] ( [ProductId], [Quantity], [PurchasePrice], [PurchaseDate] )
values ( @ProductId, @Quantity, @PurchasePrice, @PurchaseDate)
end
