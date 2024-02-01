using Marten;
using MartenDemo.Model;

namespace MartenDemo.Commands;

public class CreateProducts : ICommand
{
    private readonly IDocumentStore _store;

    public CreateProducts(IDocumentStore store)
    {
        _store = store;
    }

    public void Run()
    {
        using var session = _store.LightweightSession();

        var icecream = new Product
        {
            Id = "ICREAM",
            Name = "Ice Cream",
            Price = 5.40,
            ActiveDate = DateTimeOffset.Now
        };
        session.Store(icecream);

        var products = new List<Product>()
        {
            new Product
            {
                Id = "COKE",
                Name = "Coca Cola",
                Price = 3.50,
                ActiveDate = DateTimeOffset.Now,
            },
            new Product
            {
                Id = "DONUT",
                Name = "Iced Choc Sprinkle Donut",
                Price = 2.35,
                ActiveDate = DateTimeOffset.Now
            }
        };
        
        session.StoreObjects(products);
        session.SaveChanges();
    }
}