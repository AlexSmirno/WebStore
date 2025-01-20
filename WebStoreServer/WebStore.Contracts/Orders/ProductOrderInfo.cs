
using WebStore.Contracts.Products;

namespace WebStore.Contracts.Orders
{
    public class ProductOrderInfo
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int Count { get; set; }
    }
}
