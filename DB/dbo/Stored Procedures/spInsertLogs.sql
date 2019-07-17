CREATE PROCEDURE [dbo].[spInsertLogs]
	@records BasicUDT readonly
AS
begin
	Insert into dbo.ServerLog([ServerName],[Date],[Service],[LineNumber],[Line])
	Select [ServerName],[Date],[Service],[LineNumber],[Line]
	from @records	
end
