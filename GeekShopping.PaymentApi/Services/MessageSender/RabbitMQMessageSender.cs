using GeekShopping.MessageBus;
using GeekShopping.PaymentApi.Dtos;
using GeekShopping.PaymentApi.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentApi.Services.MessageSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private const string ExchangeName = "DirectPaymentUpdateExchage";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _userName = "guest";
            _password = "guest";
        }

        public void SendMessage(BaseMessage message)
        {
            if (_connection == null)
            {
                CreateConnection();
            }
            using var channel = _connection.CreateModel();

            channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, durable: false); // durable: false => will be removed afeter being read

            channel.QueueDeclare(PaymentEmailUpdateQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare(PaymentOrderUpdateQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            channel.QueueBind(PaymentEmailUpdateQueueName, exchange: ExchangeName, routingKey: "PaymentEmail");
            channel.QueueBind(PaymentOrderUpdateQueueName, exchange: ExchangeName, routingKey: "PaymentOrder");

            byte[] body = GetMessageAsByteArray(message);

            channel.BasicPublish(exchange: ExchangeName, routingKey: "PaymentEmail", basicProperties: null, body: body);
            channel.BasicPublish(exchange: ExchangeName, routingKey: "PaymentOrder", basicProperties: null, body: body);
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize<PaymentMessageUpdate>((PaymentMessageUpdate)message, options);

            return Encoding.UTF8.GetBytes(json);
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}