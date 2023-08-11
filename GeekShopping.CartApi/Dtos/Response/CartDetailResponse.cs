using GeekShopping.CartApi.Dtos.Request;

namespace GeekShopping.CartApi.Dtos.Response
{
    public class CartDetailResponse
    {
        public long Id { get; set; }
        public long CartHeaderId { get; set; }
        public CartHeaderResponse CartHeader { get; set; }
        public long ProductId { get; set; }
        public ProductResponse Product { get; set; }
        public int Count { get; set; }
    }
}