using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using Template.Application.DependencyInjection;
using Template.Infrastructure.DependencyInjection;
using Template.WebApi.Extensions;

namespace Template.WebApi;

[ExcludeFromCodeCoverage]
public class Program
{
    protected Program()
    {
    }

    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .CreateLogger();

        Log.Information("Starting up");

        try
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Host.Configure();
            builder.Services.Configure();
            builder.Services.AddInfrastructure(builder.Configuration);

            WebApplication app = builder.Build();
            app.UseInfrastructure(builder.Configuration);

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.Information("Server shutting down...");
            Log.CloseAndFlush();
        }
    }
}