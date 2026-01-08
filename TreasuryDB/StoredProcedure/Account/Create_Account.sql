CREATE PROCEDURE [dbo].[Create_Account]
    @AppUserId INT,
    @AccountType NVARCHAR(20),
    @CurrencyCode NVARCHAR(10),
    @Balance DECIMAL(38, 18),
    @Provider NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Account] (AppUserId, AccountType, CurrencyCode, Balance, Provider)
    VALUES (@AppUserId, @AccountType, @CurrencyCode, @Balance, @Provider);

    SELECT SCOPE_IDENTITY();
END