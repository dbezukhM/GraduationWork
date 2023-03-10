using BLL.Contracts;
using DAL.Entities;
using DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
        private readonly IEpRepositoryAsync<AreaOfExpertise> _areaOfExpertiseRepository;
        private readonly IWpRepositoryAsync<WorkingProgram> _workingProgramRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUniversityService _universityService;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<Person> _userManager;
        private readonly IAccountService _accountService;

        private readonly SignInManager<Person> _signInManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IEpRepositoryAsync<AreaOfExpertise> areaOfExpertiseRepository,
            IUnitOfWork unitOfWork,
            IUniversityService universityService,
            IWpRepositoryAsync<WorkingProgram> workingProgramRepository, UserManager<Person> userManager, ITokenGenerator tokenGenerator, SignInManager<Person> signInManager, IAccountService accountService)
        {
            _logger = logger;
            _areaOfExpertiseRepository = areaOfExpertiseRepository;
            _unitOfWork = unitOfWork;
            _universityService = universityService;
            _workingProgramRepository = workingProgramRepository;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<WeatherForecast>> Get()
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
        [Authorize(Roles = "Lecturer")]
        [Route("GetStrings")]
        public IEnumerable<string> GetString()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }
    }
}