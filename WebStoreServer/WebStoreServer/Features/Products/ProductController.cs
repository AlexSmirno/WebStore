using Microsoft.AspNetCore.Mvc;
using WebStoreServer.Models.Products;

namespace WebStoreServer.Features.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var result = await _productService.GetProductsAsync();

            if (result != null)
            {
                return await Task.FromResult(result);
            }

            return NotFound();
        }
    }
}
