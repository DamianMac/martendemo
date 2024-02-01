using Marten;
using MartenDemo.Model;

namespace MartenDemo.Commands;

public class CreateOrders : ICommand
{
    private readonly IDocumentStore _store;

    public CreateOrders(IDocumentStore store)
    {
        _store = store;
    }

    public void Run()
    {

        using var session = _store.LightweightSession();

        var customer = session
            .Query<Customer>()
            .First(c => c.Email == "damian@damianm.com");
        var products = session
            .Query<Product>()
            .Where(p => p.Price > 3)
            .ToList();

        var order = new Order
        {
            CustomerId = customer.Id,
            Status = OrderStatus.Pending,
            OrderDate = DateTimeOffset.Now,
            Products = products,
        };
        
        session.Store(order);
        session.SaveChanges();

    }
}