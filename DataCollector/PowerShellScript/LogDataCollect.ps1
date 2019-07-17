$searchWords = 'warn','error','exception'         # Words to search for
$limit = (Get-Date).AddDays(-1)                   # Files older than x days to be removed prior to search
$Path = "C:\Program Files\Snow Software\Logs"     # Folder to search in
$ComputerName = $env:computername
$Date = Get-Date -Format "yyyy-MM-dd".ToString()

# Clean out old logs and stats file
Get-ChildItem -Path $Path -Recurse -Include "*.log" | Where-Object { !$_.PSIsContainer -and $_.CreationTime -lt $limit } | Remove-Item -Force

# Search and output results
Foreach ($sw in $searchWords)
{
    Get-Childitem -Path $Path -Recurse -include "*.log" | ForEach-Object {
        $CurrentFile = $_.Name
        $CurrentService = ($_.Directory -split '[\\]')[-1]
        Select-String $_ -Pattern "$sw" | Select-Object @{n='ServerName';e={$ComputerName}},@{n='Date';e={$Date}},@{n='Service';e={$CurrentService}},@{n='Logfile';e={$CurrentFile}}, LineNumber, Line
    }
} 