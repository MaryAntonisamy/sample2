using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<DataTransferScheduler>();
        // Register your Cosmos DB and Service Bus clients here using the configuration
    })
    .Build();

await host.RunAsync();
