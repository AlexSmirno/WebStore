using Microsoft.EntityFrameworkCore;

using WebStore.DataServer.Services;

using WebStore.Domain.DAL.EF;
using WebStore.Domain.DAL.Interfaces;
using WebStore.Domain.DAL.EF.Repositories;

namespace WebStore.DataServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();

            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IClientRepository, ClientRepository>();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=test_store;Username=postgres;Password=admin");
            });

            var app = builder.Build();

            app.MapGrpcService<ProductService>();
            app.MapGrpcService<OrderService>();
            app.MapGrpcService<ClientService>();

            app.Run();
        }
    }
}
