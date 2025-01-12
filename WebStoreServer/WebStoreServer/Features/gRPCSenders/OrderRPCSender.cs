using Grpc.Net.Client;
using WebStore.Domain;
using WebStore.Domain.Orders;
using WebStoreServer.Extention;

namespace WebStoreServer.Features.gRPCSenders
{
    public class OrderRPCSender
    {
        public OrderRPCSender()
        {

        }

        public async Task<Result<List<OrderDTO>>> GetOrdersByClientId(int id)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5005");

            var client = new OrderServiceGRPS.OrderServiceGRPSClient(channel);
            var clientRequest = new ClientRequest();
            clientRequest.Id = id;
            var reply = await client.GetOrdersByClientIdAsync(clientRequest);

            var list = new List<OrderDTO>();

            foreach (var order in reply.Orders)
            {
                list.Add(order.ToOrderDTO());
            }

            return new Result<List<OrderDTO>>(list);
        }
    }
}
