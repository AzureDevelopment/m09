using System.Net.Http;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public class SomeLogic
    {
        private readonly ITelemetry _telemetry;

        public SomeLogic(ITelemetry telemetry)
        {
            _telemetry = telemetry;
        }

        [FunctionName("SomeLogic")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            _telemetry.SetUserContext("testId");
            _telemetry.Event(new Event
            {
                Name = "Starting some important operation",
                Context = req.HttpContext,
                Additional = new Dictionary<string, string>
                {
                    ["ts"] = DateTime.Now.ToString()
                }
            });

            var httpClient = new HttpClient();
            DateTime startOfDependencyCall = DateTime.Now;
            HttpResponseMessage result = await httpClient.GetAsync("https://google.pl");
            _telemetry.Dependency(new Dependency
            {
                Name = "Fetching something important",
                Request = result.RequestMessage,
                Response = result,
                Duration = (DateTime.Now - startOfDependencyCall).TotalMilliseconds
            });

            return new OkObjectResult(2 / "".Length);
        }
    }
}
