using BLL.Contracts;
using DAL.Entities;
using DAL.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {//UniversityController
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRepositoryAsync<AreaOfExpertise> _areaOfExpertiseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUniversityService _universityService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IRepositoryAsync<AreaOfExpertise> areaOfExpertiseRepository,
            IUnitOfWork unitOfWork,
            IUniversityService universityService)
        {
            _logger = logger;
            _areaOfExpertiseRepository = areaOfExpertiseRepository;
            _unitOfWork = unitOfWork;
            _universityService = universityService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await _universityService.GetByIdAsync(new Guid());

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