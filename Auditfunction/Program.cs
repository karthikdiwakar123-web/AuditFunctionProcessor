using Auditfunction;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

string? fullyQualifiedNamespace = Environment.GetEnvironmentVariable("ServiceBusConnection__fullyQualifiedNamespace");

// DefaultAzureCredential automatically uses the managed identity
var credential = new DefaultAzureCredential();

var serviceBusClient = new ServiceBusClient(fullyQualifiedNamespace, credential);

builder.Services.AddSingleton(serviceBusClient);
builder.Services.AddSingleton<IAuditServiceBusProcessor, AuditServiceBusProcessor>();

builder.Build().Run();
