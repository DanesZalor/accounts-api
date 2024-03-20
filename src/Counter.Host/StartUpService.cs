using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Counter.Host;

public class StartUpService : IHostedService
{
    private ILogger<StartUpService> _logger;
    private IMessageBroker _mb;

    public StartUpService(ILogger<StartUpService> logger, IMessageBroker mb)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mb = mb ?? throw new ArgumentNullException(nameof(mb));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting...");
        Task.Delay(500).Wait(CancellationToken.None);
        _logger.LogInformation("Started");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping...");
        Task.Delay(500).Wait(CancellationToken.None);
        _logger.LogInformation("Stopped");
        return Task.CompletedTask;
    }
}

public interface IMessageBroker
{
    Task Send(string msg);
}

public class NullMessageBroker : IMessageBroker, IDisposable
{
    public void Dispose()
    {
        Console.WriteLine($"Disposing {this}");
    }

    public Task Send(string msg)
    {
        Console.WriteLine($"{this}: Sending message '{msg}'");
        return Task.CompletedTask;
    }
}