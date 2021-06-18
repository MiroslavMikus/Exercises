function Test-Function {
    [CmdletBinding()]
    param (
        # Parameter help description
        [Parameter(ValueFromPipelineByPropertyName)]
        [int]
        $Id    
    )
    process {
        if ($_ -is [System.Diagnostics.Process]) {
            Write-Output "Pipeline has process"
            return $_
        }
        else {
            Write-Output "Gettig process"
            return get-process -Id $Id
        }
    }
}

get-process | Select-Object -first 1  | Test-Function

[pscustomobject]@{Id = 16820 } | Test-Function 