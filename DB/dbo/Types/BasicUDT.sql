CREATE TYPE [dbo].[BasicUDT] AS TABLE
(
	[ServerName] NVARCHAR(MAX), 
    [Date] DATE, 
    [Service] NVARCHAR(100), 
    [LineNumber] INT, 
    [Line] NVARCHAR(MAX)
)