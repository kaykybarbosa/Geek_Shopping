using GeekShopping.OrderApi.Dtos;
using GeekShopping.OrderApi.Dtos.Response.Payment;
using GeekShopping.OrderApi.Interfaces.MessageSenfer;
using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderApi.Services
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _orderRepository;
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(OrderRepository orderRepository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _orderRepository = orderRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;

            var factory = new ConnectionFactory
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

            _channel.BasicConsume("checkoutqueue", false, consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessOrder(CheckoutHeaderResponse response)
        {
            OrderHeader orderHeader = new()
            {
                UserId = response.UserId,
                CouponCode = response.CouponCode,
                PurchaseAmount = response.PurchaseAmount,
                DiscountAmount = response.DiscountAmount,
                FirstName = response.FirstName,
                LastName = response.LastName,
                DateTime = response.DateTime,
                OrderTime = DateTime.Now,
                Phone = response.Phone,
                Email = response.Email,
                CardNumber = response.CardNumber,
                CVV = response.CVV,
                ExpiryMonthYear = response.ExpiryMonthYear,
                PaymentStatus = false,
                OrderDetails = new List<OrderDetail>()
            };

            foreach(var details in response.CartDetails)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = details.ProductId,
                    ProductName = details.Product.Name,
                    Price = details.Product.Price,
                    Count = details.Count
                };

                orderHeader.CartTotalItens += details.Count;
                orderHeader.OrderDetails.Add(orderDetail);
            }

            if(orderHeader != null)
            {
                await _orderRepository.AddOrderHeader(orderHeader);
            }

            PaymentResponse payment = new()
            {
                OrderId = orderHeader.Id,
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                PurchaseAmount = orderHeader.PurchaseAmount,
                Email = orderHeader.Email,
            };

            try
            {
                _rabbitMQMessageSender.SendMessage(payment, "orderpaymentprocessqueue");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
 } 