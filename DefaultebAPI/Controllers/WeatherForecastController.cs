using DefaultebAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DefaultebAPI.Controllers
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
        private readonly ApplicationDBContext _dBContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDBContext dBContext)
        {
            _logger = logger;
            _dBContext = dBContext;
        }

        [Route("getforecast")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [NonAction]
        public IActionResult Test()
        {
            return Ok("This is a dummy");
        }

        [Route("getDataById")]
        [HttpGet("{id}")]
        public IEnumerable<Standard> StandardData([FromQuery] int id)
        {
            var dbData = _dBContext.Standard.Include(c => c.Students).Where(c => c.StandardId == id).ToList();
            return dbData;
        }
    }
}
