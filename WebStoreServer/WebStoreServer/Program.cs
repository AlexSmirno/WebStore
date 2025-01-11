
using Microsoft.EntityFrameworkCore;
using WebStoreServer.DAL;
using WebStoreServer.DAL.Repositories;
using WebStoreServer.Features.Clients;
using WebStoreServer.Features.Orders;
using WebStoreServer.Features.Products;
using WebStoreServer.Features.Senders;

namespace WebStoreServer
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddTransient<ISender, RPCSender>();
            
            /*
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                string str = builder.Configuration["Database:ConnectionString"];
                Console.WriteLine(str);
                options.UseNpgsql(str);
            });
            */

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
