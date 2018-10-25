using System.IO;
using System.Threading.Tasks;
using HighbushAutomation.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace HighbushFunctionApp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, [Queue("highbush-automation-queue", Connection = "AzureWebJobsStorage1")] IAsyncCollector<IotEvent> queueCollector, ILogger log)
        {
            IActionResult result;
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            IotEvent iotEvent = JsonConvert.DeserializeObject<IotEvent>(requestBody);
            //name = name ?? data?.name;

            if (iotEvent == null)
            {
                log.LogInformation("IOT event not provided.");
                result = new BadRequestObjectResult("Please pass IOT event in the request body.");
            }
            else
            {
                string message = $"IOT event was triggered.";
                await queueCollector.AddAsync(iotEvent);
                log.LogInformation(message);
                result = new OkObjectResult(message);
            }

            return result;
        }
    }
}
