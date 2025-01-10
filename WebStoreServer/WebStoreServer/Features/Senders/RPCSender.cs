using Grpc.Net.Client;
using WebStore.Domain;
using WebStore.Domain.Orders;
using WebStore.Domain.Products;
using WebStoreServer.Features.Products;

namespace WebStoreServer.Features.Senders
{
    public class RPCSender : ISender
    {
        public RPCSender()
        {

        }


        public async Task<Result<List<Product>>> GetProductAsync()
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
    }
}
