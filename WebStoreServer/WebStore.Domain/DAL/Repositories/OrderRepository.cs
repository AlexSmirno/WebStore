using Microsoft.EntityFrameworkCore;

using WebStore.Domain.Orders;

namespace WebStore.Domain.DAL.Repositories
{
    public class OrderRepository
    {

        private StoreContext _context;
        public OrderRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<OrderType>>> GetOrderTypes()
        {
            var types = _context.OrderTypes;

            return await Task.FromResult(new Result<IEnumerable<OrderType>>(types));
        }

        public async Task<Result<IEnumerable<OrderStatus>>> GetOrderStatuses()
        {
            var statuses = _context.OrderStatuses;

            return await Task.FromResult(new Result<IEnumerable<OrderStatus>>(statuses));
        }

        public async Task<Result<IEnumerable<Order>>> GetAllOrdersAsync()
        {
            var orders = _context.Orders;

            return await Task.FromResult(new Result<IEnumerable<Order>>(orders));
        }

        public async Task<Result<IEnumerable<Order>>> GetOrdersByClientIdAsync(int id)
        {
            var orders = _context.Orders.Include(o => o.ProductOrderInfos).Include(o => o.Client).Where(o => o.ClientId == id);

            return await Task.FromResult(new Result<IEnumerable<Order>>(orders));
        }

        public async Task<Result<IEnumerable<Order>>> GetOrdersByDTO(OrderDTO order)
        {
            var orders = _context.Orders.Include(o => o.OrderType);

            IEnumerable<Order> foundOrders = orders;
            if (order.Id > 0)
                foundOrders = foundOrders.Where(o => o.Id == order.Id);

            if (order.Date != "")
                foundOrders = foundOrders.Where(o => string.Compare(o.Date, order.Date) == -1);

            if (order.Time != "")
                foundOrders = foundOrders.Where(o => string.Compare(o.Time, order.Time) == -1);

            if (order.OrderType != "")
                foundOrders = foundOrders.Where(o => o.OrderType.Description == order.OrderType);

            if (order == null)
            {
                return await Task.FromResult(new Result<IEnumerable<Order>>()
                { IsSucceeded = false, ErrorMessage = "There are no these elements", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<IEnumerable<Order>>(foundOrders));
        }

        public async Task<Result<bool>> AddOrderAsync(Order newOrder)
        {
            try
            {
                var res = await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();

                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                //TODO: Think about error message for user
                return await Task.FromResult(new Result<bool>()
                { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 400 });
            }
        }


        public async Task<Result<bool>> UpdateOrderAsync(Order newOrder)
        {
            try
            {
                var currentOrder = await _context.Orders.FindAsync(newOrder.Id);

                if (currentOrder == null)
                {
                    return await Task.FromResult(new Result<bool>()
                    { IsSucceeded = false, Data = false, ErrorMessage = "There is no this element", ErrorCode = 404 });
                }

                if (newOrder.ClientId > 0)
                    currentOrder.ClientId = newOrder.ClientId;

                if (newOrder.Date != null)
                    currentOrder.Date = newOrder.Date;

                if (newOrder.Time != null)
                    currentOrder.Time = newOrder.Time;

                if (newOrder.OrderTypeId > 0)
                    currentOrder.OrderTypeId = newOrder.OrderTypeId;


                _context.Orders.Update(currentOrder);

                _context.SaveChanges();

                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Result<bool>()
                { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 503 });
            }
        }

        public async Task<Result<bool>> DeleteOrderAsync(Order Order)
        {
            int count = await _context.Orders.Where(p => p.Id == Order.Id).ExecuteDeleteAsync();

            if (count == 0)
            {
                return await Task.FromResult(new Result<bool>()
                { IsSucceeded = false, Data = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            _context.SaveChanges();
            return await Task.FromResult(new Result<bool>(true));
        }
    }
}
