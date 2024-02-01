using Marten;
using MartenDemo.Model;
using Serilog;

namespace MartenDemo.Commands;

public class ViewCustomers : ICommand
{
    private readonly IDocumentStore _store;

    public ViewCustomers(IDocumentStore store)
    {
        _store = store;
    }

    public void Run()
    {

        using var sesion = _store.LightweightSession();
        var q = sesion.Query<Customer>().ToList();


        Log.Information("Got data {@data}", q);
        

    }
}