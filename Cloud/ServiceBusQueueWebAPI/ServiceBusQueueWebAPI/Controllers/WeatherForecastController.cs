using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ServiceBusQueueWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task Post(WeatherForecast data)
        {
            string connectionString = "Endpoint=sb://servicebusqueuewebapi.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yVMiwZNu3XeFa5PJSOaEbWxziMVcESanQ+ASbGcoqEg=";

            var client = new ServiceBusClient(connectionString);
            var sender = client.CreateSender("add-weather-data");

            var body = JsonSerializer.Serialize(data); //json body return
            var msg = new ServiceBusMessage(body);

            if (body.Contains("Schedule"))
            {
                msg.ScheduledEnqueueTime = DateTime.UtcNow.AddSeconds(15);
            }

            if (body.Contains("time"))
            {
                msg.TimeToLive = TimeSpan.FromSeconds(10);
            }

            await sender.SendMessageAsync(msg);
        }
    }
}