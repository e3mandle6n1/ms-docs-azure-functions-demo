using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace My.Function;

public class GreetUser
{
    private readonly ILogger<GreetUser> _logger;

    public GreetUser(ILogger<GreetUser> logger)
    {
        _logger = logger;
    }

    [Function("GreetUser")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "greet")] HttpRequest req)
    {
        var name = req.Query["name"].ToString();

        if (string.IsNullOrWhiteSpace(name))
        {
            return new BadRequestObjectResult(new { error = "Query parameter 'name' is required." });
        }

        var lang = req.Query["lang"].ToString();
        var message = string.IsNullOrWhiteSpace(lang)
            ? $"Hello, {name}!"
            : GetLocalizedGreeting(name, lang);

        _logger.LogInformation("Greeted {Name} (lang: {Lang})", name, lang ?? "en");

        return new OkObjectResult(new { message });
    }

    private static string GetLocalizedGreeting(string name, string lang) => lang.ToLowerInvariant() switch
    {
        "fr" => $"Bonjour, {name}!",
        "es" => $"Hola, {name}!",
        "de" => $"Hallo, {name}!",
        _ => $"Hello, {name}!"
    };
}