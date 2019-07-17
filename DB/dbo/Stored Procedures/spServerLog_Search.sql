CREATE PROCEDURE [dbo].[spServerLog_Search]
	@server nvarchar(100),
	@date datetime
AS
begin
	set nocount on;

	select	s.ServerName, 
			s.Date, 
			s.Service,
			s.LineNumber,
			s.Line
	from dbo.ServerLog s
	where ServerName = @server
	and Date = @date
end
