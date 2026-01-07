CREATE PROCEDURE [dbo].[Get_AllCorpUsers]
	@CorporationId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id],
           [CorporationId],
           [Name],
           [Email],
           [Phone],
           [Password],
           [Role],
           [Verified],
           [IsActive]
    FROM [dbo].[AppUser]
    WHERE [CorporationId] = @CorporationId;
END;
