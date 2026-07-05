using My.Function.Models;

namespace My.Function.Services;

public interface IWeatherService
{
    Task<WeatherReport?> GetWeatherAsync(string city, CancellationToken cancellationToken = default);
}
