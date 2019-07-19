CREATE PROCEDURE [dbo].[spDujInsertLogs]
	@records DujUDT readonly
AS
begin
	Insert into dbo.Servers([ServerName],[StartTime],[Duration])
	Select [ServerName],[StartTime],[Duration]
	from @records	
end