CREATE PROCEDURE [dbo].[sp_InsertSale]
    @Id int = 0 output,
	@CashierId nvarchar(128),
	@SaleDate dateTime2,
	@SubTotal money,
	@Tax money,
	@Total money
AS
begin
insert into [Sale] (CashierId, SaleDate, SubTotal,Tax, Total)
values (@CashierId, @SaleDate, @SubTotal,@Tax, @Total)
select @Id = SCOPE_IDENTITY()
end