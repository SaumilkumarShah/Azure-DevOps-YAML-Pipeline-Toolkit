##Parameters
param
(
    [Parameter(Mandatory = $true)]
    [string]$FilePath
)

#Modules
Write-Output 'Installing powershell module'
Install-Module -Name PSScriptAnalyzer -Force

$ScriptAnalyzerRules = Get-ScriptAnalyzerRule -Severity Warning
$ScriptAnalyzerResult = Invoke-ScriptAnalyzer -Path $FilePath -IncludeRule $ScriptAnalyzerRules
If ( $ScriptAnalyzerResult ) {  
    $ScriptAnalyzerResultString = $ScriptAnalyzerResult | Out-String
    Write-Warning $ScriptAnalyzerResultString
}