using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BLL.Settings;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApi.Models;

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

        public WeatherForecastController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet(Name = "GetWeatherForecast")]
        //[Authorize(Roles = "Admin")]
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