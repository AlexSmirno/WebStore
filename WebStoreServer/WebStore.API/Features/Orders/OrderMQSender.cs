using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

using WebStore.Domain.Orders;
using WebStore.Domain.Rabbit;

namespace WebStore.API.Features.Orders
{
    public class OrderMQSender
    {
        private readonly RabbitMQSetting _rabbitMqSetting;

        public OrderMQSender(IOptions<RabbitMQSetting> rabbitMqSetting)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
        }

        public async Task PublishMessageAsync(OrderDTO message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.UserName,
                Password = _rabbitMqSetting.Password
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queueName, false, false, false, null);

            var messageJson = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            await channel.BasicPublishAsync("", queueName, body);
        }
    }
}
