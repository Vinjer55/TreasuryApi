CREATE PROCEDURE [dbo].[Update_Account]
    @Id INT,
    @AccountType NVARCHAR(20),
    @CurrencyCode NVARCHAR(10),
    @Balance DECIMAL(38, 18),
    @Provider NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Ensure the user exists before updating
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [Id] = @Id)
    BEGIN
        RAISERROR('User with the specified Id does not exist.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[Account]
    SET 
        [AccountType] = COALESCE(@AccountType, [AccountType]),
        [CurrencyCode] = COALESCE(@CurrencyCode, [CurrencyCode]),
        [Balance] = COALESCE(@Balance, [Balance]),
        [Provider] = COALESCE(@Provider, [Provider])
    WHERE [Id] = @Id;
END;