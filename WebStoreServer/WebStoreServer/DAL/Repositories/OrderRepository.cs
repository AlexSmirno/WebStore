using Microsoft.EntityFrameworkCore;

using WebStore.Domain;
using WebStore.Domain.Orders;

namespace WebStoreServer.DAL.Repositories
{
    public class OrderRepository
    {

        private StoreContext _context;
        public OrderRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Order>>> GetAllOrdersAsync()
        {
            var supplies = _context.SuppliesTable;

            return await Task.FromResult(new Result<IEnumerable<Order>>(supplies));
        }

        public async Task<Result<IEnumerable<Order>>> GetOrdersByDTO(OrderDTO order)
        {
            var orders = _context.SuppliesTable.Include(o => o.OrderType);

            IEnumerable<Order> foundOrders = orders;
            if (order.Id > 0) 
                foundOrders = foundOrders.Where(o => o.Id == order.Id);

            if (order.Date != "")
                foundOrders = foundOrders.Where(o => String.Compare(o.Date, order.Date) == -1);

            if (order.Time != "")
                foundOrders = foundOrders.Where(o => String.Compare(o.Time, order.Time) == -1);

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
                var currentOrder = await _context.SuppliesTable.AddAsync(newOrder);

                _context.SaveChanges();

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
                var currentOrder = await _context.SuppliesTable.FindAsync(newOrder.Id);

                if (currentOrder == null)
                {
                    return await Task.FromResult(new Result<bool>() 
                    { IsSucceeded = false, Data = false, ErrorMessage = "There is no this element", ErrorCode = 404 });
                }

                currentOrder.ClientId = newOrder.ClientId;
                currentOrder.Date = newOrder.Date;
                currentOrder.Time = newOrder.Time;
                currentOrder.OrderType = newOrder.OrderType;

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
            int count = await _context.SuppliesTable.Where(p => p.Id == Order.Id).ExecuteDeleteAsync();

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
