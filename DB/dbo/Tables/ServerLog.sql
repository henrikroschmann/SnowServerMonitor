CREATE TABLE [dbo].[ServerLog]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ServerName] NVARCHAR(MAX) NOT NULL, 
    [Date] DATE NOT NULL, 
    [Service] NVARCHAR(100) NOT NULL, 
    [LineNumber] INT NOT NULL, 
    [Line] NVARCHAR(MAX) NOT NULL
)
