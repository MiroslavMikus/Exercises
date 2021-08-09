function Print {
    write-host "Printing here"    
}

$object = [PSCustomObject]@{
    User = "Miro"
    Email = "my@email.com"
    SomePrint = { Print }
}
$object | Add-Member -NotePropertyMembers @{StringUse="Display"}  

$object | gm

$object.SomePrint.Invoke()
