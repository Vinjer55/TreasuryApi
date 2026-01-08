CREATE PROCEDURE [dbo].[Delete_Account]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[Account]
    WHERE [Id] = @Id;
END;
