CREATE TABLE [dbo].[AppUser]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [CorporationId] INT NULL, 
    [AccountId] INT NULL, 
    [Name] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [Password] NVARCHAR(255) NOT NULL, 
    [Role] NVARCHAR(50) NULL,
    [Verified] BIT NOT NULL DEFAULT ((0)), 
    [IsActive] BIT NOT NULL DEFAULT ((1)),

    CONSTRAINT [FK_AppUser_Corporation] FOREIGN KEY ([CorporationId]) REFERENCES [dbo].[Corporation] ([Id])
)
