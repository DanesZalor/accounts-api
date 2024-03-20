using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Counter.Host;

public class Program 
{
    public static void Main(string[] args)
    {
        var builder = Microsoft.Extensions.Hosting.Host
            .CreateApplicationBuilder(args);
        
        builder.Services
            .AddLogging()
            .AddHostedService<CounterService>()
            .AddHostedService<StartUpService>()
            .AddTransient<IMessageBroker, NullMessageBroker>();

        var app = builder.Build();
        app.Run();
    }
}
