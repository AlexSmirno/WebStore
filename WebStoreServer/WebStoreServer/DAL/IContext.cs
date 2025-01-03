using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Clients;
using WebStore.Domain.Orders;
using WebStore.Domain.Products;

namespace WebStoreServer.DAL
{
    public interface IContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrderInfo> ProductOrderInfos { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
    }
}
