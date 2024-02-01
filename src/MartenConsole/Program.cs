using System;
using Autofac;
using MartenDemo.Commands;
using Microsoft.Extensions.Configuration;
using Serilog;

public class Program
{

    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .CreateLogger();
        
        var builder = new ContainerBuilder();
        builder.RegisterAssemblyModules(typeof(Program).Assembly);
        builder.Register(ctx => configuration).As<IConfiguration>().InstancePerLifetimeScope();

        // Build the container
        var container = builder.Build();

        try
        {
            var command = container.ResolveNamed<ICommand>(args[0].ToLower());
            command.Run();
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to run command");
            throw;
        }
        
        
        Log.Information("Done");
        
    }
    
}