mkdir c:/temp/temping
function What-If {
    [CmdletBinding()]
    param (
        [switch]$WhatIf
    )
    process {
        Remove-Item "c:/temp/temping" -WhatIf:$WhatIf
    }
}

What-If -WhatIf