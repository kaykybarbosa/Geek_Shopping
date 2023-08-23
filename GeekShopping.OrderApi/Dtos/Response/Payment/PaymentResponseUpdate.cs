namespace GeekShopping.OrderApi.Dtos.Response.Payment
{
    public class PaymentResponseUpdate
    {
        public long OrderId { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
    }
}