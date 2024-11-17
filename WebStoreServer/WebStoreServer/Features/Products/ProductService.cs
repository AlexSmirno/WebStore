using Microsoft.AspNetCore.Mvc;
using WebStoreServer.Models.Products;

namespace WebStoreServer.Features.Products
{
    public class ProductService
    {


        public async Task<ActionResult<List<Product>>> GetProductsAsync()
        {
            var products = new List<Product>()
            {
                new Product() { Id = Guid.NewGuid(), Price = "Дохуя", Count = -1 },
                new Product() { Id = Guid.NewGuid(), Price = "Дохуя", Count = 0 },
                new Product() { Id = Guid.NewGuid(), Price = "Дохуя", Count = 1 },
                new Product() { Id = Guid.NewGuid(), Price = "Дохуя", Count = 2 },
            };

            return await Task.FromResult(products);
        }
    }
}
