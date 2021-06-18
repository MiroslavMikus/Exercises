# Beep beep
[console]::beep(1200, 500)
Start-Sleep -Milliseconds 100
[console]::beep(1000, 500)
Start-Sleep -Milliseconds 100
[console]::beep(1500, 500)
Start-Sleep -Milliseconds 100

# Balloon
Add-Type -AssemblyName System.Windows.Forms 
$global:balloon = New-Object System.Windows.Forms.NotifyIcon
$path = (Get-Process -id $pid).Path
$balloon.Icon = [System.Drawing.Icon]::ExtractAssociatedIcon($path) 
$balloon.BalloonTipIcon = [System.Windows.Forms.ToolTipIcon]::Warning 
$balloon.BalloonTipText = 'What do you think of this balloon tip?'
$balloon.BalloonTipTitle = "Attention $Env:USERNAME" 
$balloon.Visible = $true 
$balloon.ShowBalloonTip(2000)
start-sleep
