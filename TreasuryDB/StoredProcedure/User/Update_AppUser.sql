CREATE PROCEDURE [dbo].[Update_AppUser]
    @Id INT,
    @Name NVARCHAR(50) ,
    @Email NVARCHAR(50) ,
    @Phone NVARCHAR(50) 
AS
BEGIN
    SET NOCOUNT ON;

    -- Ensure the user exists before updating
    IF NOT EXISTS (SELECT 1 FROM [dbo].[AppUser] WHERE [Id] = @Id)
    BEGIN
        RAISERROR('User with the specified Id does not exist.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[AppUser]
    SET 
        [Name] = COALESCE(@Name, [Name]),
        [Email] = COALESCE(@Email, [Email]),
        [Phone] = COALESCE(@Phone, [Phone])
    WHERE [Id] = @Id;
END;
