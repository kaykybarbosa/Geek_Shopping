namespace GeekShopping.Email.Dto
{
    public class PaymentResultUpdateMessage
    {
        public long OrderId { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
    }
}