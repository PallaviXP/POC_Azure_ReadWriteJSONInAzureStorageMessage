#get azure rm version
Get-Module -ListAvailable AzureRM

Get-Module PowerShellGet -list | Select-Object Name,Version,Path

#login azure subscription
Login-AzureRmAccount

#get list of locations
Get-AzureRmLocation | select Location 

#select east asia in location variable
$location = "eastasia"

#create new resource group
$resourceGoraroup = "myqueueresourcegroup"
New-AzureRMResourceGroup -ResourceGroupName $resourceGroup -Location $location

#LRS=locally redundant storage = create  standard general purpose storage account
$storageAccountName = "howtoqueuestorage"
$storageAccount = New-AzureRmStorageAccount -ResourceGroupName $resourceGroup Name $storageAccountName -Location $location -SkuName Standard_LRS
$ctx = $storageAccount.Context

#get context for later use

$queueName = "howtoqueue"

$queue = Get-AzureStorageQueue -name $queueName -Context $ctx
if(-not $queue){ 
   $queue = New-AzureStorageQueue –Name $queueName -Context $ctx
}

#$queue = New-AzureStorageQueue –Name $queueName -Context $ctx

#list out all properties of queue
$queue

#perticular property
$queue | select name

#get all queue list in resource group
Get-AzureStorageQueue -Context $ctx
Get-AzureStorageQueue -Context $ctx | select Name

#addv new message to the queue
# Create a new message using a constructor of the CloudQueueMessage class
$queueMessage = New-Object -TypeName Microsoft.WindowsAzure.Storage.Queue.CloudQueueMessage -ArgumentList "This is message 1"
# Add a new message to the queue
#$queue.CloudQueue.AddMessage($QueueMessage)


