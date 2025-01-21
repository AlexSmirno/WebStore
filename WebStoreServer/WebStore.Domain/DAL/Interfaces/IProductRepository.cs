
using WebStore.Domain.Products;

namespace WebStore.Domain.DAL.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync();

        public Task<Product> GetProductByIdAsync(int id);

        public Task<List<Product>> GetProductByIdsAsync(List<int> ids);

        public Task<bool> AddProductAsync(Product newProduct);

        public Task<bool> UpdateProductAsync(Product newProduct);

        public Task<bool> DeleteProductAsync(Product product);
    }
}
