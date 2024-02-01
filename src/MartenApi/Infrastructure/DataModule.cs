using Autofac;
using Marten;
using MartenApi.Data;
using Weasel.Core;

namespace MartenApi.Infrastructure;

public class DataModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(
            ctx =>
            {
                
                var config = ctx.Resolve<IConfiguration>();
                var connstring = config["Settings:ConnectionString"] ?? throw new ArgumentNullException("Connection String");
                
                
                return DocumentStore.For(
                    _ =>
                    {
                        _.Connection(connstring);
                        _.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
                        _.Advanced.HiloSequenceDefaults.MaxLo = 50;

                        // _.Schema.For<Customer>().Duplicate(s => s.Email, "varchar(200)");
                        // _.Schema.For<Order>().Duplicate(s => s.CustomerId, "uuid");
                    });
            }).As<IDocumentStore>().SingleInstance();


        builder.RegisterType<ProductRepository>().As<IProductRepository>();
    }
}