CREATE PROCEDURE [dbo].[Get_AccountByIdAndCurrencyCode]
    @Id INT,
    @AppUserId INT,
    @CurrencyCode NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id],
           [AppUserId],
           [AccountKind],
           [CurrencyCode],
           [Balance],
           [Provider],
           [DateCreated],
           [IsActive]
    FROM [dbo].[Account]
    WHERE [Id] = @Id 
      AND [AppUserId] = @AppUserId
      AND [CurrencyCode] = @CurrencyCode;
END;

