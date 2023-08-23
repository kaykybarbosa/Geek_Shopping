using GeekShopping.MessageBus;

namespace GeekShopping.OrderApi.Dtos.Response.Payment
{
    public class PaymentResponse : BaseMessage
    {
        public long OrderId { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public decimal PurchaseAmount { get; set; }
        public string ExpiryMonthYear { get; set; }
        public string Email { get; set; }
    }
}