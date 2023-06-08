CREATE PROCEDURE [dbo].[sp_GetSaleReport]	
	as
	begin 
	select s.CashierId, s.SaleDate, s.SubTotal, s.Tax, s.Total , u.FirstName, u.LastName, u.EmailAddress
	from [Sale] s inner join [User] u on CashierId = u.AuthUserId
	end
