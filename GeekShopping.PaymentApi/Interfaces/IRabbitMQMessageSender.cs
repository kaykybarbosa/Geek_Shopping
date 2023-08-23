using GeekShopping.MessageBus;

namespace GeekShopping.PaymentApi.Interfaces
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage);
    }
}