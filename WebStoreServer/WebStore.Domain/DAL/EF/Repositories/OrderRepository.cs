using Microsoft.EntityFrameworkCore;

using WebStore.Domain.DAL.Interfaces;
using WebStore.Domain.Orders;

namespace WebStore.Domain.DAL.EF.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private StoreContext _context;
        public OrderRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderType>> GetOrderTypesAsync()
        {
            var types = _context.OrderTypes;

            return await Task.FromResult(types);
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatusesAsync()
        {
            var statuses = _context.OrderStatuses;

            return await Task.FromResult(statuses);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = _context.Orders;

            return await Task.FromResult(orders);
        }

        public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int id)
        {
            var orders = _context.Orders
                .Include(o => o.ProductOrderInfos)
                .Include(o => o.Client)
                .Include(o => o.OrderType)
                .Include(o => o.OrderStatus)
                .Where(o => o.ClientId == id);

            return await Task.FromResult(orders);
        }

        public async Task<bool> AddOrderAsync(Order newOrder)
        {
            try
            {
                var res = await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }


        public async Task<bool> UpdateOrderAsync(Order newOrder)
        {
            try
            {
                var currentOrder = await _context.Orders.FindAsync(newOrder.Id);

                if (currentOrder == null)
                {
                    return await Task.FromResult(false);
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

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteOrderAsync(Order Order)
        {
            int count = await _context.Orders.Where(p => p.Id == Order.Id).ExecuteDeleteAsync();

            if (count == 0)
            {
                return await Task.FromResult(false);
            }

            _context.SaveChanges();
            return await Task.FromResult(true);
        }
    }
}
