CREATE PROCEDURE [dbo].[Update_Corp]
    @CorporationId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE AppUser
    SET CorporationId = NULL
    WHERE CorporationId = @CorporationId;
END