function Set-Data {
    [CmdletBinding(SupportsShouldProcess,ConfirmImpact='High')]
    param (
        [Parameter(ValueFromPipeline)]
        [string]$Name
    )
    
    begin {
        Write-Host "begin block"
    }
    
    process {
        if ($PSCmdlet.ShouldProcess($Name, "Write")){
            Write-Host "Process $Name"
        }
    }
    
    end {
        Write-Host "End block"
    }
}

"aha","here" | set-data
"aha","here" | set-data -WhatIf

$ConfirmPreference = 'High'
$ConfirmPreference = 'High'
