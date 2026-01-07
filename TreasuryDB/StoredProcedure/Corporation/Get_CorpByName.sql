CREATE PROCEDURE [dbo].[Get_CorpByName]
	@Name NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id],
           [Name],
           [DateCreated],
           [IsActive]
    FROM [dbo].[Corporation]
    WHERE [Name] = @Name;
END;
