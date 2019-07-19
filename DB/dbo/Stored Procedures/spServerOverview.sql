CREATE PROCEDURE [dbo].[spServerOverview]
AS
	DECLARE @Txt1 VARCHAR(MAX)= '', @ServerName NVARCHAR(MAX), @Date DATE;
SELECT @Txt1 = @Txt1 + [Type] + ' ' + CAST(COUNT([Type]) AS NVARCHAR(MAX)) + ',', 
       @ServerName = ServerName, 
       @Date = Date
FROM
(
    SELECT DISTINCT 
           'ERROR' AS [Type], 
           ServerName, 
           date, 
           Service, 
           COUNT(1) Number
    FROM ServerLog
    WHERE line LIKE '%error%'
    GROUP BY ServerName, 
             date, 
             Service
    UNION
    SELECT DISTINCT 
           'EXCEPTION' AS [Type], 
           ServerName, 
           date, 
           Service, 
           COUNT(1) Number
    FROM ServerLog
    WHERE line LIKE '%exception%'
    GROUP BY ServerName, 
             date, 
             Service
    UNION
    SELECT DISTINCT 
           'WARNING' AS [Type], 
           ServerName, 
           date, 
           Service, 
           COUNT(1) Number
    FROM ServerLog
    WHERE line LIKE '%WARN%'
    GROUP BY ServerName, 
             date, 
             Service
) result
GROUP BY ServerName, 
         Date, 
         Type;
SELECT @ServerName as ServerName, 
       @Date as Date, 
       LEFT(@Txt1, LEN(@Txt1) - 1) AS Result;
RETURN 0
