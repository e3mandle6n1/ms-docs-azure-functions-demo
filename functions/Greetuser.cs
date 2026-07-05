using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Models;

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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "greet")] HttpRequest req,
        [FromQuery] string? name,
        [FromQuery] GreetLanguage lang = GreetLanguage.En)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new BadRequestObjectResult(new { error = "Query parameter 'name' is required." });
        }

        var message = GetLocalizedGreeting(name, lang);

        _logger.LogInformation("Greeted {Name} (lang: {Lang})", name, lang);

        return new OkObjectResult(new { message });
    }

    private static string GetLocalizedGreeting(string name, GreetLanguage lang) => lang switch
    {
        GreetLanguage.Fr => $"Bonjour, {name}!",
        GreetLanguage.Es => $"Hola, {name}!",
        GreetLanguage.De => $"Hallo, {name}!",
        _ => $"Hello, {name}!"
    };
}
