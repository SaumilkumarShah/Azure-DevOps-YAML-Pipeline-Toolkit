param(
[Parameter(Mandatory=$true)]
[string]$resourceGroups
)

$resourceGrouparray= $resourceGroups -split ","

if ($resourceGrouparray)
{
    # run for reach resource group
    foreach ($resourceGroup in $resourceGrouparray)
    {
        #check if any resource exists inside resource group
        $resourcedetails=Get-AzResource | ?{$_.ResourceGroupName -eq $resourceGroup}
        if($resourcedetails -eq $null)
        {
            write-output "Resourcegroup:$resourceGroup is empty.No resources to delete"
        }
        else
        {
            # Move each resource in resource group 
            foreach($resourcedetail in $resourcedetails)  
            {
               write-output "Deleting $($resourcedetail.name) from $($resourcedetail.ResourceGroupName)"
               Remove-AzResource -ResourceName $resourcedetail.name -ResourceGroupName $resourcedetail.ResourceGroupName -ResourceType $resourcedetail.ResourceType -ErrorAction SilentlyContinue -Force
            }
        }
        $count= (Get-AzResource | Where-Object{ $_.ResourceGroupName -eq $resourceGroup }).Count
       
        # to crosscheck all resources are deleted (no resource left)
        if($count -eq 0)
        {
            Write-Output "Deleting Resourcegroup:$resourceGroup"
            Remove-AzResourceGroup -Name $resourceGroup -Force
        }
    }
}
else
{
  Write-Output "No Resourcegroup provided in input. "
}