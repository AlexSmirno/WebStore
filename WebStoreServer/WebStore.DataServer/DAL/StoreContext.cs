using System.Collections.Generic;
using WebStore.Domain.Products;

namespace WebStore.DataServer.DAL
{
    public class StoreContext
    {
        public StoreContext() 
        {
            Products = new List<Product>();
            Products.Add(new Product() { Id = 1, ProductName = "1" });
            Products.Add(new Product() { Id = 2, ProductName = "2" });
        }

        public List<Product> Products { get; set; }
    }
}
