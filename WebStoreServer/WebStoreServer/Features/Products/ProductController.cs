using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Products;

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

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var result = await _productService.GetProductsAsync();

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }


        [HttpPost("/api/Products", Name = "Products")]
        public async Task<ActionResult<List<Product>>> GetProductsByObject([FromBody] Product product)
        {
            var result = await _productService.GetProductsByObject(product);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateProduct([FromBody] Product product)
        {
            var result = await _productService.CreateProduct(product);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] Product product)
        {
            var result = await _productService.UpdateProduct(product);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProduct([FromBody] Product product)
        {
            var result = await _productService.DeleteProduct(product);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }
    }
}
