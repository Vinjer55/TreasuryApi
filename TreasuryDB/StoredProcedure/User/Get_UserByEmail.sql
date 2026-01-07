CREATE PROCEDURE [dbo].[Get_UserByEmail]
    @Email NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id],
           [CorporationId],
           [AccountId],
           [Name],
           [Email],
           [Phone],
           [Password],
           [Role],
           [Verified],
           [IsActive]
    FROM [dbo].[AppUser]
    WHERE [Email] = @Email;
END;
