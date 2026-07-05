namespace My.Function.Models;

public record WeatherReport(
    string City,
    int TemperatureC,
    string Condition,
    DateTimeOffset ObservedAt)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
