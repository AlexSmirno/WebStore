using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebStore.Domain.Products;

namespace WebStoreServer.Features.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductRPCSender _sender;

        public ProductController(ProductRPCSender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var result = await _sender.GetProductsAsync();

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }


        [HttpPost("/api/Products", Name = "Products")]
        public async Task<ActionResult<Product>> GetProductByObject([FromBody] Product product)
        {
            var result = await _sender.GetProductByObjectAsync(product);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateProduct([FromBody] Product product)
        {
            var result = await _sender.CreateProductAsync(product);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] Product product)
        {
            var result = await _sender.UpdateProductAsync(product);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProduct([FromBody] Product product)
        {
            var result = await _sender.DeleteProductAsync(product);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }
    }
}
