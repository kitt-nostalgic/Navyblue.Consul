using Microsoft.AspNetCore.Mvc;

namespace Navyblue.Consul.Sample2.Controllers
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
        private readonly ITestApiService _testApiService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITestApiService testApiService)
        {
            _logger = logger;
            _testApiService = testApiService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
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



        [HttpGet]
        [Route("GetInvokeTestResult")]
        public async Task<string> GetInvokeTestResult()
        {
            try
            {
                return await _testApiService.GetInvokeTestResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}