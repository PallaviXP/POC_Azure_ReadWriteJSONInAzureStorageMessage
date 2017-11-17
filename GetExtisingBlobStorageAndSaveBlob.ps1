#howtoqueuestorage1  = storage account
# myqueueresourcegroup = resource group


Login-AzureRmAccount
$account = Get-AzureRmStorageAccount -Name "howtoqueuestorage1" -ResourceGroupName "myqueueresourcegroup"
$account
$con = $account.Context

#listout container named mycontainer
Get-AzureStorageContainer -Name my* -Context $con

#uploads the blob
$containerName = "mycontainer"
Set-AzureStorageBlobContent -File "D:\Personal\2.bmp" -Container $containerName -Blob "2" -Context $con

#list out the blobs
Get-AzureStorageBlob -Container $ContainerName -Context $ctx | select Name 

#download the blob
Get-AzureStorageBlobContent -Blob "2" -Container $containerName -Destination "d:\" -Context $con