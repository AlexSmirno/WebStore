using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebStoreServer.DAL.Repositories;
using WebStoreServer.Models;
using WebStoreServer.Models.Products;

namespace WebStoreServer.Features.Products
{
    public class ProductService
    {
        private ProductRepository _repository;
        public ProductService(ProductRepository repository) 
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<Product>>> GetProductsAsync()
        {
            var products = await _repository.GetAllProductsAsync();

            return await Task.FromResult(products);
        }

        public async Task<Result<Product>> GetProductByIdAsync(int id)
        {
            var products = await _repository.GetProductByIdAsync(id);

            return await Task.FromResult(products);
        }

        public async Task<Result<IEnumerable<Product>>> GetProductByNameAsync(string name)
        {
            var products = await _repository.GetProductsByNameAsync(name);

            return await Task.FromResult(products);
        }

        public async Task<Result<bool>> CreateProduct(Product newProduct)
        {
            var res = await _repository.AddProductAsync(newProduct);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> UpdateProduct(Product newProduct)
        {
            var res = await _repository.UpdateProductAsync(newProduct);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> DeleteProduct(Product newProduct)
        {
            var res = await _repository.DeleteProductAsync(newProduct);

            return await Task.FromResult(res);
        }
    }
}
