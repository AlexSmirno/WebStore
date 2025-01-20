
using System.ComponentModel.DataAnnotations;

namespace WebStore.Contracts.Orders
{
    public class OrderType
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
