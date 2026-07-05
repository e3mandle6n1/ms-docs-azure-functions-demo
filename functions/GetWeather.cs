using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Services;

namespace My.Function;

public class GetWeather
{
    private readonly IWeatherService _weatherService;
    private readonly ILogger<GetWeather> _logger;

    public GetWeather(IWeatherService weatherService, ILogger<GetWeather> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    [Function("GetWeather")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "weather/{city}")] HttpRequest req,
        string city)
    {
        var report = await _weatherService.GetWeatherAsync(city, req.HttpContext.RequestAborted);

        if (report is null)
        {
            return new NotFoundObjectResult(new { error = $"No weather data available for '{city}'." });
        }

        _logger.LogInformation(
            "Weather for {City}: {TemperatureC}°C, {Condition}",
            report.City,
            report.TemperatureC,
            report.Condition);

        return new OkObjectResult(report);
    }
}
