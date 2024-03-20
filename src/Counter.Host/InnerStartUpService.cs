using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Counter.Host;

public class InnerStartUpService : IHostedService
{
    private ILogger<InnerStartUpService> _logger;

    public InnerStartUpService(ILogger<InnerStartUpService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting...");
        Task.Delay(5000).Wait(CancellationToken.None);
        _logger.LogInformation("Started");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping...");
        Task.Delay(5000).Wait(CancellationToken.None);
        _logger.LogInformation("Stopped");
        return Task.CompletedTask;
    }
}