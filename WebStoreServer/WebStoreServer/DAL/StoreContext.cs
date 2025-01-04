using Microsoft.EntityFrameworkCore;

using WebStore.Domain.Clients;
using WebStore.Domain.Orders;
using WebStore.Domain.Products;

namespace WebStoreServer.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base (options)
        {
            string name = Database.GetDbConnection().Database;
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(nameof(Product.Id));
            modelBuilder.Entity<Client>().HasKey(nameof(Client.Id));
            modelBuilder.Entity<Order>().HasKey(nameof(Order.Id));
            modelBuilder.Entity<OrderType>().HasKey(nameof(OrderType.Id));
            modelBuilder.Entity<ProductOrderInfo>().HasKey(
                    nameof(ProductOrderInfo.ProductId), 
                    nameof(ProductOrderInfo.OrderId)
                );

            modelBuilder.Entity<Client>().HasMany(nameof(Client.Orders));
            modelBuilder.Entity<Order>().HasOne(nameof(Order.Client));

            modelBuilder.Entity<Order>().HasMany(nameof(Order.ProductOrderInfos));
            modelBuilder.Entity<ProductOrderInfo>().HasOne(nameof(ProductOrderInfo.Order));

            modelBuilder.Entity<ProductOrderInfo>().HasOne(nameof(ProductOrderInfo.Product));

            modelBuilder.Entity<OrderType>().HasMany(nameof(OrderType.Orders));
            modelBuilder.Entity<Order>().HasOne(nameof(Order.OrderType));
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrderInfo> ProductOrderInfos { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
    }
}
