using System.Net;
using Microsoft.AspNetCore.HttpLogging;

namespace Something.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDependencies();

        builder.Services
            .AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
            })
            .AddControllers();
        
        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseHttpLogging();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}