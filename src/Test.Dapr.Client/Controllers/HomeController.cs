using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Dapr.Client.Models;

namespace Test.Dapr.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly DaprClient daprClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(DaprClient daprClient, ILogger<HomeController> logger)
        {
            this.daprClient = daprClient;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var client = DaprClient.CreateInvokeHttpClient();
                var forecasts1 = await client.GetFromJsonAsync<IEnumerable<WeatherForecast>>("http://mybackend/weatherforecast");

                var forecasts2 = await daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
                                   HttpMethod.Get, "mybackend", "weatherforecast");
            }
            catch (Exception ex)
            {

            }
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}