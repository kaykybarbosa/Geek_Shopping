namespace GeekShopping.OrderApi.Dtos.Response
{
    public class CartDetailResponse
    {
        public long Id { get; set; }
        public long CartHeaderId { get; set; }
        public long ProductId { get; set; }
        public virtual ProductResponse Product { get; set; }
        public int Count { get; set; }
    }
} 