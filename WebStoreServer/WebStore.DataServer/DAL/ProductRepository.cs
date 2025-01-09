using WebStore.Domain.Products;

namespace WebStore.DataServer.DAL
{
    public class ProductRepository
    {
        private StoreContext _storeContext;
        public ProductRepository(StoreContext storeContext) 
        {
            _storeContext = new StoreContext();
        }

        public List<Product> GetProducts()
        {
            return _storeContext.Products;
        }
    }
}
