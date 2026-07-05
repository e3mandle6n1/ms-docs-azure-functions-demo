using My.Function.Models;

namespace My.Function.Services;

/// <summary>
/// In-memory stand-in for a real weather API. Results are deterministic per city
/// (seeded from the city name) so repeated calls return the same "weather".
/// </summary>
public class FakeWeatherService : IWeatherService
{
    private static readonly string[] Conditions =
    [
        "Sunny",
        "Partly cloudy",
        "Overcast",
        "Light rain",
        "Thunderstorm",
        "Snow",
        "Fog"
    ];

    public Task<WeatherReport?> GetWeatherAsync(string city, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return Task.FromResult<WeatherReport?>(null);
        }

        // Deterministic seed so the same city always gets the same forecast.
        var seed = city.Trim().ToLowerInvariant()
            .Aggregate(17, (hash, c) => hash * 31 + c);
        var random = new Random(seed);

        var report = new WeatherReport(
            City: Capitalize(city.Trim()),
            TemperatureC: random.Next(-10, 36),
            Condition: Conditions[random.Next(Conditions.Length)],
            ObservedAt: DateTimeOffset.UtcNow);

        return Task.FromResult<WeatherReport?>(report);
    }

    private static string Capitalize(string value) =>
        string.Create(value.Length, value, static (span, source) =>
        {
            source.AsSpan().ToLowerInvariant(span);
            span[0] = char.ToUpperInvariant(span[0]);
        });
}
