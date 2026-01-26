CREATE PROCEDURE [dbo].[Check_AccountExist]
    @AppUserId INT,
    @AccountKind  NVARCHAR(20),
    @AssetType   NVARCHAR(10),
    @CurrencyCode NVARCHAR(10),
    @Provider NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id],
           [AppUserId],
           [AccountKind],
           [AssetType],
           [CurrencyCode],
           [Balance],
           [Provider],
           [DateCreated],
           [IsActive]
    FROM [dbo].[Account]
    WHERE [AppUserId] = @AppUserId AND [AccountKind] = @AccountKind AND [AssetType] = @AssetType AND [CurrencyCode] = @CurrencyCode AND
            [Provider ]= @Provider;
END