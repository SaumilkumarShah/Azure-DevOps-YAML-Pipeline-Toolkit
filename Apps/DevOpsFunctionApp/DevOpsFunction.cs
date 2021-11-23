using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DevOpsFunctionApp
{
    public static class DevOpsFunction
    {
        [FunctionName("DevOpsFunction")]
        public static void Run([QueueTrigger( "%queuename%" , Connection = "StorageConnectionAppSetting")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
