using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceBusQueueConsumer
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([ServiceBusTrigger("add-weather-data", Connection = "WeatherDataConnectionString")]string myQueueItem, ILogger log)
        {
            //logic

            //For example
                // Save in DB
                // Send Email
                // Send data to Another API

            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
