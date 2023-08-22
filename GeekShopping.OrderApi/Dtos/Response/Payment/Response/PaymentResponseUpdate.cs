namespace GeekShopping.OrderApi.Dtos.Response.Payment.Response
{
    public class PaymentResponseUpdate
    {
        public long OrderId { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
    }
}