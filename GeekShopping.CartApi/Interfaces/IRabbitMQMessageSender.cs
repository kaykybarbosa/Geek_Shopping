using GeekShopping.MessageBus;

namespace GeekShopping.CartApi.Interfaces
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
