using System.Net;
using Microsoft.AspNetCore.HttpLogging;

namespace Something.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpLogging();
        app.UsePathBase("/public");
        app.UseAuthorization();
        app.MapControllers();
        Console.WriteLine("I am HTTP");
        app.Run();
    }
}