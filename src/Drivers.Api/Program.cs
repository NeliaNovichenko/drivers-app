using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Drivers.Domain;
using Drivers.Db;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services
    .AddDriversDb(builder.Configuration)
    .AddDomainServices();

if(Environment.GetEnvironmentVariable("KEYVAULT_URI") != null)
{
    string keyVaultUri = Environment.GetEnvironmentVariable("KEYVAULT_URI");
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
}

builder.Build().Run();

public partial class Program;
