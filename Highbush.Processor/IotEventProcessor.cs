using System;
using System.Threading.Tasks;
using HighbushAutomation.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace Highbush.Processor
{
    public static class IotEventProcessor
    {
        [FunctionName("IotEventProcessor")]
        public static async Task Run(
            [QueueTrigger("highbush-automation-queue", Connection = "AzureWebJobsStorage")]IotEvent iotEvent, [Inject]IEventSender eventSender, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {iotEvent}");      
            log.LogInformation($"Event type: {iotEvent.DoorEvent.DoorEventType}");
           
            string topicName = Environment.GetEnvironmentVariable("EventGridTopicEndpoint");
            string topicKey = Environment.GetEnvironmentVariable("EventGridTopicKey");
            await eventSender.SendAsync(iotEvent.DoorEvent, topicName, topicKey);
        }
    }
}