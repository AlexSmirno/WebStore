
using WebStore.Domain;
using WebStore.Domain.Orders;
using WebStoreServer.DAL.Repositories;
using WebStoreServer.Features.Senders;

namespace WebStoreServer.Features.Orders
{
    public class OrderService
    {
        private OrderRepository _OrderRepository;
        private ProductRepository _productRepository;
        private ISender _sender;
        public OrderService(
            OrderRepository OrderRepository, 
            ProductRepository productRepository, 
            ISender sender)
        {
            _OrderRepository = OrderRepository;
            _productRepository = productRepository;
            _sender = sender;
        }

        public async Task<Result<IEnumerable<Order>>> GetOrdersAsync()
        {
            var supplies = await _OrderRepository.GetAllOrdersAsync();

            return await Task.FromResult(supplies);
        }

        public async Task<Result<Order>> GetOrderByDTOAsync(OrderDTO order)
        {
            var supplies = await _OrderRepository.GetOrdersByDTO(order.Id);

            return await Task.FromResult(supplies);
        }

        public async Task<Result<bool>> CreateOrder(OrderDTO newOrder)
        {
            var productResult = await _productRepository.GetProductByIdsAsync(newOrder.Products.Select(p => p.Id).ToList());
            if (productResult.IsSucceeded == false)
            {
                return await Task.FromResult(new Result<bool>()
                {
                    IsSucceeded = false,
                    ErrorCode = productResult.ErrorCode,
                    ErrorMessage = productResult.ErrorMessage,
                    Data = false
                }
                );
            }

            var Order = newOrder.ToOrder();
            Order.Product = productResult.Data;

            if (newOrder.OrderType == "in")
            {
                return await CreateInOrder(Order);
            }

            if (newOrder.OrderType == "out")
            {
                return await CreateOutOrder(Order);
            }

            return new Result<bool> { 
                Data = false,
                ErrorCode = 404, 
                ErrorMessage = "Несущесвтующий тип поставки" + newOrder.OrderType, 
                IsSucceeded = false 
            };
        }

        //Добавление продуктов
        private async Task<Result<bool>> CreateInOrder(Order Order)
        {
            if (Order.Product == null)
            {
                return await Task.FromResult(new Result<bool>()
                    {
                        IsSucceeded = false,
                        ErrorCode = 404,
                        ErrorMessage = "Неизвестный продукт",
                        Data = false
                    }
                );
            }

            await _sender.Send("Do it!");

            Order.Product.Count += Order.Count;

            var res = await _OrderRepository.AddInOrderAsync(Order);

            return await Task.FromResult(res);
        }

        //Удаление продуктов
        private async Task<Result<bool>> CreateOutOrder(Order Order)
        {
            if (Order.Product == null)
            {
                return await Task.FromResult(new Result<bool>()
                    {
                        IsSucceeded = false,
                        ErrorCode = 400,
                        ErrorMessage = "Неизвестный продукт",
                    Data = false
                    }
                );
            }

            if (Order.Product.Count < Order.Count)
            {
                return await Task.FromResult(new Result<bool>()
                    {
                        IsSucceeded = false,
                        ErrorCode = 400,
                        ErrorMessage = "Недостаток продкта" + Order.Product.Id,
                        Data = false
                    }
                );
            }

            Order.Product.Count -= Order.Count;

            var res = await _OrderRepository.AddInOrderAsync(Order);

            return await Task.FromResult(res);
        }

        private void GetProductId()
        {

        }


        public async Task<Result<bool>> UpdateOrder(Order newOrder)
        {
            var res = await _OrderRepository.UpdateOrderAsync(newOrder);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> DeleteOrder(Order newOrder)
        {
            var res = await _OrderRepository.DeleteOrderAsync(newOrder);

            return await Task.FromResult(res);
        }
    }
}
