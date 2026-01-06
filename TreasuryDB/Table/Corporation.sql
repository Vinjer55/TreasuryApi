CREATE TABLE [dbo].[Corporation]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [DateCreated] DATETIME2 (7) DEFAULT (sysutcdatetime()) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT (1)
)
