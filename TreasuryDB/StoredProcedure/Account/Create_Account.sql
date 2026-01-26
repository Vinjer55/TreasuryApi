CREATE PROCEDURE [dbo].[Create_Account]
    @AppUserId INT,
    @AccountKind  NVARCHAR(20),
    @AssetType   NVARCHAR(10),
    @CurrencyCode NVARCHAR(10),
    @Balance DECIMAL(38, 18),
    @Provider NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Account] (AppUserId, AccountKind, AssetType, CurrencyCode, Balance, Provider)
    VALUES (@AppUserId, @AccountKind, @AssetType, @CurrencyCode, @Balance, @Provider);

    SELECT SCOPE_IDENTITY();
END