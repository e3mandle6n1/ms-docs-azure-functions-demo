using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using My.Function.Models;

namespace My.Function.Services;

public class WeatherApiWeatherService : IWeatherService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly WeatherApiOptions _options;
    private readonly ILogger<WeatherApiWeatherService> _logger;

    public WeatherApiWeatherService(
        HttpClient httpClient,
        IOptions<WeatherApiOptions> options,
        ILogger<WeatherApiWeatherService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<WeatherReport?> GetWeatherAsync(string city, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return null;
        }

        var query = Uri.EscapeDataString(city.Trim());
        var path = $"current.json?key={Uri.EscapeDataString(_options.ApiKey)}&q={query}";

        using var response = await _httpClient.GetAsync(path, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        var payload = JsonSerializer.Deserialize<WeatherApiCurrentResponse>(body, JsonOptions);

        if (payload?.Error is { Code: 1006 })
        {
            _logger.LogInformation("WeatherAPI found no location for {City}", city);
            return null;
        }

        if (!response.IsSuccessStatusCode || payload?.Current is null || payload.Location is null)
        {
            var message = payload?.Error?.Message ?? response.ReasonPhrase ?? "Unknown WeatherAPI error";
            _logger.LogError(
                "WeatherAPI request failed for {City} with status {StatusCode}: {Message}",
                city,
                (int)response.StatusCode,
                message);
            throw new HttpRequestException(
                $"WeatherAPI request failed ({(int)response.StatusCode}): {message}",
                null,
                response.StatusCode);
        }

        var observedAt = payload.Current.LastUpdatedEpoch is long epoch
            ? DateTimeOffset.FromUnixTimeSeconds(epoch)
            : DateTimeOffset.UtcNow;

        return new WeatherReport(
            City: payload.Location.Name ?? city.Trim(),
            TemperatureC: (int)Math.Round(payload.Current.TempC),
            Condition: payload.Current.Condition?.Text ?? "Unknown",
            ObservedAt: observedAt);
    }

    private sealed class WeatherApiCurrentResponse
    {
        public WeatherApiLocation? Location { get; init; }
        public WeatherApiCurrent? Current { get; init; }
        public WeatherApiError? Error { get; init; }
    }

    private sealed class WeatherApiLocation
    {
        public string? Name { get; init; }
    }

    private sealed class WeatherApiCurrent
    {
        [JsonPropertyName("temp_c")]
        public decimal TempC { get; init; }

        public WeatherApiCondition? Condition { get; init; }

        [JsonPropertyName("last_updated_epoch")]
        public long? LastUpdatedEpoch { get; init; }
    }

    private sealed class WeatherApiCondition
    {
        public string? Text { get; init; }
    }

    private sealed class WeatherApiError
    {
        public int Code { get; init; }
        public string? Message { get; init; }
    }
}
