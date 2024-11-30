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
            var products = await _repository.GetAllProducts();

            return await Task.FromResult(products);
        }

        public async Task<Result<Product>> GetProductByIdAsync(Guid id)
        {
            var products = await _repository.GetProductById(id);

            return await Task.FromResult(products);
        }

        public async Task<Result<IEnumerable<Product>>> GetProductByNameAsync(string name)
        {
            var products = await _repository.GetProductsByName(name);

            return await Task.FromResult(products);
        }

        public async Task<Result<bool>> CreateProduct(Product newProduct)
        {
            newProduct.Id = Guid.NewGuid();

            var res = await _repository.AddProduct(newProduct);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> UpdateProduct(Product newProduct)
        {
            var res = await _repository.UpdateProduct(newProduct);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> DeleteProduct(Product newProduct)
        {
            var res = await _repository.DeleteProduct(newProduct);

            return await Task.FromResult(res);
        }
    }
}
