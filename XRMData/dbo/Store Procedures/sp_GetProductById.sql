CREATE PROCEDURE [dbo].[sp_GetProductById]
	@ProductId int
	As
	begin
	select * from [Product]
	where Id = @ProductId
end
