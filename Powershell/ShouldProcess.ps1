function set-data {
    [CmdletBinding(SupportsShouldProcess,ConfirmImpact='High')]
    param([string]$Name)
    
    if ($PSCmdlet.ShouldProcess("Printing $Name")) {
        Write-Host $Name

        if ($PSCmdlet.ShouldContinue("do we continue?","Caption")){
            Write-Host Yes
        } else {
            Write-Host No
        }
    }
}

set-data "Miro"