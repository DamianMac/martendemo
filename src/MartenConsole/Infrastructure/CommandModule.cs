using Autofac;
using MartenDemo.Commands;

namespace MartenDemo.Infrastructure;

public class CommandModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(ICommand).Assembly)
            .Where(t => t.IsAssignableTo(typeof(ICommand)))
            .Named<ICommand>(t => t.Name.ToLower())
            .InstancePerLifetimeScope();
    }
    
}