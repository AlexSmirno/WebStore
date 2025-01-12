using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Orders;
using WebStore.Domain.Rabbit;
using WebStoreServer.Features.gRPCSenders;
using WebStoreServer.Features.RabbitMQSender;

namespace WebStoreServer.Features.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderService _OrderService;
        private OrderRPCSender _rpcSender;
        private OrderMQSender _mqSender;

        public OrderController(OrderRPCSender rpcSender, OrderMQSender mqSender)
        {
            _rpcSender = rpcSender;
            _mqSender = mqSender;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetOrders()
        {
            var result = await _OrderService.GetOrdersAsync();

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpGet("/api/Order/{id}", Name = "OrdersById")]
        public async Task<ActionResult<List<OrderDTO>>> GetOrders(int id)
        {
            var result = await _rpcSender.GetOrdersByClientId(id);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateOrder([FromBody] OrderDTO order)
        {
            try
            {
                await _mqSender.PublishMessageAsync(order, RabbitMQQueues.OrderQueue);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return await Task.FromResult(false);
            }
        }
    }
}
