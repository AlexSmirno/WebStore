using WebStore.Contracts.Products;

namespace WebStore.Contracts.Orders
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string? OrderType { get; set; }
        public string? OrderStatus { get; set; }
        public int ClientId { get; set; }
        public List<Product>? Products { get; set; }

        public OrderDTO() { }

        public OrderDTO(Order order)
        {
            Id = order.Id;
            Date = order.Date;
            Time = order.Time;
            OrderType = order.OrderType?.Description;
            OrderStatus = order.OrderStatus?.Description;
            Products = new List<Product>();

            foreach (var product in order.ProductOrderInfos)
            {
                Products.Add(new Product() { Id = product.ProductId, Count = product.Count });
            }
        }

        public Order ToOrder()
        {
            var order = new Order
            {
                Id = Id,
                Date = Date,
                Time = Time,
                ClientId = ClientId,
                ProductOrderInfos = new List<ProductOrderInfo>()
            };

            foreach (var product in Products)
            {
                order.ProductOrderInfos.Add(new ProductOrderInfo()
                {
                    ProductId = product.Id,
                    Count = product.Count,
                    OrderId = order.Id,
                    Order = order
                });
            }

            return order;
        }
    }
}
