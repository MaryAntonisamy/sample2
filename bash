dotnet new console -n CosmosDbToServiceBusScheduler -o CosmosDbToServiceBusScheduler
cd CosmosDbToServiceBusScheduler

dotnet add package Microsoft.Azure.Cosmos
dotnet add package Azure.Messaging.ServiceBus
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Hosting
dotnet add package Microsoft.Extensions.Configuration
