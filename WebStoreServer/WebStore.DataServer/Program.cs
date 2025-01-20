using Microsoft.EntityFrameworkCore;
using WebStore.DataServer.Services;

using WebStore.Domain.DAL.Repositories;
using WebStore.Domain.DAL;

namespace WebStore.DataServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();

            builder.Services.AddTransient<ProductRepository>();
            builder.Services.AddTransient<OrderRepository>();
            builder.Services.AddTransient<ClientRepository>();

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
