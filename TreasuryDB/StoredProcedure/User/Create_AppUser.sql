CREATE PROCEDURE [dbo].[Create_AppUser]
	@Name NVARCHAR(50) = NULL,
    @Email NVARCHAR(50),
    @Phone NVARCHAR(50),
    @Password VARCHAR(MAX),
    @Verified BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[AppUser] ([Name], [Email], [Phone], [Password], [Verified])
    VALUES (@Name, @Email, @Phone, @Password, @Verified);
    
    SELECT SCOPE_IDENTITY() AS NewUserId;
END;
