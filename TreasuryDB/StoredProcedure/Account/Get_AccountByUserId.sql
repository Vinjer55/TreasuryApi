CREATE PROCEDURE [dbo].[Get_AccountByUserId]
    @AppUserId INT
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
    WHERE [AppUserId] = @AppUserId;
END;

