using Azure.Identity;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using My.Function.Services;
using OpenTelemetry;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.Configure<WeatherApiOptions>(
    builder.Configuration.GetSection(WeatherApiOptions.SectionName));

var weatherApiKey = builder.Configuration[$"{WeatherApiOptions.SectionName}:ApiKey"];
if (!string.IsNullOrWhiteSpace(weatherApiKey))
{
    builder.Services.AddHttpClient<IWeatherService, WeatherApiWeatherService>(client =>
    {
        client.BaseAddress = new Uri("https://api.weatherapi.com/v1/");
    });
}
else
{
    builder.Services.AddSingleton<IWeatherService, FakeWeatherService>();
}

if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")))
{
    builder.Services.AddOpenTelemetry()
        .UseFunctionsWorkerDefaults()
        .UseAzureMonitorExporter(options => options.Credential = new DefaultAzureCredential());
}

builder.Build().Run();
