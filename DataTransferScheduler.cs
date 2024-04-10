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
        // Example state object that could be used to control behavior in DoWorkAsync
        var state = new TimerState { Name = "FetchAndPublish" };
        _timer = new Timer(DoWork, state, TimeSpan.Zero, TimeSpan.FromMinutes(30));
        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        var timerState = state as TimerState;
        Console.WriteLine($"Timer ticked with state: {timerState?.Name}");

        // Run the async operation, handling exceptions
        Task.Run(async () => await DoWorkAsync(timerState)).ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                // Log or handle the exception as needed
                Console.WriteLine(task.Exception);
            }
        });
    }

    private async Task DoWorkAsync(TimerState state)
    {
        // Placeholder for determining action based on state
        if (state?.Name == "FetchAndPublish")
        {
            Console.WriteLine("Performing fetch and publish operation");
            // Implement fetching from Cosmos DB and publishing to Service Bus here
            await Task.Delay(1000); // Simulate asynchronous work
        }
        else
        {
            // Handle other states or default action
            Console.WriteLine("Performing default or other state-specific operation");
            await Task.Delay(500); // Simulate asynchronous work for other operations
        }
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

// Example state class used with the Timer
public class TimerState
{
    public string Name { get; set; }
}
