using Microsoft.AspNetCore.Mvc;
using ResCad.Application.Interfaces;
using ResCad.Dominio.Dtos;

namespace ResCad.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(
            ILogger<WeatherForecastController> logger,

            IResidentesAplService residentesAplService
        ) : ControllerBase
    {
        private readonly IResidentesAplService _residentesAplService = residentesAplService;
        private readonly ILogger<WeatherForecastController> _logger = logger;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("residentes")]
        public async Task<ActionResult<ResidentesDto>> GetAll()
        {
            var residentes = await _residentesAplService.GetAllResidentes();
            return Ok(residentes);
        }
    }
}
