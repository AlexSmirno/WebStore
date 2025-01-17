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

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=test_store;Username=postgres;Password=admin");
            });

            var app = builder.Build();

            app.MapGrpcService<ProductService>();
            app.MapGrpcService<OrderService>();

            // Configure the HTTP request pipeline.
            app.MapGet("/", () => 
            "Communication with gRPC endpoints must be made through a gRPC client. " +
            "To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            
            app.Run();
        }
    }
}
