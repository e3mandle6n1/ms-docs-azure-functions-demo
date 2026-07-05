using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace My.Function;

public class Heartbeat
{
    // NCRONTAB with seconds field: {second} {minute} {hour} {day} {month} {day-of-week}
    
    //private const string Every10Seconds = "*/10 * * * * *";
    private const string EveryMinute = "0 * * * * *";
    //private const string Every2Minutes = "0 */2 * * * *";
    //private const string EveryHour = "0 0 * * * *";
    //private const string EveryDay = "0 0 0 * * *";
    //private const string EveryWeek = "0 0 0 * * 0";
    //private const string EveryMonth = "0 0 0 1 * *";
    //private const string EveryYear = "0 0 0 1 1 *";
    //private const string EveryMinute = "0 * * * * *";
    //private const string Every2Minutes = "0 */2 * * * *";
    //private const string EveryHour = "0 0 * * * *";
    //private const string EveryDay = "0 0 0 * * *";

    private readonly ILogger<Heartbeat> _logger;

    public Heartbeat(ILogger<Heartbeat> logger)
    {
        _logger = logger;
    }

    [Function("Heartbeat")]
    public void Run([TimerTrigger(EveryMinute)] TimerInfo timer)
    {
        var environment = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") ?? "unknown";

        _logger.LogInformation(
            "Heartbeat at {TimestampUtc:O} (environment: {Environment}, isPastDue: {IsPastDue})",
            DateTimeOffset.UtcNow,
            environment,
            timer.IsPastDue);

        if (timer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next heartbeat scheduled for {NextUtc:O}", timer.ScheduleStatus.Next);
        }
    }
}
