
using Grpc.Core;
using WebStore.DataServer.Extention;
using WebStore.Domain.DAL.EF.Repositories;

namespace WebStore.DataServer.Services
{
    public class OrderService : OrderServiceGRPS.OrderServiceGRPSBase
    {
        private OrderRepository _orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public override async Task<OrderListReply> GetOrdersByClientId(ClientIdRequest client, ServerCallContext context)
        {
            var orders = await _orderRepository.GetOrdersByClientIdAsync(client.Id);

            var reply = new OrderListReply();

            foreach (var order in orders)
            {
                var newOrder = new OrderReply();
                newOrder.FromOrder(order);
                reply.Orders.Add(newOrder);
            }

            return reply;
        }

    }
}
