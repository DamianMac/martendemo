using Serilog;

namespace MartenDemo.Commands;

public class HelloCommand : ICommand
{
    public void Run()
    {
        
        Log.Information("HI Brisbane .NET UG!");
    }
}