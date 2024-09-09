using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueueTrigger
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([QueueTrigger("myshowitqueue-items", Connection = "QueueConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
