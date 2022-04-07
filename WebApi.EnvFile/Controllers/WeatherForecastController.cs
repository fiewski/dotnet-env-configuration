using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.EnvFile.Models;

namespace WebApi.EnvFile.Controllers
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
        private readonly IOptions<RabbitConfig> _config;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<RabbitConfig> config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = $"Host:{_config.Value.Host} | Port: {_config.Value.Port}"
            })
            .ToArray();
        }
    }
}