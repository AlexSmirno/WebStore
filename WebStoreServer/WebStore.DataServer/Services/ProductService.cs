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

        public ListReply GetProducts(VoidRequest request, ServerCallContext context)
        {
            var list = new ListReply();

            foreach (var product in _productRepository.GetProducts())
            {
                var productReply = new ProductReply();

                list.Products.Add(productReply);
            }

            return list;
        }
    }
}
