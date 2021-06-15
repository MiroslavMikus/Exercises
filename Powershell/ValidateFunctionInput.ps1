# https://github.com/borough11/Install-Application/blob/master/Install-Application.ps1
[CmdletBinding(DefaultParametersetName='None')]
param(
    [String]
    [Parameter(Position=1,Mandatory=$true)]
    [ValidateScript( {($_ -ne '') -And (Test-Path $_)} )]
    $InstallerPath,

    [String[]]
    [Parameter()]
    $InstallerParameters='',

    [String]
    [Parameter()]
    [ValidateScript( {($_ -ne '') -And (Test-Path $_)} )]
    $LogPath,

    [String]
    [Parameter()]
    [ValidateNotNullOrEmpty()]
    $RegistryKey='',

    [String]
    [Parameter(ParameterSetName='RegExtra',Mandatory=$True)]
    [ValidateNotNullOrEmpty()]
    $RegistryName='',

    [String]
    [Parameter(ParameterSetName='RegExtra',Mandatory=$True)]
    [ValidateNotNullOrEmpty()]
    $RegistryValue='',

    [Switch]
    [Parameter()]
    $SkipIfRunOnceSet
)