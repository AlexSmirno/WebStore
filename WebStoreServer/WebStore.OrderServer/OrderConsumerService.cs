using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using WebStore.Domain.Orders;
using WebStore.Domain.Rabbit;
using WebStore.OrderServer.Orders;

namespace WebStore.OrderServer
{
    public class OrderConsumerService : BackgroundService
    {
        private OrderService _orderService;
        private readonly RabbitMQSetting _rabbitMqSetting;
        private IConnection _connection;
        private IChannel _channel;

        public OrderConsumerService(
            IOptions<RabbitMQSetting> rabbitMqSetting, 
            IServiceProvider serviceProvider,
            OrderService orderService)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
            _orderService = orderService;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.UserName,
                Password = _rabbitMqSetting.Password
            };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StartConsuming(RabbitMQQueues.OrderQueue, stoppingToken);
            await Task.CompletedTask;
        }

        private async void StartConsuming(string queueName, CancellationToken cancellationToken)
        {
            await _channel.QueueDeclareAsync(queueName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    OrderDTO order = JsonSerializer.Deserialize<OrderDTO>(message);

                    _orderService.CreateOrder(order);

                    await _channel.BasicAckAsync(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    await _channel.BasicRejectAsync(ea.DeliveryTag, true);
                }
            };

            await _channel.BasicConsumeAsync(queueName, false, consumer);
        }

        public override void Dispose()
        {
            _channel.CloseAsync();
            _connection.CloseAsync();
            base.Dispose();
        }
    }
}
