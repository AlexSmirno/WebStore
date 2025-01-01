using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Orders;

namespace WebStoreServer.Features.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderService _OrderService;

        public OrderController(OrderService OrderService)
        {
            _OrderService = OrderService;
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


        [HttpPost("/find", Name = "find")]
        public async Task<ActionResult<List<OrderDTO>> GetOrdersByDTO([FromBody] OrderDTO order)
        {
            var result = await _OrderService.GetOrderByDTOAsync(order);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateOrder([FromBody] OrderDTO Order)
        {
            var result = await _OrderService.CreateOrder(Order);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateOrder([FromBody] Order Order)
        {
            var result = await _OrderService.UpdateOrder(Order);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteOrder([FromBody] Order Order)
        {
            var result = await _OrderService.DeleteOrder(Order);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }
    }
}
