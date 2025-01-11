
using WebStore.Domain;
using WebStore.Domain.Orders;
using WebStoreServer.DAL.Repositories;
using WebStoreServer.Features.Products;

namespace WebStoreServer.Features.Orders
{
    public class OrderService
    {
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;
        private ProductRPCSender _sender;

        private List<OrderType> orderTypes;
        private List<OrderStatus> orderStatuses;

        public OrderService(OrderRepository OrderRepository)
        {
            orderTypes = _orderRepository.GetOrderTypes().Result.Data.ToList();
            orderStatuses = _orderRepository.GetOrderStatuses().Result.Data.ToList();
        }

        public async Task<Result<IEnumerable<OrderDTO>>> GetOrdersAsync()
        {
            var ordersResult = await _orderRepository.GetAllOrdersAsync();

            if (ordersResult.IsSucceeded == false)
            {
                return new Result<IEnumerable<OrderDTO>>
                {
                    IsSucceeded = false,
                    ErrorCode = ordersResult.ErrorCode,
                    ErrorMessage = ordersResult.ErrorMessage
                };
            }

            var newResult = new Result<IEnumerable<OrderDTO>>();
            var list = new List<OrderDTO>();

            foreach (var order in ordersResult.Data) 
                list.Add(new OrderDTO(order));

            newResult.Data = list;
            
            return await Task.FromResult(newResult);
        }

        public async Task<Result<IEnumerable<OrderDTO>>> GetOrderByDTOAsync(OrderDTO order)
        {
            var result = await _orderRepository.GetOrdersByDTO(order);

            if (result.IsSucceeded == false)
            {
                return await Task.FromResult(new Result<IEnumerable<OrderDTO>>()
                {
                    IsSucceeded = false,
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                });
            }

            List<OrderDTO> orders = new List<OrderDTO>();

            foreach (var item in result.Data)
            {
                orders.Add(new OrderDTO(item));
            }

            return await Task.FromResult(new Result<IEnumerable<OrderDTO>>(orders));
        }

        public async Task<Result<bool>> CreateOrder(OrderDTO newOrder)
        {
            var productResult = await _productRepository
                .GetProductByIdsAsync(newOrder.Products.Select(p => p.Id)
                .ToList());

            if (productResult.IsSucceeded == false || productResult.Data.Count != newOrder.Products.Count)
            {
                return await Task.FromResult(new Result<bool>()
                {
                    IsSucceeded = false,
                    ErrorCode = 404,
                    ErrorMessage = "There are no these products",
                    Data = false
                });
            }

            var order = newOrder.ToOrder();

            foreach (var product in productResult.Data)
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

            return new Result<bool> { 
                Data = false,
                ErrorCode = 404, 
                ErrorMessage = "There is no this order type: " + newOrder.OrderType, 
                IsSucceeded = false 
            };
        }

        //Добавление продуктов
        private async Task<Result<bool>> CreateInOrder(Order order)
        {
            if (order.ProductOrderInfos == null)
            {
                return await Task.FromResult(new Result<bool>()
                    {
                        IsSucceeded = false,
                        ErrorCode = 404,
                        ErrorMessage = "Unknown products",
                        Data = false
                    }
                );
            }

            //Набудующее
            //await _sender.Send("Do it!");

            foreach (var item in order.ProductOrderInfos)
            {
                item.Product.Count += item.Count;
            }

            var res = await _orderRepository.AddOrderAsync(order);

            return await Task.FromResult(res);
        }

        //Удаление продуктов
        private async Task<Result<bool>> CreateOutOrder(Order order)
        {
            if (order.ProductOrderInfos == null)
            {
                return await Task.FromResult(new Result<bool>()
                {
                    IsSucceeded = false,
                    ErrorCode = 404,
                    ErrorMessage = "Unknown products",
                    Data = false
                });
            }

            //Набудующее
            //await _sender.Send("Do it!");

            foreach (var item in order.ProductOrderInfos)
            {
                if (item.Product.Count >= item.Count)
                {
                    item.Product.Count -= item.Count;
                }
                else
                {
                    return await Task.FromResult(new Result<bool>()
                    {
                        IsSucceeded = false,
                        ErrorCode = 400,
                        ErrorMessage = "Not enought product: " + item.Product.Id + " " + item.Product.ProductName,
                        Data = false
                    });
                }
            }

            var res = await _orderRepository.AddOrderAsync(order);

            return await Task.FromResult(res);
        }
        public async Task<Result<bool>> UpdateOrder(Order newOrder)
        {
            var res = await _orderRepository.UpdateOrderAsync(newOrder);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> DeleteOrder(Order newOrder)
        {
            var res = await _orderRepository.DeleteOrderAsync(newOrder);

            return await Task.FromResult(res);
        }
    }
}
