param(
    [string]$resourceType,
    [string]$resourcePurpose,
    [string]$componentID,
    [string]$subEnvironment,
    [Parameter(Mandatory = $true)]
    [string]$resourceGroupName,
    [Parameter(Mandatory = $true)]
    [string]$resourceName,
    [string]$apiVersion
)

$tag = @{Environment = $env:env.ToUpper(); ComponentID = $componentID; Sub_Environment = $subEnvironment; Type = $resourceType; Purose = $resourcePurpose }

$resource = Get-AzResource -ResourceGroupName $resourceGroupName -Name $resourceName

if ($apiVersion) {
    Set-AzResource -ResourceId $resource.Id -Tag $tag -ApiVersion $apiVersion -Force
}
else {
    Set-AzResource -ResourceId $resource.Id -Tag $tag -Force -ErrorAction SilentlyContinue
}