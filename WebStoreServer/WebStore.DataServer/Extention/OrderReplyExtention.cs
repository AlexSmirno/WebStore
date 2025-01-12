using WebStore.Domain.Orders;
using WebStore.Domain.Products;

namespace WebStore.DataServer.Extention
{
    public static class OrderReplyExtention
    {
        public static OrderDTO ToOrderDTO(this OrderReply orderReply)
        {
            OrderDTO orderDTO = new OrderDTO();

            orderDTO.Id = orderReply.Id;
            orderDTO.Date = orderReply.Date;
            orderDTO.Time = orderReply.Time;
            orderDTO.OrderStatus = orderReply.Status;
            orderDTO.OrderType = orderReply.OrderType;
            orderDTO.Products = new List<Product>();

            foreach (var product in orderReply.ProductList.Products)
            {
                var productReply = new ProductReply();
            }

            return orderDTO;
        }

        public static void FromOrderDTO(this OrderReply orderReply, OrderDTO dto)
        {
            orderReply.Id = dto.Id;
            orderReply.Date = dto.Date;
            orderReply.Time = dto.Time;
            orderReply.Status = dto.OrderStatus;
            orderReply.OrderType = dto.OrderType;

            var list = new ProductList();

            foreach (var product in orderReply.ProductList.Products)
            {
                var productReply = new ProductReply();
                productReply.ProductId = product.ProductId;
                productReply.Count = product.Count;
                list.Products.Add(productReply);
            }
        }


        public static void FromOrder(this OrderReply orderReply, Order order)
        {
            orderReply.Id = order.Id;
            orderReply.Date = order.Date;
            orderReply.Time = order.Time;
            orderReply.Status = order.OrderStatus?.Description;
            orderReply.OrderType = order.OrderType?.Description;

            var list = new ProductList();

            foreach (var product in orderReply.ProductList.Products)
            {
                var productReply = new ProductReply();
                productReply.ProductId = product.ProductId;
                productReply.Count = product.Count;
                list.Products.Add(productReply);
            }
        }
    }
}
