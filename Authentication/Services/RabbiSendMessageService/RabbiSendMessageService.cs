using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Services.Options;
using System.Text;
using System.Text.Json;

namespace Services.RabbiSendMessageService
{
    public class RabbiSendMessageService : IRabbiSendMessageService
    {
        private readonly RabbitMqConfig _rabbitMqConfig;

        public RabbiSendMessageService(IOptions<RabbitMqConfig> rabbitMqConfig)
        {
            _rabbitMqConfig = rabbitMqConfig.Value;
        }

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqConfig.Host,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare("emails", durable: true, exclusive: false);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "emails", body: body);
        }
    }
}
