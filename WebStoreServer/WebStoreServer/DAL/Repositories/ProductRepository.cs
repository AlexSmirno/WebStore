using Microsoft.EntityFrameworkCore;
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
            var products = _context.ProductsTable;

            return await Task.FromResult(new Result<IEnumerable<Product>>(products));
        }

        public async Task<Result<Product>> GetProductByIdAsync(int id)
        {
            var product = await _context.ProductsTable.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return await Task.FromResult(new Result<Product>() 
                { IsSucceeded = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<Product>(product));
        }

        public async Task<Result<List<Product>>> GetProductByIdsAsync(List<int> ids)
        {
            var product = await _context.ProductsTable.Where(p => ids.Contains(p.Id)).ToListAsync();

            if (product == null)
            {
                return await Task.FromResult(new Result<List<Product>>() 
                { IsSucceeded = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<List<Product>>(product));
        }

        public async Task<Result<IEnumerable<Product>>> GetProductsByObject(Product product)
        {
            var products = _context.ProductsTable;

            IEnumerable<Product> foundProducts = null;

            if (product.Id > 0)
                foundProducts = products.Where(p => p.Id == product.Id);

            if (product.ProductName != null && product.ProductName != "")
                foundProducts = products.Where(p => p.ProductName == product.ProductName);

            foundProducts = products.Where(p => p.Count > 0);

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
                var currentProduct = await _context.ProductsTable.AddAsync(newProduct);

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
                var currentProduct = await _context.ProductsTable.FindAsync(newProduct.Id);

                if (currentProduct == null)
                {
                    return await Task.FromResult(new Result<bool>() 
                    { IsSucceeded = false, Data = false, ErrorMessage = "There is no this element", ErrorCode = 404 });
                }

                currentProduct.ProductName = newProduct.ProductName;
                currentProduct.Price = newProduct.Price;
                currentProduct.Count = newProduct.Count;
                currentProduct.Description = newProduct.Description;
                currentProduct.Size = newProduct.Size;

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
            int count = await _context.ProductsTable.Where(p => p.Id == product.Id).ExecuteDeleteAsync();

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
