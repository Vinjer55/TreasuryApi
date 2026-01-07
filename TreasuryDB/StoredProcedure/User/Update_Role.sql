CREATE PROCEDURE [dbo].[Update_Role]
	@AppUserId INT,
	@Role NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[AppUser]
    SET Role = @Role
    WHERE Id = @AppUserId;

END;
