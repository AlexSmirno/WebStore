
using WebStore.Domain.Products;

namespace WebStore.Domain.Orders
{
    public class ProductOrderInfo
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }
    }
}
