using Marten;
using MartenDemo.Model;
using Serilog;

namespace MartenDemo.Commands;

public class FindOrders : ICommand
{
    private readonly IDocumentStore _store;

    public FindOrders(IDocumentStore store)
    {
        _store = store;
    }

    public void Run()
    {

        using var session = _store.LightweightSession();
        
        var customer = session
            .Query<Customer>()
            .First(c => c.Email == "damian@damianm.com");

        var ordersForCustomer = session
            .Query<Order>()
            .Where(o => o.CustomerId == customer.Id)
            .ToList();
        
        Log.Information("Found {count} orders", ordersForCustomer.Count);

    }
}