CREATE PROCEDURE [dbo].[Get_UserByPhone]
	 @Phone NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id],
           [CorporationId],
           [AccountId]
           [Name],
           [Email],
           [Phone],
           [Password],
           [Verified],
           [IsActive]
    FROM [dbo].[AppUser]
    WHERE [Phone] = @Phone;
END;