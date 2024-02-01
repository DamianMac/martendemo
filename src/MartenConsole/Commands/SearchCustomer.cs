using Marten;
using MartenDemo.Model;
using Serilog;

namespace MartenDemo.Commands;

public class SearchCustomer : ICommand
{
    private readonly IDocumentStore _store;

    public SearchCustomer(IDocumentStore store)
    {
        _store = store;
    }

    public void Run()
    {

        using var session = _store.LightweightSession();

        var customer = session
            .Query<Customer>()
            .FirstOrDefault(c => c.Email == "damian@damianm.com");

        if (customer != null)
        {
            Log.Information("Found customer {first} {last} {email}", customer.FirstName, customer.LastName, customer.Email);    
        }
        else
        {
            Log.Warning("Customer not found");
        }
        

    }
}