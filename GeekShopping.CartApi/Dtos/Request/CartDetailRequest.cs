namespace GeekShopping.CartApi.Dtos.Request
{
    public class CartDetailRequest
    {
        public long CartHeaderId { get; set; }
        public CartHeaderRequest CartHeader { get; set; }
        public long ProductId { get; set; }
        public ProductRequest Product { get; set; }
        public int Count { get; set; }
    }
}