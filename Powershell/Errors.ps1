

function throw-error {
    param (
        [System.Management.Automation.ActionPreference]
        $callerErrorActionPreference = $ErrorActionPreference
    )
    try {
        14/0
    }
    catch {
        Write-Error $_ -ErrorAction $callerErrorActionPreference
    }
}

throw-error -callerErrorActionPreference Stop