using Microsoft.AspNetCore.Builder;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using My.Function.ModelBinding;
using My.Function.Repositories;
using My.Function.Services;
using My.Function.Swagger;
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

var blobServiceUri = builder.Configuration["AzureWebJobsStorage:BlobServiceUri"]
    ?? throw new InvalidOperationException("AzureWebJobsStorage:BlobServiceUri is required for todo persistence.");

var storageClientId = builder.Configuration["AzureWebJobsStorage:ClientId"];
var credentialOptions = new DefaultAzureCredentialOptions();
if (!string.IsNullOrWhiteSpace(storageClientId))
{
    credentialOptions.ManagedIdentityClientId = storageClientId;
}

builder.Services.AddSingleton(_ => new BlobServiceClient(new Uri(blobServiceUri), new DefaultAzureCredential(credentialOptions)));
builder.Services.AddSingleton<ITodoRepository, BlobTodoRepository>();

if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")))
{
    builder.Services.AddOpenTelemetry()
        .UseFunctionsWorkerDefaults()
        .UseAzureMonitorExporter(options => options.Credential = new DefaultAzureCredential());
}

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Azure Functions Demo API",
        Version = "v1"
    });
    options.SchemaFilter<StringEnumSchemaFilter>();
    options.UseInlineDefinitionsForEnums();
});

builder
    .ConfigureAspNetCoreMvcIntegration(mvcBuilder =>
    {
        mvcBuilder.AddMvcOptions(options =>
        {
            options.ModelBinderProviders.Insert(0, new EnumMemberModelBinderProvider());
        });
    })
    .UseAspNetCoreMiddleware(app =>
    {
        app.UseFunctionSwaggerUI();
    });

builder.Build().Run();
