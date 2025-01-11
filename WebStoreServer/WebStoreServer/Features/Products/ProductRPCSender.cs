using Grpc.Net.Client;
using WebStore.Domain;
using WebStore.Domain.Products;
using WebStoreServer.Extention;

namespace WebStoreServer.Features.Products
{
    public class ProductRPCSender
    {
        public ProductRPCSender()
        {

        }

        public async Task<Result<List<Product>>> GetProductsAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5005");

            var client = new ProductServiceGRPS.ProductServiceGRPSClient(channel);
            var reply = await client.GetProductsAsync(new VoidRequest());

            var list = new List<Product>();

            foreach (var item in reply.Products)
            {
                list.Add(new Product()
                {
                    Id = item.Id,
                    Size = item.Size,
                    Count = item.Count,
                    Description = item.Description,
                    ProductName = item.ProductName
                });
            }

            return new Result<List<Product>>(list);
        }


        public async Task<Result<Product>> GetProductByObjectAsync(Product product)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5005");

            var client = new ProductServiceGRPS.ProductServiceGRPSClient(channel);

            var request = product.ToProductRequest();

            var reply = await client.GetProductsByObjectAsync(request);

            var recivedProduct = new Product();
            recivedProduct.FromProductRequest(reply);

            return new Result<Product>(recivedProduct);
        }

        public async Task<Result<bool>> CreateProductAsync(Product product)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5005");

            var client = new ProductServiceGRPS.ProductServiceGRPSClient(channel);

            var request = product.ToProductRequest();

            var reply = await client.CreateProductAsync(request);

            return new Result<bool>(reply.Result);
        }

        public async Task<Result<bool>> UpdateProductAsync(Product product)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5005");

            var client = new ProductServiceGRPS.ProductServiceGRPSClient(channel);

            var request = product.ToProductRequest();

            var reply = await client.UpdateProductAsync(request);

            return new Result<bool>(reply.Result);
        }

        public async Task<Result<bool>> DeleteProductAsync(Product product)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5005");

            var client = new ProductServiceGRPS.ProductServiceGRPSClient(channel);

            var request = product.ToProductRequest();

            var reply = await client.DeleteProductAsync(request);

            return new Result<bool>(reply.Result);
        }

    }
}
