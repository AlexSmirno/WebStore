using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Orders;

namespace WebStore.Domain.DAL.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<OrderType>> GetOrderTypesAsync();
        public Task<IEnumerable<OrderStatus>> GetOrderStatusesAsync();
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
        public Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int id);
        public Task<bool> AddOrderAsync(Order newOrder);
        public Task<bool> UpdateOrderAsync(Order newOrder);
        public Task<bool> DeleteOrderAsync(Order Order);
    }
}
