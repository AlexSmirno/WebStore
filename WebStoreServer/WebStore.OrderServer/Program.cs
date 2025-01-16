
using WebStore.Domain.Rabbit;
using WebStore.OrderServer.Orders;
using WebStore.Domain.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.DAL;

namespace WebStore.OrderServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<RabbitMQSetting>(options =>
            {
                options.HostName = builder.Configuration["RabbitMQ:HostName"];
                options.UserName = builder.Configuration["RabbitMQ:UserName"];
                options.Password = builder.Configuration["RabbitMQ:Password"];
            });

            builder.Services.AddHostedService<OrderConsumerService>();

            builder.Services.AddTransient<OrderService>();
            builder.Services.AddTransient<OrderRepository>();
            builder.Services.AddTransient<ProductRepository>();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=test_store;Username=postgres;Password=admin");
            });

            var app = builder.Build();

            app.Run();
        }
    }
}
