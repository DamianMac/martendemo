using Marten;
using MartenDemo.Model;
using Serilog;

namespace MartenDemo.Commands;

public class CreateCustomer : ICommand
{
    private readonly IDocumentStore _store;

    public CreateCustomer(IDocumentStore store)
    {
        _store = store;
    }

    public void Run()
    {
        using var session = _store.LightweightSession();

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = "Damian",
            LastName = "Maclennan",
            Email = "damian@damianm.com"
        };
        
        session.Store(customer);
        session.SaveChanges();

    }
}