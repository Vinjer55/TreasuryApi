CREATE TABLE [dbo].[Account]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [AppUserId] INT NOT NULL,
    [AccountKind] NVARCHAR(20) NOT NULL, -- Wallet | Bank | Exchange
    [AssetType]   NVARCHAR(10) NOT NULL, -- Crypto | Fiat
    [CurrencyCode] NVARCHAR(10) NOT NULL, -- USD | EUR | BTC | ETH 
    [Balance] DECIMAL(38, 18) NULL, 
    [Provider] NVARCHAR(50) NOT NULL,
    [DateCreated] DATETIME2 (7) DEFAULT (sysutcdatetime()) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT ((1)),

    CONSTRAINT [FK_Account_AppUser] FOREIGN KEY ([AppUserId]) REFERENCES [dbo].[AppUser] ([Id]),
    CONSTRAINT UQ_User_Account UNIQUE (AppUserId, CurrencyCode, Provider)
)
