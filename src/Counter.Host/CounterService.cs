using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Counter.Host;

public class CounterService : IHostedService
{
    public ILogger<CounterService> _logger;

    public CounterService(ILogger<CounterService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException("amongus");
    }
    
    private async Task StartCounter(CancellationToken cancellationToken)
    {
        for(int i = 0; !cancellationToken.IsCancellationRequested; i++)
        {
            if(i%5==0)
            {
                await Task.Delay(1000);
                _logger.LogInformation($"Counter tick: {i}");

                await DumpFile(cancellationToken, $"Counter tick: {i}");
            }
        }
    }

    private async Task DumpFile(CancellationToken cancellationToken, string content="Empty")
    {
        using var file = File.Create("filedump");
        
        var info = new UTF8Encoding(true).GetBytes(content);
        
        Environment.SetEnvironmentVariable("COUNTER_TICK", content, EnvironmentVariableTarget.User);
        
        await file.WriteAsync(info, cancellationToken);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting Counter Service...");
        
        Task.Run(() => StartCounter(cancellationToken));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Counter Service...");
        return Task.CompletedTask;
    }
}
