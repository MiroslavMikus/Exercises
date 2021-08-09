$GetChildItemParams = @{
    Path = "C:\temp"
    Recurse = $true
}

Get-ChildItem @GetChildItemParams -Depth 1

Get-ChildItem -path HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall | Get-ItemProperty | Select-Object DisplayName,UninstallString
