CREATE PROCEDURE [dbo].[sp_GetUserById]
	@UserId nvarchar(128)
	
AS
begin
	SELECT * from [User]
	where AuthUserId = @UserId
end
