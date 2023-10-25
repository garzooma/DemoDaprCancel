using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MyBackEnd.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get(CancellationToken token)
        {
            bool cancelled = false;
            token.Register(() => {
                Debug.WriteLine("Cancelled");
                cancelled = true;
            });

            List<WeatherForecast> forecasts = new List<WeatherForecast>();
            for (int i = 1; i < 6; i++)
            {
                if (cancelled)
                {
                    Debug.WriteLine($"Cancelled after {i} iterations");
                    break;
                }
                Debug.WriteLine($"Iter #{i}");
                forecasts.Add(new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(i),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                });
                Task.Delay(1000).Wait();
            }

            return forecasts;
        }
    }
}