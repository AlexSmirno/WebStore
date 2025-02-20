
using WebStore.Domain.Rabbit;
using WebStore.API.Features.Orders;
using WebStore.API.Features.Products;
using WebStore.API.Features.Clients;
using Serilog;

namespace WebStore.API
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();

            builder.Host.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(context.Configuration);
            });

            builder.Services.AddControllers();

            builder.Services.Configure<RabbitMQSetting>(options =>
            {
                options.HostName = builder.Configuration["RabbitMQ:HostName"];
                options.UserName = builder.Configuration["RabbitMQ:UserName"];
                options.Password = builder.Configuration["RabbitMQ:Password"];
            });

            builder.Services.AddTransient<ProductRPCSender>();
            builder.Services.AddTransient<OrderRPCSender>();
            builder.Services.AddTransient<ClientRPCSender>();

            builder.Services.AddTransient<OrderMQSender>();
            
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

            app.UseSerilogRequestLogging();

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
