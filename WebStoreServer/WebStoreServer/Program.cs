
using Microsoft.EntityFrameworkCore;
using WebStoreServer.DAL;
using WebStoreServer.DAL.Repositories;
using WebStoreServer.Features.Clients;
using WebStoreServer.Features.Products;
using WebStoreServer.Features.Senders;
using WebStoreServer.Features.Supplies;

namespace WebStoreServer
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddTransient<ProductService>();
            builder.Services.AddTransient<ProductRepository>();

            builder.Services.AddTransient<OrderService>();
            builder.Services.AddTransient<OrderRepository>();

            builder.Services.AddTransient<ClientService>();
            builder.Services.AddTransient<ClientRepository>();

            builder.Services.AddTransient<ISender, RPCSender>();


            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=store;Username=postgres;Password=admin");
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
