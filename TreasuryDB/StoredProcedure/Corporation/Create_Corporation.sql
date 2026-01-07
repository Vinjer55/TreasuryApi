CREATE PROCEDURE [dbo].[Create_Corporation]
	@Name NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].Corporation(Name)
    VALUES (@Name);

    SELECT SCOPE_IDENTITY();
END
