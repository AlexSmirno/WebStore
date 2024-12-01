
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using WebStoreServer.Models;
using WebStoreServer.Models.Products;

namespace WebStoreServer.DAL.Repositories
{
    public class ProductRepository
    {
        private StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Product>>> GetAllProductsAsync()
        {
            var products = _context.ProductsTable;

            return await Task.FromResult(new Result<IEnumerable<Product>>(products));
        }

        public async Task<Result<Product>> GetProductByIdAsync(Guid id)
        {
            var product = await _context.ProductsTable.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return await Task.FromResult(new Result<Product>() { IsSucceeded = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<Product>(product));
        }

        public async Task<Result<IEnumerable<Product>>> GetProductsByNameAsync(string name)
        {
            var product = _context.ProductsTable.Where(p => p.ProductName == name);

            if (product == null || product.Count() == 0)
            {
                return await Task.FromResult(new Result<IEnumerable<Product>> () { IsSucceeded = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<IEnumerable<Product>>(product));
        }

        public async Task<Result<bool>> AddProductAsync(Product newProduct)
        {
            try
            {
                var currentProduct = await _context.ProductsTable.AddAsync(newProduct);

                _context.SaveChanges();

                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //TODO: Think about error message for user
                return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 400 });
            }
        }


        public async Task<Result<bool>> UpdateProductAsync(Product newProduct)
        {
            try
            {
                var currentProduct = await _context.ProductsTable.FindAsync(newProduct.Id);

                if (currentProduct == null)
                {
                    return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
                }

                currentProduct.ProductName = newProduct.ProductName;
                currentProduct.Price = newProduct.Price;
                currentProduct.Count = newProduct.Count;
                currentProduct.Description = newProduct.Description;
                currentProduct.DevisionId = newProduct.DevisionId;
                currentProduct.ProductTypeId = newProduct.ProductTypeId;
                currentProduct.Size = newProduct.Size;

                _context.SaveChanges();
                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 503 });
            }
        }

        public async Task<Result<bool>> DeleteProductAsync(Product product)
        {
            int count = await _context.ProductsTable.Where(p => p.Id == product.Id).ExecuteDeleteAsync();

            if (count == 0)
            {
                return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            _context.SaveChanges();
            return await Task.FromResult(new Result<bool>(true));
        }

    }
}
