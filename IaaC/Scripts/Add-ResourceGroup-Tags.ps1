param(
    [Parameter(Mandatory=$true)]
    [string]$resourceGroupName,
    [string]$componentID
)

if ($env:env -ne "prod")
{
    $environmentValue="PRE-PRODUCTION"
}
else
{
    $environmentValue="PRODUCTION"
}

$tag = @{Environment=$environmentValue;ComponentID=$componentID}

$resource = Get-AzResourceGroup -Name $resourceGroupName

Set-AzResource -ResourceId $resource.ResourceId -Tag $tag -Force