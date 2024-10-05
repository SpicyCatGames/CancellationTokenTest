using Microsoft.AspNetCore.Mvc;
using System;

namespace CancellationTokenTest.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellationToken)
    {
        var weatherForecasts = new List<WeatherForecast>();

        for (int index = 1; index <= 5; index++)
        {
            // Check if the cancellation is requested
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Delay(1000, cancellationToken); // Pass the token to Task.Delay

            weatherForecasts.Add(new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
        }

        return weatherForecasts;
    }

}
