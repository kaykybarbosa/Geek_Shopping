using GeekShopping.OrderApi.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace GeekShopping.OrderApi.Services
{
    public class RabbitMQMessageConsumer : BackgroundService
    {
        private readonly OrderService _orderService;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQMessageConsumer(OrderService orderService)
        {
            _orderService = orderService;
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "checkoutqueue", false, false, false, null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evt) =>
            {
                var context = Encoding.UTF8.GetString(evt.Body.ToArray());
                CheckoutHeaderResponse response = JsonSerializer.Deserialize<CheckoutHeaderResponse>(context);
 
                ProcessOrder(response).GetAwaiter().GetResult();

                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume("chekoutqueue", false, consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessOrder(CheckoutHeaderResponse response)
        {
            throw new NotImplementedException();
        }
    }
 } 