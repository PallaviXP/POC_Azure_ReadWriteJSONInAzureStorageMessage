# POC_Azure_ReadWriteJSONInAzureStorageMessage

# Date started: 9 Nov. 2017

This is .net core console application talking to Azure message queue having json format string.

this will do below thing
~~~~~~~~~~~~~~~~~~~~~~~~~~
1. take azure connection string from application configuration
2. connect to azure queue
3. read/get,  write/save json string in/from azure queue


Nuget packages required:
Microsoft.WindowsAzure.Storage
Newtonsoft.Json

refernace : https://briancaos.wordpress.com/2015/11/20/azure-cloudqueue-get-and-set-json-using-newtonsoft-json-and-microsoft-windowsazure-storage/


