
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace HighbushFunctionApp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            IActionResult result;
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            IotEvent data = JsonConvert.DeserializeObject<IotEvent>(requestBody);
            //name = name ?? data?.name;

            if (data == null)
            {
                log.LogTrace("IOT event not provided.");
                result = new BadRequestObjectResult("Please pass IOT event in the request body.");
            }
            else
            {
                string message = $"IOT event {data.Event} triggered for {data.Device} device.";
                log.LogDebug(message);
                result = new OkObjectResult(message);
            }

            return result;
        }
    }
}
