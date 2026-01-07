CREATE PROCEDURE [dbo].[Get_UserById]
    @Id INT
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
           [Verified],
           [IsActive]
    FROM [dbo].[AppUser]
    WHERE [Id] = @Id;
END;
