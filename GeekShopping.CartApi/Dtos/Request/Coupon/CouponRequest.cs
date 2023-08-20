namespace GeekShopping.CartApi.Dtos.Request.Coupon
{
    public class CouponRequest
    {
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}