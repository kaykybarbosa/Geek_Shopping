using GeekShopping.MessageBus;

namespace GeekShopping.OrderApi.Interfaces.MessageSenfer
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}