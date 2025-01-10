using WebStore.DataServer.DAL;
using WebStore.DataServer.Services;

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
            builder.Services.AddTransient<StoreContext>();

            var app = builder.Build();

            app.MapGrpcService<ProductService>();

            // Configure the HTTP request pipeline.
            app.MapGet("/", () => 
            "Communication with gRPC endpoints must be made through a gRPC client. " +
            "To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            
            app.Run();
        }
    }
}
