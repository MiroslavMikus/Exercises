$dir,$files = Get-ChildItem -Recurse | Measure-Object -Sum PSIsContainer,Length -ErrorAction Ignore

Set-PSBreakpoint -Variable now -Action {$Global:now = Get-Date } -Mode Read

Remove-PSBreakpoint -Id 1,2,3,4

$now