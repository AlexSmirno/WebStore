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

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var result = await _productService.GetProductsAsync();

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            if (result.ErrorCode == 404) return NotFound();

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }


        [HttpGet("{ProductName}", Name = "/get")]
        public async Task<ActionResult<List<Product>>> GetProductsByName(string name)
        {
            var result = await _productService.GetProductByNameAsync(name);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            if (result.ErrorCode == 404) return NotFound();

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

            if (result.ErrorCode/100 == 4) return BadRequest();

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

            if (result.ErrorCode / 100 == 4) return BadRequest();

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

            if (result.ErrorCode / 100 == 4) return BadRequest();

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }
    }
}
