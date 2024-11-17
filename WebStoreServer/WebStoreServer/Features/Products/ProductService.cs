using Microsoft.AspNetCore.Mvc;
using WebStoreServer.Models.Products;

namespace WebStoreServer.Features.Products
{
    public class ProductService
    {


        public async Task<ActionResult<List<Product>>> GetProductsAsync()
        {
            var products = new List<Product>();

            return await Task.FromResult(products);
        }
    }
}
