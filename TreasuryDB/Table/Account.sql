CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Currency] NVARCHAR(50) NOT NULL, 
    [Amount] DECIMAL(18, 2) NULL, 
    [Bank] NVARCHAR(50) NOT NULL,
    [DateCreated] DATETIME2 (7) DEFAULT (sysutcdatetime()) NOT NULL
)
