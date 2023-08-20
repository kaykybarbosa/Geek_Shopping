namespace GeekShopping.CartApi.Dtos.Response.Coupon
{
    public class CouponResponse
    {
        public long Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}