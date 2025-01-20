

namespace WebStore.Contracts.Orders
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
