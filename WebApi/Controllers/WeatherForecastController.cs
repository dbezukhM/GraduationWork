using AutoMapper;
using BLL.Contracts;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BLL.Settings;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ResultController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ProgramSettings _settings;
        private readonly IFileGenerator _fileGenerator;
        private readonly IFileProvider _fileProvider;

        private readonly SignInManager<Person> _signInManager;

        public WeatherForecastController(
            SignInManager<Person> signInManager,
            IOptionsSnapshot<ProgramSettings> settings,
            IFileGenerator fileGenerator,
            IMapper mapper, IFileProvider fileProvider) : base(mapper)
        {
            _signInManager = signInManager;
            _fileGenerator = fileGenerator;
            _fileProvider = fileProvider;
            _settings = settings.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        //[Authorize(Roles = "Admin")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var a = _settings;
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

        [HttpGet]
        [Route("GenerateFile/{id}")]
        public async Task<IActionResult> GenerateFile(Guid id)
        {
            var result = await _fileGenerator.GenerateFile(id);

            if (result.IsFailed)
            {
                return ErrorResult(result);
            }

            return File(result.Value.File.ToArray(), "application/vnd.ms-word", result.Value.FullFileName);
        }

        public class CreateModel
        {
            public string Name { get; set; }

            public IFormFile File { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> PostFile([FromForm] CreateModel model)
        {
            var result = await _fileProvider.PostFileAsync(model.File);

            return OperationResult(result);
        }
    }
}