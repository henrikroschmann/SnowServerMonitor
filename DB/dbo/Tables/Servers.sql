CREATE TABLE [dbo].[Servers]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ServerName] NVARCHAR(MAX) NOT NULL, 
    [Warnings] INT NOT NULL, 
    [Date] DATE NOT NULL
)
