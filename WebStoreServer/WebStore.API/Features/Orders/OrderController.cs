using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Orders;
using WebStore.Domain.Rabbit;

namespace WebStore.API.Features.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderRPCSender _rpcSender;
        private OrderMQSender _mqSender;

        public OrderController(OrderRPCSender rpcSender, OrderMQSender mqSender)
        {
            _rpcSender = rpcSender;
            _mqSender = mqSender;
        }

        [HttpGet("{id}", Name = "OrdersById")]
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
