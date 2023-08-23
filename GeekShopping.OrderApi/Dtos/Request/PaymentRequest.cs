namespace GeekShopping.OrderApi.Dtos.Request
{
    public class PaymentRequest
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