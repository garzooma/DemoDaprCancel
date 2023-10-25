using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HttpFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        private static CancellationTokenSource cts;
        public async Task OnGet()
        {
            cts = new CancellationTokenSource();
            string serverUrl = $"http://mybackend/WeatherForecast";
            List<WeatherForecast>? forecasts = await _httpClient.GetFromJsonAsync<List<WeatherForecast>>(serverUrl, cts.Token);
            ViewData["WeatherForecastData"] = forecasts;
            cts.Dispose();
        }

        public void OnPost()
        {
            cts.Cancel();
            cts.Dispose();
            //_httpClient.CancelPendingRequests();
            ViewData["WeatherForecastData"] = new List<WeatherForecast>();
        }
    }
}