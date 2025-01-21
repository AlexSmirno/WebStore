using Microsoft.EntityFrameworkCore;

using WebStore.Domain.DAL.Interfaces;
using WebStore.Domain.Products;

namespace WebStore.Domain.DAL.EF.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = _context.Products;

            return await Task.FromResult(products);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return await Task.FromResult(new Product() { Id = -1 });
            }

            return await Task.FromResult(product);
        }

        public async Task<List<Product>> GetProductByIdsAsync(List<int> ids)
        {
            var product = await _context.Products.Where(p => ids.Contains(p.Id)).ToListAsync();

            if (product == null)
            {
                return await Task.FromResult(new List<Product>());
            }

            return await Task.FromResult(product);
        }

        public async Task<IEnumerable<Product>> GetProductsByObject(Product product)
        {
            var products = _context.Products;

            IEnumerable<Product> foundProducts = null;

            if (product.Id > 0)
                foundProducts = products.Where(p => p.Id == product.Id);

            if (product.ProductName != null && product.ProductName != "")
                foundProducts = products.Where(p => p.ProductName == product.ProductName);


            if (foundProducts == null || foundProducts.Count() == 0)
            {
                return await Task.FromResult(new List<Product>());
            }

            return await Task.FromResult(foundProducts);
        }

        public async Task<bool> AddProductAsync(Product newProduct)
        {
            try
            {
                var currentProduct = await _context.Products.AddAsync(newProduct);

                _context.SaveChanges();

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }


        public async Task<bool> UpdateProductAsync(Product newProduct)
        {
            try
            {
                var currentProduct = await _context.Products.FindAsync(newProduct.Id);

                if (currentProduct == null)
                {
                    return await Task.FromResult(false);
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

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            int count = await _context.Products.Where(p => p.Id == product.Id).ExecuteDeleteAsync();

            if (count == 0)
            {
                return await Task.FromResult(false);
            }

            _context.SaveChanges();
            return await Task.FromResult(true);
        }

    }
}
