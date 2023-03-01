using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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
        private readonly IRepositoryAsync<AreaOfExpertise> _areaOfExpertiseRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IRepositoryAsync<AreaOfExpertise> areaOfExpertiseRepository)
        {
            _logger = logger;
            _areaOfExpertiseRepository = areaOfExpertiseRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var a = await _areaOfExpertiseRepository.GetAllAsync();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}