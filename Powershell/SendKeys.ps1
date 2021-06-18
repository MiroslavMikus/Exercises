# https://superuser.com/questions/1249976/sendkeys-method-in-powershell
# https://www.jesusninoc.com/11/05/simulate-key-press-by-user-with-sendkeys-and-powershell/
# COM Object
$test = new-object -ComObject wscript.shell
$test.SendKeys("hi")

# Interop
# send
[void] [System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms")
[System.Windows.Forms.SendKeys]::SendWait("x")

# activate window
[void] [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.VisualBasic")
[Microsoft.VisualBasic.Interaction]::AppActivate("Container list")