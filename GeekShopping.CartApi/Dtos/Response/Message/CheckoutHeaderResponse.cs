using GeekShopping.MessageBus;

namespace GeekShopping.CartApi.Dtos.Response.Message
{
    public class CheckoutHeaderResponse : BaseMessage
    {
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public int CartTotalItens { get; set; }
        public IEnumerable<CartDetailResponse> CartDetails { get; set; }
    }
}