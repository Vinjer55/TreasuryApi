CREATE PROCEDURE [dbo].[Add_UserToCorp]
	@AppUserId INT,
	@CorporationId INT,
	@Role NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE [dbo].[AppUser]
    SET 
        [CorporationId] = @CorporationId, [Role] = @Role
    WHERE [Id] = @AppUserId;

	RETURN 0
END
