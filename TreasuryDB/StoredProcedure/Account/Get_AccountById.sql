CREATE PROCEDURE [dbo].[Get_AccountById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id],
           [AppUserId],
           [AccountType],
           [CurrencyCode],
           [Balance],
           [Provider],
           [DateCreated],
           [IsActive]
    FROM [dbo].[Account]
    WHERE [Id] = @Id;
END;

