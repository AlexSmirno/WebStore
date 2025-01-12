using Grpc.Core;

using WebStore.Domain.Products;
using WebStore.DataServer.Extention;

using WebStore.Domain.DAL.Repositories;

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

            var productsResult = await _productRepository.GetAllProductsAsync();

            foreach (var product in productsResult.Data)
            {
                var productReply = product.ToProductRequest();

                list.Products.Add(productReply);
            }

            return await Task.FromResult<ListReply>(list);
        }

        public override async Task<ProductRequest> GetProductsByObject(ProductRequest request, ServerCallContext context)
        {
            var product = new Product();
            product.FromProductRequest(request);

            var productsResult = await _productRepository.GetProductsByObject(product);

            var recivedProduct = productsResult.Data.FirstOrDefault();

            return await Task.FromResult<ProductRequest>(recivedProduct.ToProductRequest());
        }


        public override async Task<ResultReply> CreateProduct(ProductRequest request, ServerCallContext context)
        {
            var product = new Product();
            product.FromProductRequest(request);

            var productsResult = await _productRepository.AddProductAsync(product);

            var result = new ResultReply();
            result.Result = productsResult.Data;

            return await Task.FromResult<ResultReply>(result);
        }

        public override async Task<ResultReply> UpdateProduct(ProductRequest request, ServerCallContext context)
        {
            var product = new Product();
            product.FromProductRequest(request);

            var productsResult = await _productRepository.UpdateProductAsync(product);

            var result = new ResultReply();
            result.Result = productsResult.Data;

            return await Task.FromResult<ResultReply>(result);
        }
        
        public override async Task<ResultReply> DeleteProduct(ProductRequest request, ServerCallContext context)
        {
            var product = new Product();
            product.FromProductRequest(request);

            var productsResult = await _productRepository.DeleteProductAsync(product);

            var result = new ResultReply();
            result.Result = productsResult.Data;

            return await Task.FromResult<ResultReply>(result);
        }
    }
}
