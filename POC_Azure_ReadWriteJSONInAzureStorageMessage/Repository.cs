using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace POC_Azure_ReadWriteJSONInAzureStorageMessage
{
    public class Repository
    {
        private readonly CloudQueue _queue;

        public Repository(string queueName, string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudQueueClient client = storageAccount.CreateCloudQueueClient();
            _queue = client.GetQueueReference(queueName);
            _queue.CreateIfNotExistsAsync();
        }

        public IEnumerable<CloudQueueMessage> GetMessages(int count)
        {
            int batchRetrieveCount = 32; //max batchsize
            var retrievedMessages = new List<CloudQueueMessage>();
            do
            {
                if (count < 32)
                    batchRetrieveCount = count;
                IEnumerable<CloudQueueMessage> receivedBatch = _queue.GetMessagesAsync(batchRetrieveCount).Result;
                retrievedMessages.AddRange(receivedBatch);
                DeleteBatch(receivedBatch);
            } while ((count -= batchRetrieveCount) > 0);
            return retrievedMessages;
        }

        public CloudQueueMessage Peek()
        {
            return _queue.GetMessageAsync().Result;
        }

        public void Delete(CloudQueueMessage message)
        {
            _queue.DeleteMessageAsync(message);
        }

        public void AddMessage(object messageObject)
        {
            string serializedMessage = JsonConvert.SerializeObject(messageObject);
            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(serializedMessage);
            _queue.AddMessageAsync(cloudQueueMessage);
        }

        private void DeleteBatch(IEnumerable<CloudQueueMessage> batch)
        {
            foreach (CloudQueueMessage message in batch)
            {
                _queue.DeleteMessageAsync(message);
            }
        }

      
    }   
}
