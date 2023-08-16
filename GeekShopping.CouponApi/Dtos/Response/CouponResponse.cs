namespace GeekShopping.CouponApi.Dtos.Response
{
    public class CouponResponse
    {
        public long Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}