using WebStore.Domain;
using WebStore.Domain.Orders;
using WebStore.Domain.DAL.EF.Repositories;

namespace WebStore.OrderServer.Orders
{
    public class OrderService
    {
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;

        private List<OrderType> orderTypes;
        private List<OrderStatus> orderStatuses;
        public OrderService(OrderRepository orderRepository, ProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            orderTypes = _orderRepository.GetOrderTypesAsync().Result.ToList();
            orderStatuses = _orderRepository.GetOrderStatusesAsync().Result.ToList();
        }

        public async Task<bool> CreateOrder(OrderDTO newOrder)
        {
            var productResult = await _productRepository
                .GetProductByIdsAsync(newOrder.Products.Select(p => p.Id)
                .ToList());

            if (productResult.Count != newOrder.Products.Count)
            {
                return await Task.FromResult(false);
            }

            var order = newOrder.ToOrder();

            foreach (var product in productResult)
            {
                order.ProductOrderInfos.Find(poi => poi.ProductId == product.Id).Product = product;
            }

            order.OrderStatusId = orderStatuses.FirstOrDefault(s => s.Description == "Pending").Id;

            if (newOrder.OrderType == 
                orderTypes.FirstOrDefault(s => s.Description == "Import").Description)
            {
                order.OrderTypeId = orderTypes[0].Id;
                return await CreateInOrder(order);
            }

            if (newOrder.OrderType ==
                orderTypes.FirstOrDefault(s => s.Description == "Export").Description)
            {
                order.OrderTypeId = orderTypes[1].Id;
                return await CreateOutOrder(order);
            }

            return false;
        }

        //Добавление продуктов
        private async Task<bool> CreateInOrder(Order order)
        {
            if (order.ProductOrderInfos == null)
            {
                return false;
            }

            foreach (var item in order.ProductOrderInfos)
            {
                item.Product.Count += item.Count;
            }

            var res = await _orderRepository.AddOrderAsync(order);

            return await Task.FromResult(res);
        }

        //Удаление продуктов
        private async Task<bool> CreateOutOrder(Order order)
        {
            if (order.ProductOrderInfos == null)
            {
                return false;
            }

            foreach (var item in order.ProductOrderInfos)
            {
                if (item.Product.Count >= item.Count)
                {
                    item.Product.Count -= item.Count;
                }
                else
                {
                    return false;
                }
            }

            var res = await _orderRepository.AddOrderAsync(order);

            return await Task.FromResult(res);
        }
    }
}
