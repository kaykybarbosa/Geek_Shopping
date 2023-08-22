using GeekShopping.MessageBus;

namespace GeekShopping.PaymentApi.Dtos
{
    public class PaymentMessageUpdate : BaseMessage
    {
        public long OrderId { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
    }
}