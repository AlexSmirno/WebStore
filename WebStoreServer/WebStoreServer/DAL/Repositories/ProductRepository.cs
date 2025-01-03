using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebStore.Domain;
using WebStore.Domain.Clients;
using WebStore.Domain.Products;

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
            var products = _context.Products;

            return await Task.FromResult(new Result<IEnumerable<Product>>(products));
        }

        public async Task<Result<Product>> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return await Task.FromResult(new Result<Product>() 
                { IsSucceeded = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<Product>(product));
        }

        public async Task<Result<List<Product>>> GetProductByIdsAsync(List<int> ids)
        {
            var product = await _context.Products.Where(p => ids.Contains(p.Id)).ToListAsync();

            if (product == null)
            {
                return await Task.FromResult(new Result<List<Product>>() 
                { IsSucceeded = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<List<Product>>(product));
        }

        public async Task<Result<IEnumerable<Product>>> GetProductsByObject(Product product)
        {
            var products = _context.Products;

            IEnumerable<Product> foundProducts = null;

            if (product.Id > 0)
                foundProducts = products.Where(p => p.Id == product.Id);

            if (product.ProductName != null && product.ProductName != "")
                foundProducts = products.Where(p => EF.Functions.FuzzyStringMatchDifference(p.ProductName, product.ProductName) > 0);


            if (foundProducts == null || foundProducts.Count() == 0)
            {
                return await Task.FromResult(new Result<IEnumerable<Product>> () 
                { IsSucceeded = false, ErrorMessage = "There are no these elements", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<IEnumerable<Product>>(foundProducts));
        }

        public async Task<Result<bool>> AddProductAsync(Product newProduct)
        {
            try
            {
                var currentProduct = await _context.Products.AddAsync(newProduct);

                _context.SaveChanges();

                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //TODO: Think about error message for user
                return await Task.FromResult(new Result<bool>()
                { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 400 });
            }
        }


        public async Task<Result<bool>> UpdateProductAsync(Product newProduct)
        {
            try
            {
                var currentProduct = await _context.Products.FindAsync(newProduct.Id);

                if (currentProduct == null)
                {
                    return await Task.FromResult(new Result<bool>() 
                    { IsSucceeded = false, Data = false, ErrorMessage = "There is no this element", ErrorCode = 404 });
                }

                if (newProduct.ProductName != null)
                    currentProduct.ProductName = newProduct.ProductName;

                if (newProduct.Price > 0)
                    currentProduct.Price = newProduct.Price;

                if (newProduct.Count > 0)
                    currentProduct.Count = newProduct.Count;

                if (newProduct.Size > 0)
                    currentProduct.Size = newProduct.Size;

                if (newProduct.Description != null)
                    currentProduct.Description = newProduct.Description;

                _context.Products.Update(currentProduct);
                _context.SaveChanges();

                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Result<bool>() 
                { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 503 });
            }
        }

        public async Task<Result<bool>> DeleteProductAsync(Product product)
        {
            int count = await _context.Products.Where(p => p.Id == product.Id).ExecuteDeleteAsync();

            if (count == 0)
            {
                return await Task.FromResult(new Result<bool>() 
                { IsSucceeded = false, Data = false, ErrorMessage = "There is no this element", ErrorCode = 404 });
            }

            _context.SaveChanges();
            return await Task.FromResult(new Result<bool>(true));
        }

    }
}
