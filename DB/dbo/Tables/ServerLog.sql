CREATE TABLE [dbo].[ServerLog]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ServerName] NVARCHAR(MAX) NOT NULL, 
    [Date] DATE NOT NULL, 
    [Service] NVARCHAR(100) NOT NULL, 
    [Path] NVARCHAR(200) NOT NULL, 
    [LineNumber] INT NOT NULL, 
    [Line] NVARCHAR(MAX) NOT NULL
)
