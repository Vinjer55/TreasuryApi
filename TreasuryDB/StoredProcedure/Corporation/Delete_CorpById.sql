CREATE PROCEDURE [dbo].[Delete_CorpById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[Corporation]
    WHERE [Id] = @Id;
END;
