using GeekShopping.OrderApi.Dtos.Response.Payment;
using GeekShopping.OrderApi.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderApi.Services
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly OrderRepository _orderRepository;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchageName = "DirectPaymentUpdateExchage";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";

        public RabbitMQPaymentConsumer(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchageName, ExchangeType.Direct);

            _channel.QueueDeclare(PaymentOrderUpdateQueueName, false, false, false, null);

            _channel.QueueBind(PaymentOrderUpdateQueueName, ExchageName, "PaymentOrder");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evt) =>
            {
                var context = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentResponseUpdate response = JsonSerializer.Deserialize<PaymentResponseUpdate>(context);

                ProcessPaymentStatus(response).GetAwaiter().GetResult();

                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume(PaymentOrderUpdateQueueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessPaymentStatus(PaymentResponseUpdate response)
        {
            try
            {
                await _orderRepository.UpdateOrderPaymentStatus(response.OrderId, response.Status);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}