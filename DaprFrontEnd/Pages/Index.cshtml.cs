using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dapr.Client;

namespace DaprFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DaprClient _daprClient;

        public IndexModel(ILogger<IndexModel> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        private static CancellationTokenSource cts;
        public async Task OnGet()
        {
            cts = new CancellationTokenSource();
            var forecasts = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
                HttpMethod.Get,
                "MyBackEnd",
                "weatherforecast",
                cts.Token);

            ViewData["WeatherForecastData"] = forecasts;
            cts.Dispose();
        }

        public void OnPost()
        {
            cts.Cancel();
            cts.Dispose();
            ViewData["WeatherForecastData"] = new List<WeatherForecast>();
        }
    }
}