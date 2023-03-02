using DAL.Entities;
using DAL.Contracts;
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
        private readonly IUnitOfWork _unitOfWork;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IRepositoryAsync<AreaOfExpertise> areaOfExpertiseRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _areaOfExpertiseRepository = areaOfExpertiseRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var a = await _areaOfExpertiseRepository.GetAllAsync();
            var b = new AreaOfExpertise
            {
                Name = "New name",
            };
            await _areaOfExpertiseRepository.AddAsync(b);

            await _unitOfWork.SaveChangesAsync();
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