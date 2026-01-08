CREATE PROCEDURE [dbo].[Check_AccountExist]
    @AppUserId INT,
    @AccountType NVARCHAR(20),
    @CurrencyCode NVARCHAR(10),
    @Provider NVARCHAR(50)
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
    WHERE [AppUserId] = @AppUserId AND [AccountType] = @AccountType AND [CurrencyCode] = @CurrencyCode AND
            [Provider ]= @Provider;
END