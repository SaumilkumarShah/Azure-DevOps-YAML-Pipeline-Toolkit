Param(	
	[string] [Parameter(Mandatory=$true)]
	$resourceGroupName,
		
	[string] [Parameter(Mandatory=$true)]
	$keyVaultName
)

Set-strictmode -version latest
$ErrorActionPreference = 'stop'


$VstsSpnApplicationId = (Get-AzContext).Account.Id
Set-AzKeyVaultAccessPolicy -VaultName $keyVaultName -ResourceGroupName $resourceGroupName -ServicePrincipalName $VstsSpnApplicationId -PermissionsToCertificates get, list, delete, create, import, update, managecontacts, getissuers, listissuers, setissuers, deleteissuers, manageissuers, recover, purge, backup, restore -PermissionsToKeys decrypt, encrypt, unwrapKey, wrapKey, verify, sign, get, list, update, create, import, delete, backup, restore, recover, purge -PermissionsToSecrets get, list, set, delete, backup, restore, recover, purge