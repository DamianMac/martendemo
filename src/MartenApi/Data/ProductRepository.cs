using Marten;
using MartenApi.Extensions;
using MartenApi.Model;

namespace MartenApi.Data;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    Page<Product> GetProductsBetter(int page);
}

public class ProductRepository : IProductRepository
{
    private readonly IDocumentStore _store;

    public ProductRepository(IDocumentStore store)
    {
        _store = store;
    }

    public IEnumerable<Product> GetProducts()
    {
        using var session = _store.LightweightSession();
        var products = session.Query<Product>().Where(p => p.Price > 1);
        return products;
    }

    public Page<Product> GetProductsBetter(int page)
    {
        using var session = _store.LightweightSession();
        var resultPage = session.RunPageQueryOrdered<Product, string>(p => p.Price > 1, p => p.Name, page);
        return resultPage;
    }
}