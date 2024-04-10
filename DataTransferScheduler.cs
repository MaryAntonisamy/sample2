using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class DataTransferScheduler : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceProvider _services;

    public DataTransferScheduler(IServiceProvider services)
    {
        _services = services;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, 
            TimeSpan.FromMinutes(30)); // Set your interval here

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        // Implement your logic to fetch from Cosmos DB and publish to Service Bus
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
