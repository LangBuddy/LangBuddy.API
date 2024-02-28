using Consumer.Models;
using Consumer.Options;
using Consumer.Services.HttpService;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Consumer.Consumers
{
    public class EmailSendConsumer : BackgroundService
    {
        private IServiceProvider _sp;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        private readonly RabbitMqConfig _rabbitMqConfig;

        public EmailSendConsumer(IServiceProvider sp, IOptions<RabbitMqConfig> options)
        {
            _rabbitMqConfig = options.Value;

            _sp = sp;

            _factory = new ConnectionFactory() { HostName = _rabbitMqConfig.Host };

            _connection = _factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: "emails",
                durable: true,
                exclusive: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }

            Console.WriteLine("Get Consumer");
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();

                Console.WriteLine(Encoding.UTF8.GetString(body));
                Console.WriteLine(JsonSerializer.Deserialize<Email>(Encoding.UTF8.GetString(body)));

                var email = JsonSerializer.Deserialize<Email>(Encoding.UTF8.GetString(body));

                Console.WriteLine($"to: {email.To} \nsubject: {email.Subject} \ntext: {email.Text}");

                Task.Run(async () =>
                {
                    using (var scope = _sp.CreateScope())
                    {
                        var httpService = scope.ServiceProvider.GetRequiredService<IHttpService>();

                        Console.WriteLine("=================================================================");

                        await httpService.SendEmail(email);

                        Console.WriteLine("=================================================================");
                    }
                });

            };

            _channel.BasicConsume(queue: "emails", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
