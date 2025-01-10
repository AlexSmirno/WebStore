using Grpc.Core;
using WebStore.DataServer.DAL;

namespace WebStore.DataServer.Services
{
    public class ProductService : ProductServiceGRPS.ProductServiceGRPSBase
    {
        private ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override async Task<ListReply> GetProducts(VoidRequest request, ServerCallContext context)
        {
            var list = new ListReply();

            foreach (var product in _productRepository.GetProducts())
            {
                var productReply = new ProductReply();
                productReply.Id = product.Id;
                productReply.ProductName = product.ProductName;

                list.Products.Add(productReply);
            }

            return await Task.FromResult<ListReply>(list);
        }
    }
}
