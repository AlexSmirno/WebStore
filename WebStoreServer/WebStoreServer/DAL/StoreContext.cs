using Microsoft.EntityFrameworkCore;
using WebStoreServer.Models.Clients;
using WebStoreServer.Models.Divisions;
using WebStoreServer.Models.Products;
using WebStoreServer.Models.Supplies;

namespace WebStoreServer.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base (options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        public DbSet<Product> ProductsTable { get; set; }
        public DbSet<Client> ClientsTable { get; set; }
        public DbSet<Supply> SuppliesTable { get; set; }
        public DbSet<Division> DivisionsTable { get; set; }
        public DbSet<ProductType> ProductTypesTable { get; set; }
    }
}
