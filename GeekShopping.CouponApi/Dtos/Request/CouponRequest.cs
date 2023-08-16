namespace GeekShopping.CouponApi.Dtos.Request
{
    public class CouponRequest
    {
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}