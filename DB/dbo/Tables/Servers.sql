CREATE TABLE [dbo].[Servers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ServerName] NVARCHAR(MAX) NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    [Duration] INT NOT NULL
)
