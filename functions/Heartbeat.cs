using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Time;

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
    private readonly AppTimeZone _appTimeZone;

    public Heartbeat(ILogger<Heartbeat> logger, AppTimeZone appTimeZone)
    {
        _logger = logger;
        _appTimeZone = appTimeZone;
    }

    [Function("Heartbeat")]
    public void Run([TimerTrigger(EveryMinute)] TimerInfo timer)
    {
        var environment = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") ?? "unknown";

        _logger.LogInformation(
            "Heartbeat at {Timestamp:O} ({TimeZone}, environment: {Environment}, isPastDue: {IsPastDue})",
            _appTimeZone.Now,
            _appTimeZone.Id,
            environment,
            timer.IsPastDue);

        if (timer.ScheduleStatus is not null)
        {
            _logger.LogInformation(
                "Next heartbeat scheduled for {Next:O} ({TimeZone})",
                _appTimeZone.Convert(timer.ScheduleStatus.Next),
                _appTimeZone.Id);
        }
    }
}
