using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace POC_Azure_ReadWriteJSONInAzureStorageMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionstring = "DefaultEndpointsProtocol=https;AccountName=contoso;AccountKey=nL0gEZyM7ToXUzdQ==;EndpointSuffix=core.windows.net";

            Console.WriteLine("Hello World! Put your inputs when asked...!");

            Book book = new Book();

            Console.WriteLine("Enter Book Name : ");
            book.BookName = Console.ReadLine();
            Console.WriteLine("Enter Author : ");
            book.Author = Console.ReadLine();

            Console.WriteLine("Adding message in queue....");
            var rep = new Repository("myqueue", connectionstring);
            rep.AddMessage(book);

        
            IEnumerable<CloudQueueMessage> messages = rep.GetMessages(5);
            foreach (CloudQueueMessage message in messages)
            {
                string jsonString = message.AsString;
                Book myMessage = Deserialize<Book>(jsonString, true);

                Console.WriteLine("Book {0}{1}", myMessage.BookName, myMessage.Author);

            }



            Console.ReadLine();
        }

        public static T Deserialize<T>(string json, bool ignoreMissingMembersInObject) where T : class
        {
            T deserializedObject;
            try
            {
                MissingMemberHandling missingMemberHandling = MissingMemberHandling.Error;
                if (ignoreMissingMembersInObject)
                    missingMemberHandling = MissingMemberHandling.Ignore;
                deserializedObject = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
                {
                    MissingMemberHandling = missingMemberHandling,
                });
            }
            catch (JsonSerializationException)
            {
                return null;
            }
            return deserializedObject;
        }
    }

    public class Book
    {
        [JsonProperty("bookname", Required = Required.Always)]
        public string BookName { get; set; }

        [JsonProperty("bookauthor", Required = Required.Always)]
        public string Author { get; set; }
    }
}
