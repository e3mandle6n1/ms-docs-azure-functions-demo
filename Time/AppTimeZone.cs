using Microsoft.Extensions.Configuration;

namespace My.Function.Time;

/// <summary>
/// Converts timestamps to the app's configured IANA time zone.
/// Flex Consumption does not support WEBSITE_TIME_ZONE/TZ, so logging uses this instead.
/// </summary>
public sealed class AppTimeZone
{
    public const string ConfigKey = "APP_TIME_ZONE";

    private readonly TimeZoneInfo _timeZone;

    public AppTimeZone(IConfiguration configuration)
        : this(configuration[ConfigKey])
    {
    }

    public AppTimeZone(string? timeZoneId)
    {
        _timeZone = Resolve(timeZoneId);
    }

    public string Id => _timeZone.Id;

    public DateTimeOffset Now => TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, _timeZone);

    public DateTimeOffset Convert(DateTimeOffset value) =>
        TimeZoneInfo.ConvertTime(value, _timeZone);

    private static TimeZoneInfo Resolve(string? timeZoneId)
    {
        // Default matches the project's only allowed Azure region (southafricanorth).
        var id = string.IsNullOrWhiteSpace(timeZoneId) ? "Africa/Johannesburg" : timeZoneId;
        return TimeZoneInfo.FindSystemTimeZoneById(id);
    }
}
