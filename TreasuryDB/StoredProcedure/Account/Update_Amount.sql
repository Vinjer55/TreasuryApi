CREATE PROCEDURE [dbo].[Update_Amount]
    @Id INT,
    @AppUserId INT,
    @Balance DECIMAL(38, 18)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Account]
    SET
        [Balance] = COALESCE(@Balance, [Balance])
    WHERE [Id] = @Id AND [AppUserId] = @AppUserId;
END;
